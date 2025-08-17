using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvMovement
{
    [UserAuthorize]
    public class DispatchRequisitionController : Controller
    {
        // GET: DispatchRequisition
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;

        public DispatchRequisitionController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 32)]
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var assignedrole = Session["AssignRole"].ToString();
            if (assignedrole.Trim() == "Admin_User" || assignedrole.Trim() == "Administrator")
            {
                var list = _dapperrepo.GetAllPlaceRequisitionApproved(0, 0);
                return View(list);
            }
            else if (assignedrole.Trim() == "StoreKeeper(HeadOffice)")
            {
                var list = _dapperrepo.GetAllPlaceRequisitionApproved(branchid, 0);
                return View(list);
            }
            else
            {
                var list = _dapperrepo.GetAllPlaceRequisitionApprovedOfOwnBranch(branchid, userid);
                return View(list);
            }
        }

        [UserAuthorize(menuId: 32)]
        public ActionResult ViewApproved(int id)
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var assignedrole = Session["AssignRole"].ToString();
           
            IEnumerable<InRequisitionMessageVM> dispatchMenuItems;
            if (assignedrole.Trim() == "Admin_User" || assignedrole.Trim() == "Administrator")
            {
                dispatchMenuItems = _dapperrepo.GetAllPlaceRequisitionApproved(0, 0);
            }
            else if (assignedrole.Trim() == "StoreKeeper(HeadOffice)")
            {
                dispatchMenuItems = _dapperrepo.GetAllPlaceRequisitionApproved(branchid, 0);
            }
            else
            {
                dispatchMenuItems = _dapperrepo.GetAllPlaceRequisitionApprovedOfOwnBranch(branchid, userid);
            }

            if (!dispatchMenuItems.Any(r => r.id == id))
            {
                return RedirectToAction("Index", "Error");
            }

            var record = _unitofWork.InRequisitionMessage.GetById(id);
            if (record.Delivered_Date == null && record.Delivered_By == null)
            {
                record.inreqList = _unitofWork.InRequisition.GetItemById(id);
                record.Delivered_Date = DateTime.Now.Date;
                record.AssignedRole = assignedrole;
                return View(record);
            }

            TempData["Fail"] = "This Requisition has been Already Dispatched.";
            TempData["Title"] = "<strong>Error!</strong><br />";
            TempData["Icon"] = "fa fa-exclamation-circle";
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [UserAuthorize(menuId: 32)]
        public async Task<ActionResult> RejectRequisition(int reqmesid, string rejectmes)
        {
            var Item = _unitofWork.InRequisitionMessage.GetById(reqmesid);
            Item.rejected_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
            Item.rejected_date = DateTime.Now.Date;
            Item.status = "Rejected";
            Item.rejected_message = rejectmes.Trim();
            _unitofWork.InRequisitionMessage.Update(Item);

            var notificationdata = _unitofWork.InNotifications.GetByPOMId(reqmesid);
            if (notificationdata != null)
            {
                notificationdata.Status = true;
                _unitofWork.InNotifications.Update(notificationdata);
            }
            _unitofWork.Save();

            var senderemail = CodeService.GetOfficialEmail(Item.Requ_by);
            var fromuser = CodeService.GetEmployeeFullName(Item.Requ_by);
            var touser = CodeService.GetEmployeeFirstName(_unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name);
            if (senderemail != "")
            {
                await EmailService.SendMailAsync(senderemail, "Items rejected",
                EmailService.EmailMessage.RejectPlaceRequisition(touser, fromuser, rejectmes.Trim()));
            }
            return this.Json(new { success = true, mes = "success" });
        }

        [HttpPost]
        [UserAuthorize(menuId: 32)]
        public ActionResult GeneratePO(PurchaseOrderMessageVM pom)
        {
            if (pom.vendor_code != 0)
            {
                foreach (var itm1 in pom.poList.Where(x => x.productname != "" && x.rate > 0 && x.IsRowCheck == true))
                {
                    var serialstatus = CodeService.GetSerialStatusForProduct(itm1.productname.Trim());
                    var TempPurOtherId = CodeService.INRequisitionDetailOtherData(itm1.productname.Trim(), pom.temppurchaseid);
                    //  var data = _unitofWork.TempPurchaseOther.GetById(TempPurId);
                    if (serialstatus == true && TempPurOtherId == 0)
                    {
                        return Json(new { success = false, err = "Please Give Serial No. For Seralized Product.", JsonRequestBehavior.AllowGet });
                    }
                }
                var pomcount = pom.poList.Where(x => x.IsRowCheck == true).Count();
                if (pomcount > 0)
                {
                    using (TransactionScope Scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {

                        //purchaseordermessage insert
                        var compshortname = CodeService.GetCompanyShortName();
                        var fiscalyear = CodeService.GetFiscalYear();
                        var purchaseordermessageid = CodeService.GetPurchaseOrderMessageId();
                        pom.status = "Requested";
                        pom.order_no = compshortname + "-" + "PO" + "-" + fiscalyear + "-" + purchaseordermessageid;
                        pom.branch_id = Convert.ToInt32(Session["BranchId"]);
                        pom.department_id = Convert.ToInt32(Session["DepartmentId"]);
                        pom.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                        pom.created_date = DateTime.Now;
                        _unitofWork.PurchaseOrderMessage.Insert(pom);
                        _unitofWork.Save();

                        //purchaseorder insert
                        var po = new PurchaseOrderVM();
                        foreach (var itm in pom.poList.Where(x => x.productname != "" && x.rate > 0))
                        {
                            var pid = CodeService.GetProductIdFromProductName(itm.productname.Trim());
                            po.order_message_id = CodeService.GetPurchaseOrderMessageIdforpurchaseorder();
                            po.product_code = pid;
                            po.qty = itm.qty;
                            po.rate = itm.rate;
                            po.amount = itm.amount;
                            _unitofWork.PurchaseOrder.Insert(po);
                            _unitofWork.Save();
                        }

                        //PurchaseOrderMessageHistory insert
                        var OrdId = CodeService.GetPurchaseOrderMessageIdforpurchaseorder();
                        var pomh = new PurchaseOrderMessageHistoryVM();
                        pomh.Ord_id = OrdId;
                        pomh.Requ_date = pom.order_date;
                        pomh.from_user = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                        pomh.to_user = Convert.ToString(pom.forwarded_to);
                        _unitofWork.PurchaseOrderMessageHistory.Insert(pomh);
                        _unitofWork.Save();

                        foreach (var itm in pom.poList.Where(x => x.productname != "" && x.rate > 0))
                        {
                            var serialstatus = CodeService.GetSerialStatusForProduct(itm.productname.Trim());
                            var prodcode = CodeService.GetProductIdFromProductName(itm.productname.Trim());
                            var branchid = Convert.ToInt32(Session["BranchId"]);

                            //SerialProductStock insert
                            if (serialstatus == true)
                            {
                                var IRDOData = _unitofWork.InRequisitionDetailOther.GetInRequisitionDetailOtherByReqMesIdAndPId(pom.temppurchaseid, prodcode);
                                int snf = Convert.ToInt32(IRDOData.sn_from);
                                int snt = Convert.ToInt32(IRDOData.sn_to);
                                var spsdata = new SerialProductStockVM();
                                _dapperrepo.InsertSerialProductStockPurchaseVoucher(branchid, prodcode, OrdId, snf, snt);// in this case Purchaseid is OrderId
                                _dapperrepo.DeleteByReqMesId(pom.temppurchaseid, prodcode);
                            }
                        }

                        var imsn = new InNotificationsVM();
                        imsn.CreatedDate = DateTime.Now;
                        imsn.Subject = "P.O. Approval Request";
                        imsn.Forwardedby = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                        imsn.Forwardedto = pom.forwarded_to;
                        imsn.URL = "/ApprovePurchaseOrder/Index" + "/" + OrdId;
                        imsn.SpecialId = OrdId;
                        imsn.Status = false;
                        _unitofWork.InNotifications.Insert(imsn);
                        _unitofWork.Save();

                        Scope.Complete();
                        return Json(new { success = true, mes = "Successfully Requested PO.", JsonRequestBehavior.AllowGet });
                    }
                }
                else
                {
                    return Json(new { success = false, err = "Please Select At Least One Item for PO.", JsonRequestBehavior.AllowGet });
                }
            }
            else
            {
                return Json(new { success = false, err = "Please Select Vendor.", JsonRequestBehavior.AllowGet });
            }
        }
        public JsonResult GetDetailIdandPurIdFromInRequisitionDetailOther(int inreqmesid, string prodname)
        {
            var pid = CodeService.GetProductIdFromProductName(prodname.Trim());
            var data = _unitofWork.InRequisitionDetailOther.GetAllInRequisitionDetailOtherFromDetailIdandPurId(inreqmesid, pid).ToList();
            return Json(data);
        }
    }
}