using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
    [UserAuthorize(menuId: 1105)]
    public class ApproveDirectPurchaseOrderController : Controller
    {
        // GET: ApprovePurchaseOrder
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;


        public ApproveDirectPurchaseOrderController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var list = _unitofWork.BillInfo.GetAllDirectPurchaseOrder(branchid, userid);
            return View(list);
        }

        public ActionResult Create(int id)
        {
            try
            {
                var list = _unitofWork.BillInfo.GetById(id);
                list.VendorName = CodeService.GetVendorNameFromBillInfo(id);
                list.poList = _unitofWork.PurchaseOrder.GetItemByBillId(id);
                list.SubTotal = list.poList.Sum(x => x.amount);
                list.bill_amount = list.SubTotal + list.vat_amt;
                list.billno = CodeService.GetBillNoFromBillId(id);
                list.EmployeeList = _dropDownList.EmployeeList();
                list.bill_notes = list.bill_notes;
                list.IsVatablePO = list.vat_amt > 0 ? true : false;
                list.branch_id = list.branch_id;
                return View(list);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Create(BillInfoVM data)
        {

            try
            {
                using (TransactionScope Scope = new TransactionScope(TransactionScopeOption.Required))
                {

                    var existingBillInfo = _unitofWork.BillInfo.GetById(data.bill_id);

                    if (existingBillInfo == null)
                    {
                        return Json(new { success = false, mes = "Bill information not found.", JsonRequestBehavior.AllowGet });
                    }

                    existingBillInfo.ApprovedDate = DateTime.Now;
                    existingBillInfo.ApprovedBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                    existingBillInfo.Status = "Direct P.O Approved";
                    existingBillInfo.Approved_Message = data.Approved_Message;
                    _unitofWork.BillInfo.Update(existingBillInfo);
                    _unitofWork.Save();

                    int currentBranchId = Convert.ToInt32(Session["BranchId"]);
                    int? requestingBranch = (int)data.branch_id;
                    if (requestingBranch == null)
                    {
                        return Json(new { success = false, mes = "Requesting branch not specified.", JsonRequestBehavior.AllowGet });
                    }
                    //in purchase insert
                    var billid = CodeService.GetBillInfoId();
                    var inpurchase = new InPurchaseVM();
                    var insbranch = new INBranchVM();
                    foreach (var itm in data.poList.Where(x => x.product_code > 0))
                    {
                        inpurchase.bill_id = billid;
                        inpurchase.prod_code = itm.product_code;
                        inpurchase.p_qty = (int)itm.Received_Qty;
                        inpurchase.p_rate = itm.rate;
                        inpurchase.p_stk_remain = (int)itm.Received_Qty;
                        inpurchase.entered_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                        inpurchase.entered_date = DateTime.Now.Date;
                        inpurchase.branch_id = (int)requestingBranch;
                        inpurchase.order_msg_id = data.pomid;
                        _unitofWork.InPurchase.Insert(inpurchase);
                        _unitofWork.Save();

                        var serialstatus = CodeService.GetSerialStatusForProductById(itm.product_code);
                        var purid = CodeService.GetPurchaseId();
                        var branchid = Convert.ToInt32(Session["BranchId"]);
                        //SerialProductStock insert
                        if (serialstatus == true)
                        {
                            var tpdata = _dapperrepo.GetTempPurchaseDataByTPO(itm.product_code,data.bill_id);
                            var tpodata = _dapperrepo.GetTempPurchaseOtherDataByTPO(tpdata);
                            int snfrom = Convert.ToInt32(tpodata.sn_from);
                            int snto = Convert.ToInt32(tpodata.sn_to);
                            var spsdata = new SerialProductStockVM();
                            _dapperrepo.InsertSerialProductStockPurchaseVoucher(Convert.ToInt32(Session["BranchId"]), itm.product_code, purid, snfrom, snto);
                            var tpdataremove = _unitofWork.TempPurchase.GetById(tpdata);
                            _unitofWork.TempPurchase.Delete(tpdataremove.id);
                            _unitofWork.Save();

                            var tpodataremove = _unitofWork.TempPurchaseOther.GetById(tpdata);
                            _unitofWork.TempPurchaseOther.Delete(tpdataremove.id);
                            _unitofWork.Save();

                            var inpurdata = _dapperrepo.getInPurchaseDataByPIdAndBillId(itm.product_code, billid);
                            inpurdata.p_sn_from = snfrom.ToString();
                            inpurdata.p_sn_to = snto.ToString();
                            _unitofWork.InPurchase.Update(inpurdata);
                            _unitofWork.Save();
                        }
                        var in_branch = new INBranchVM();
                        var inbranch = _unitofWork.InBranchAssign.GetItemById(itm.product_code, (int)requestingBranch);
                        if (inbranch == null)
                        {
                            in_branch.BRANCH_ID = (int)requestingBranch;
                            in_branch.PRODUCT_ID = itm.product_code;
                            in_branch.IS_ACTIVE = true;
                            in_branch.stock_in_hand = (int)itm.Received_Qty;
                            _unitofWork.InBranchAssign.Insert(in_branch);
                            _unitofWork.Save();
                        }
                        else
                        {
                            inbranch.stock_in_hand = inbranch.stock_in_hand + (int)itm.Received_Qty;
                            _unitofWork.InBranchAssign.Update(inbranch);
                            _unitofWork.Save();

                        }

                    }
                    //Voucher update
                    var VoucherNo = _dapperrepo.getVoucherNo();
                    _dapperrepo.UpdateVoucherNo();
                    _unitofWork.Save();

                    Scope.Complete();

                    var mes = " Approved and Voucher Saved Successfully, Voucher No: " + VoucherNo + "";
                    return Json(new { success = true, mes = mes, JsonRequestBehavior.AllowGet });
                }

            }
            catch (Exception Ex)

            {
                return Json(new
                {
                    success = false,
                    mes = "Error Occurred!!",
                    JsonRequestBehavior.AllowGet
                });
            }
        }

        [HttpPost]
        public JsonResult Update(int id, string approved_Message)
        {
            try
            {
                var Item = _unitofWork.BillInfo.GetById(id);
                var notificationdata = _unitofWork.InNotifications.GetById(id);
                if (string.IsNullOrEmpty(approved_Message))
                {
                    return Json(new { success = false, message = "Approve Message can't be empty." });
                }

                Item.CancelledBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                Item.CancelledDate = DateTime.Now;
                Item.Status = "Direct P.O Rejected";
                Item.Approved_Message = approved_Message;
                _unitofWork.BillInfo.Update(Item);
                _unitofWork.Save();
                if (notificationdata != null)
                {
                    notificationdata.Status = true;
                    _unitofWork.InNotifications.Update(notificationdata);
                    _unitofWork.Save();
                }
                _unitofWork.Save();
                TempData["Success"] = "<p>Purchase Order :  Successfully Rejected by</p>" + Item.CancelledBy;
                TempData["Title"] = "<strong>Data Cancelled</strong> <br />";
                TempData["Icon"] = "fa fa-check";

                return Json(new { success = true, message = "Purchase Order successfully rejected." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }




        //[HttpPost]
        //public ActionResult UpdateApproveOrder(PurchaseOrderMessageVM data)
        //{
        //    try
        //    {
        //        var approvedby = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
        //        var res = _dapperrepo.UpdatePurchaseOrderById(data.poList, data.id, approvedby, data.vat_amt);
        //        if (res)
        //        {
        //            var mes = "Purchase Order Approved Successfully";
        //            return Json(new { success = true, mes = mes });
        //        }
        //        else
        //        {
        //            var mes = "Cannot approved. please try again later";
        //            return Json(new { success = false, mes = mes });
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return Json(new { success = false, mes = "Error Occurred !!" });
        //    }

        //}


    }
}
