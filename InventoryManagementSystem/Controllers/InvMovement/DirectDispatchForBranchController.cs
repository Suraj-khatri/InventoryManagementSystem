using InventoryManagementSystem.Domain;
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
    public class DirectDispatchForBranchController : Controller
    {
        // GET: DirectDispatchForBranch
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        public DirectDispatchForBranchController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        [UserAuthorize(menuId:28)]
        public ActionResult Create()
        {
            var record = new InRequisitionMessageVM();
            var empid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var bidfromemp = CodeService.GetBranchIdFromEmployee(empid);
            var assignedrole = Session["AssignRole"].ToString();
            var branchId = Session["BranchId"].ToString();
            ViewBag.AssignedRole = assignedrole;
            ViewBag.BranchId = branchId;
        
            if (assignedrole.Trim() == "Supervisor" || assignedrole.Trim() == "StoreKeeper(Branch)")
            {
                record.EmployeeList = _dropDownList.EmployeeListDirectDispatchForBranch(bidfromemp);
                record.BranchNameList = _dropDownList.BranchListForDirectDispatchForBranch(bidfromemp);
            }
            else if (assignedrole.Trim() == "StoreKeeper(HeadOffice)" || assignedrole.Trim() == "Admin_User" || assignedrole.Trim() == "Administrator")
            {
                record.BranchNameList = _dropDownList.BranchList();
                record.BranchNameList.Remove(record.BranchNameList.First());
                record.EmployeeList = _dropDownList.EmployeeList();
                record.branch_id = Convert.ToInt32(Session["BranchId"]);
                record.EmployeeList = _dropDownList.EmployeeListDirectDispatchForBranch(bidfromemp);
            }
            else
            {
                TempData["Fail"] = "You do not have access to this page. Please contact your Administration.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index", "Home");
            }
            record.ProductNameList = _dropDownList.ProductList();
            record.PriorityList = _dropDownList.PriorityList();
            record.DepartmentList = _dropDownList.DepartmentList();
            return View(record);
        }
        public JsonResult GetDepartmentForBranchList(int branchid)
        {
            var data = _dropDownList.DepartmentFromBranchList(branchid).ToList();
            return Json(data);
        }
        public JsonResult GetEmployeeForBranchList(int branchid)
        {
            var assignedrole = Session["AssignRole"].ToString();
            if (assignedrole.Trim() == "StoreKeeper(HeadOffice)" || assignedrole.Trim() == "Admin_User" || assignedrole.Trim() == "Administrator")
            {
                var data = _dropDownList.EmployeeListDirectDispatchForBranch(branchid);
                return Json(data);
            }
            else
            {
                var empid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                var data = _dropDownList.EmployeeListBranchWiseSuperVisor(branchid, empid);
                return Json(data);
            }
        }
        public JsonResult GetEmployeeNameForBranchAndDepartmentList(int branchid,int deptid)
        {
            var data = _dropDownList.GetEmployeeNameForBranchAndDepartmentList(branchid,deptid).ToList();
            return Json(data);
        }
        public JsonResult GetProductNameForBranchList(int branchid)
        {
            var data = _dropDownList.ProductNameFromInBranchAssignList(branchid).ToList();
            data.Insert(0, new SelectListItem { Text = "--Select--", Value = "--Select--" });
            return Json(data);
        }
        [HttpPost]
        public void RemoveInTempRequisition(int id)
        {
            try
            {
                _unitofWork.InRequisitionDetailOther.Delete(id);
                _unitofWork.InTempRequisition.Delete(id);
                _unitofWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}