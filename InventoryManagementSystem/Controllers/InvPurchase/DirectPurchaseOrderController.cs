using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
 
    public class DirectPurchaseOrderController : Controller
    {
        // GET: DirectPurchaseOrder
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;

        public DirectPurchaseOrderController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 25)]
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32(Session["BranchId"]);
            var assignedrole = Session["AssignRole"].ToString();

            List<BillInfoVM> list = new List<BillInfoVM>();

            if (assignedrole == "StoreKeeper(HeadOffice)" || assignedrole == "Admin_User")
            {
                list = _dapperrepo.GetAllDirectPurchaseRequisition();
            }

            return View(list);
        }

        [UserAuthorize(menuId: 25)]
        public ActionResult Create()
        {
            var branchId = Convert.ToInt32(Session["BranchId"]);
            var record = new BillInfoVM();
            record.ProductList = _dropDownList.ProductList();
            record.EmployeeList = _dropDownList.BranchWiseSupervisorandAdminsList(branchId);
            record.bill_date = DateTime.Now.Date;
           record.branch_id = branchId;
            return View(record);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(BillInfoVM data)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            data.SubTotal =(Convert.ToDecimal(data.bill_amount) * 100)/113;
        //            data.vat_amt =(Convert.ToDecimal(0.13) * data.SubTotal);
        //            data.bill_type = "p";
        //            data.entered_date = DateTime.Now;
        //            data.entered_by = "B";
        //            _unitofWork.BillInfo.Insert(data);
        //            _unitofWork.Save();

        //            var inpurchase = new InPurchaseVM();

        //            TempData["Success"] = "<p>Purchase Voucher :  Succesfully Created</p>";
        //            TempData["Title"] = "<strong>Data Created</strong> <br />";
        //            TempData["Icon"] = "fa fa-check";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return View(data);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        TempData["Fail"] = "Failed . Please try again later.";
        //        TempData["Title"] = " <strong>Error</strong> <br />";
        //        TempData["Icon"] = "fa fa-exclamation-circle";
        //        ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
        //        return View(data);
        //    }
        //}
        public JsonResult GetUnitForProduct(int prodname)
        {
            var data = CodeService.GetUnitForProductName(prodname);
            return Json(data);
        }
    }
}