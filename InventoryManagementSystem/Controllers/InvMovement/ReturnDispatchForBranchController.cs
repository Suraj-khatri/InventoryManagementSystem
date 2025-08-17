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
    public class ReturnDispatchForBranchController : Controller
    {
        // GET: ReturnDispatchForBranch
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public ReturnDispatchForBranchController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId:1090)]
        public ActionResult Create()
        {
            _dapperrepo.DeleteTempReturnProduct();
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
        public JsonResult GetEmployeeForBranchList(int branchid)
        {
            var data = _dropDownList.EmployeeListBranchWise(branchid);
            return Json(data);

        }
        public JsonResult GetProductNameForBranchList(int branchid)
        {
            var data = _dropDownList.ProductNameFromInBranchAssignList(branchid).ToList();
            data.Insert(0, new SelectListItem { Text = "--Select--", Value = "--Select--" });
            return Json(data);
        }
        public ActionResult GetSerialStatusForProduct(int pid)
        {
            var serialstatus = CodeService.GetSerialStatusForProductById(pid);
            return Json(serialstatus);
        }
        public JsonResult GetTempReturnProduct(int pid)
        {
            var data = _unitofWork.TempReturnProduct.GetByProductId(pid);
            if (data!=null)
            {
            return Json(data);
            }
            return Json("");
        }
        public void RemoveTempReturn(int id)
        {
            _unitofWork.TempReturnProduct.Delete(id);
            _unitofWork.Save();
        }
    }
}