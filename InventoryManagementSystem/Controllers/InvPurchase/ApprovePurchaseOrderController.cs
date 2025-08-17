using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{

    public class ApprovePurchaseOrderController : Controller
    {
        // GET: ApprovePurchaseOrder
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public ApprovePurchaseOrderController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }

        [UserAuthorize(menuId: 20)]
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var list = _unitofWork.PurchaseOrderMessage.GetAllRequested(userid, branchid);
            return View(list);
        }


        [UserAuthorize(menuId: 20)]

        public ActionResult Create(int id)
        {
            var list = _unitofWork.PurchaseOrderMessage.GetById(id);
            list.vendorname = CodeService.GetVendorNameFromPurchaseOrderMessage(id);
            list.poList = _unitofWork.PurchaseOrder.GetItemById(id);
            list.SubTotal = list.poList.Sum(x => x.amount);
            list.NetAmount = list.SubTotal + list.vat_amt;
            list.EmployeeList = _dropDownList.EmployeeList();
            list.IsVatablePO = list.vat_amt > 0 ? true : false;
            return View(list);
        }

        [HttpPost]
        [UserAuthorize(menuId: 20)]
        public ActionResult UpdateApproveOrder(PurchaseOrderMessageVM data)
        {
            try
            {
                var approvedby = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                var res = _dapperrepo.UpdatePurchaseOrderById(data.poList, data.id, approvedby, data.vat_amt);
                if (res)
                {
                    var mes = "Purchase Order Approved Successfully";
                    return Json(new { success = true, mes = mes });
                }
                else
                {
                    var mes = "Cannot approved. please try again later";
                    return Json(new { success = false, mes = mes });
                }
            }
            catch (Exception)
            {

                return Json(new { success = false, mes = "Error Occurred !!" });
            }

        }

        public ActionResult Update(int id, string approveorforward)
        {
            var Item = _unitofWork.PurchaseOrderMessage.GetById(id);
            var notificationdata = _unitofWork.InNotifications.GetByPOMId(id);
            //if (approveorforward == "A")
            //{
            //    Item.approved_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name; 
            //    Item.approved_date = DateTime.Now.Date;
            //    Item.status = "Approved";
            //    _unitofWork.PurchaseOrderMessage.Update(Item);
            //    if (notificationdata != null)
            //    {
            //        notificationdata.Status = true;
            //        _unitofWork.InNotifications.Update(notificationdata);
            //    }
            //    _unitofWork.Save();

            //    TempData["Success"] = "<p>Purchase Order :  Succesfully Approved</p>";
            //    TempData["Title"] = "<strong>Data Approved</strong> <br />";
            //    TempData["Icon"] = "fa fa-check";
            //}
            //else if (approveorforward == "F")
            //{
            //    Item.forwarded_to = 9;
            //    Item.forwarded_date = DateTime.Now.Date;
            //    Item.status = "Forwarded";
            //    _unitofWork.PurchaseOrderMessage.Update(Item);

            //    if (notificationdata != null)
            //    {
            //        notificationdata.Status = true;
            //        _unitofWork.InNotifications.Update(notificationdata);
            //    }
            //    _unitofWork.Save();

            //    TempData["Success"] = "<p>Purchase Order :  Succesfully Forwarded To</p>" + Item.forwarded_to;
            //    TempData["Title"] = "<strong>Data Forwarded</strong> <br />";
            //    TempData["Icon"] = "fa fa-check";
            //}

            if (approveorforward == "C")
            {
                Item.cancelled_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                Item.cancelled_date = DateTime.Now.Date;
                Item.status = "Rejected";
                _unitofWork.PurchaseOrderMessage.Update(Item);

                if (notificationdata != null)
                {
                    notificationdata.Status = true;
                    _unitofWork.InNotifications.Update(notificationdata);
                }
                _unitofWork.Save();

                TempData["Success"] = "<p>Purchase Order :  Succesfully Rejected by</p>" + Item.cancelled_by;
                TempData["Title"] = "<strong>Data Cancelled</strong> <br />";
                TempData["Icon"] = "fa fa-check";
            }
            return RedirectToAction("Index");
        }
    }
}