using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvMovement
{
    [UserAuthorize]
    public class DirectDispatchForNewBranchController : Controller
    {
        // GET: DirectDispatchForNewBranch
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public DirectDispatchForNewBranchController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId:1092)]
        public ActionResult Create()
        {
            var record = new InRequisitionMessageVM();
            var empid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var bidfromemp = CodeService.GetBranchIdFromEmployee(empid);
            record.BranchNameList = _dropDownList.BranchList();
            record.BranchNameList.Remove(record.BranchNameList.First());
            record.ProductNameList = _dropDownList.ProductList();
            record.EmployeeList = _dropDownList.EmployeeList();
            record.EmployeeList = _dropDownList.EmployeeListDirectDispatchForBranch(bidfromemp);
            record.Forwarded_To = 12;//
            return View(record);
        }
        public JsonResult GetNonPrintingProductNameForBranchList(int branchid)
        {
            var data = _dropDownList.NonPrintingProductNameFromInBranchAssignList(branchid).ToList();
            data.Insert(0, new SelectListItem { Text = "--Select--", Value = "--Select--" });
            return Json(data);
        }
        public JsonResult GetPrintingProductNameForBranchList(int branchid)
        {
            var data = _dropDownList.PrintingProductNameFromInBranchAssignList(branchid).ToList();
            data.Insert(0, new SelectListItem { Text = "--Select--", Value = "--Select--" });
            return Json(data);
        }
        public JsonResult GetNonPrintingInStaticTempDispatchData()
        {
            var data = _unitofWork.StaticTempDispatch.GetAllNonPrintingItem();
            return Json(data);
        }
        public JsonResult GetPrintingInStaticTempDispatchData()
        {
            var data = _unitofWork.StaticTempDispatch.GetAllPrintingItem();
            return Json(data);
        }
        public JsonResult GetStockInHandForProductId(int pid)
        {
            var data = CodeService.GetStockInHandfromPId(pid, 12);//12==999(Corporate)
            return Json(data);
        }
        public ActionResult DirectPurchaseForNewBranch()
        {
            var record = new BillInfoVM();
            record.VendorList = _dropDownList.VendorList();
            record.VendorList.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
            record.bill_date = DateTime.Now.Date;
            return View(record);
        }
        public JsonResult RemoveProduct(int pId)
        {
            try
            {
                _dapperrepo.DeleteInStaticDispatchByProductId(pId);
                return Json(new { success = true, message = "Product removed successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error removing product: " + ex.Message });
            }
        }
    }
}