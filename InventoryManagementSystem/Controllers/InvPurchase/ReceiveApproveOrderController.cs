using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
   
    public class ReceiveApproveOrderController : Controller
    {
        // GET: ReceiveApproveOrder

        private IUnitOfWork _unitofWork;
        private DapperRepoServices _dapperrepo;
        public ReceiveApproveOrderController()
        {
            _unitofWork = new UnitOfWork();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 21)]
        public ActionResult Index()
        {
            ViewBag.msg = TempData["msg"] as string;
            var list = _unitofWork.PurchaseOrderMessage.GetAllApproved();
            return View(list);
        }
        [UserAuthorize(menuId: 21)]
        public ActionResult Create(int id)
        {
            var list = new BillInfoVM();
            var pomRec = _unitofWork.PurchaseOrderMessage.GetById(id);
            list.poList = _unitofWork.PurchaseOrder.GetItemById(id);
            list.SubTotal = list.poList.Sum(x => x.amount);
            list.vat_amt = pomRec.vat_amt > 0 ? (Convert.ToDecimal(0.13) * list.SubTotal) : 0;
            list.bill_amount = list.SubTotal + Convert.ToDecimal(list.vat_amt);
            list.bill_date = DateTime.Now.Date;
            list.VendorName=pomRec.vendorname;
            list.pomid = id;
            list.IsVatablePO = pomRec.vat_amt > 0 ? true : false;
            return View(list);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(BillInfoVM data)
        {

            // data.poList = _unitofWork.PurchaseOrder.GetItemById(data.pomid);
            try
            {
                var Item = _unitofWork.PurchaseOrderMessage.GetById(data.pomid);
                if (data.billno != null && data.billno.Trim() != "" && data.bill_notes != null && data.bill_notes.Trim() != "")
                {
                    bool billNoExists = _unitofWork.BillInfo.BillNoExists(data.billno.Trim(), data.VendorName.Trim());
                    if (billNoExists)
                    {
                        
                        var mes = ($"Bill No.{data.billno} from Vendor'{data.VendorName}' already exists!");
                        return Json(new { success = false, mes = mes, JsonRequestBehavior.AllowGet });
                    }

                    using (TransactionScope Scope = new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        var vendorid = CodeService.GetVendorIdFromPurchaseOrderMessage(data.pomid);
                        data.party_code = Convert.ToString(vendorid);
                        data.bill_type = "p";
                        data.entered_date = DateTime.Now;
                        data.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                        _unitofWork.BillInfo.Insert(data);
                        _unitofWork.Save();

                        //in purchase insert
                        var billid = CodeService.GetBillInfoId();
                        var inpurchase = new InPurchaseVM();
                        foreach (var itm in data.poList.Where(x => x.product_code > 0))
                        {
                            inpurchase.bill_id = billid;
                            inpurchase.prod_code = itm.product_code;
                            inpurchase.p_qty = (int)itm.Received_Qty;
                            inpurchase.p_rate = itm.rate;
                            inpurchase.p_stk_remain = (int)itm.Received_Qty;
                            inpurchase.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            inpurchase.entered_date = DateTime.Now.Date;
                            inpurchase.branch_id = Convert.ToInt32(Session["BranchId"]);
                            inpurchase.order_msg_id = data.pomid;
                            _unitofWork.InPurchase.Insert(inpurchase);
                            _unitofWork.Save();

                            //SerialProductStock insert
                            if (itm.serialstatus == true)
                            {
                                var purid = CodeService.GetPurchaseId();
                                var tpdata = _dapperrepo.GetTempPurchaseDataByTPO(itm.product_code,data.bill_id);
                                var tpodata = _dapperrepo.GetTempPurchaseOtherDataByTPO(tpdata);
                                int snfrom = Convert.ToInt32(tpodata.sn_from);
                                int snto = Convert.ToInt32(tpodata.sn_to);
                                var spsdata = new SerialProductStockVM();
                                _dapperrepo.InsertSerialProductStockPurchaseVoucher(Convert.ToInt32(Session["BranchId"]), itm.product_code, purid, snfrom, snto);
                                var tpdataremove = _unitofWork.TempPurchase.GetById(tpdata);
                                _unitofWork.TempPurchase.Delete(tpdataremove.id);

                                var tpodataremove = _unitofWork.TempPurchaseOther.GetById(tpdata);
                                _unitofWork.TempPurchaseOther.Delete(tpdataremove.id);


                                //int snfrom = _dapperrepo.GetSNFFromSerialProductStock(itm.product_code, data.pomid);
                                //int snto = _dapperrepo.GetSNTFromSerialProductStock(itm.product_code, data.pomid);
                                //_dapperrepo.UpdateSerialProductStockByPOMId(data.pomid,purid,itm.product_code);

                                var inpurdata = _dapperrepo.getInPurchaseDataByPIdAndBillId(itm.product_code, billid);
                                inpurdata.p_sn_from = snfrom.ToString();
                                inpurdata.p_sn_to = snto.ToString();
                                _unitofWork.InPurchase.Update(inpurdata);
                            }
                            // in branch update
                            var inbranch = _unitofWork.InBranchAssign.GetItemById(itm.product_code, Convert.ToInt32(Session["BranchId"]));
                            if (inbranch == null)
                            {
                                var mes1 = "Branch not assignet for " + itm.productname + "";
                                return Json(new { success = false, mes = mes1, JsonRequestBehavior.AllowGet });
                            }
                            inbranch.stock_in_hand = inbranch.stock_in_hand + (int)itm.Received_Qty;
                            _unitofWork.InBranchAssign.Update(inbranch);
                            _unitofWork.Save();

                        }
                        //purchase order message Update
                        Item.received_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                        Item.received_date = DateTime.Now.Date;
                        Item.received_desc = data.bill_notes;
                        Item.status = "Received";
                        Item.vat_amt = (decimal)data.vat_amt;
                        _unitofWork.PurchaseOrderMessage.Update(Item);
                        _unitofWork.Save();

                        var VoucherNo = _dapperrepo.getVoucherNo();
                        _dapperrepo.UpdateVoucherNo();
                        Scope.Complete();

                        var mes = "Voucher Saved Successfully, Voucher No: " + VoucherNo + "";
                        return Json(new { success = true, mes = mes, JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    var mes = "Bill No and Remarks is required !!";
                    return Json(new { success = false, mes = mes, JsonRequestBehavior.AllowGet });

                }
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                return Json(new { success = false, mes = "Error Occurred!!", JsonRequestBehavior.AllowGet });
            }
        }
    }
}