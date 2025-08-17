using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.EmployeeManagement
{
    
    public class SuperVisorAssignmentController : Controller
    {
        // GET: SuperVisorAssignment
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public SuperVisorAssignmentController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        public ActionResult Index(int Id)
        {
            var data = new SuperVisorAssignmentVM();
            ViewBag.EmployeeName = CodeService.GetEmployeeFullName(Id);
            var branchid = CodeService.GetBranchIdFromEmployee(Id);
            data.SuperVisorAssignmentVMList = _unitofWork.SuperVisorAssignment.GetByEmpIdandBranchId(Id,branchid);
            data.EmployeeList = _dropDownList.EmployeeListBranchWiseSuperVisor(branchid,Id);
            data.BranchNameList = _dropDownList.BranchList();
            data.BranchNameList.Remove(data.BranchNameList.First());
            data.BRANCH = branchid;
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(SuperVisorAssignmentVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (!_unitofWork.SuperVisorAssignment.IsEmployeeAssigned(data))
                    {
                        data.record_status = "y";
                        data.SUPERVISOR_TYPE = "s";
                        data.CREATED_BY = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                        data.CREATED_DATE = DateTime.Now;
                        _unitofWork.SuperVisorAssignment.Insert(data);
                        _unitofWork.Save();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Fail"] = "This Supervisor already assigned for this Employee !.";
                        TempData["Title"] = " <strong>Error</strong> <br />";
                        TempData["Icon"] = "fa fa-exclamation-circle";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {


                TempData["Fail"] = "Failed to Add Data. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var EmpId = CodeService.GetEmpIdFromSuperVisorAssignment(id);
            try
            {
                //todo not deleted
                _dapperrepo.DeleteSupervisorAssignmentId(id);
                TempData["Success"] = "<p>Successfully removed Supervisor Assigned</p>";
                TempData["Title"] = "<strong>Supervisor Removed</strong> <br />";
                TempData["Icon"] = "fa fa-check";
                return RedirectToAction("Index",new { Id = EmpId });
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                TempData["Fail"] = "Failed to remove. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
            }
            return RedirectToAction("Index", new { Id = EmpId });
        }
    }
}