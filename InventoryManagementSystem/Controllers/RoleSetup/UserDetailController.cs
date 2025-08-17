using InventoryManagementSystem.Domain.RoleSetupVM;
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

namespace InventoryManagementSystem.Controllers.RoleSetup
{
    [UserAuthorize(menuId: 3)]
    public class UserDetailController : Controller
    {
        // GET: UserDetail
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public UserDetailController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        public ActionResult Index()
        {
            try
            {
                var list = new UserRoleVM();
                list.UserRoleList = _dropDownList.UserRoleList();
                list.BranchList = _dropDownList.BranchList();
                list.DepartmentList = _dropDownList.DepartmentList();
                list.UserRoeVMList = _dapperrepo.GetAllUserRole(0, 0, 0) ?? new List<UserRoleVM>();
                return View(list);
            }
            catch (Exception ex)
            {

                throw new Exception (ex.Message);
            }
      
        }

        [HttpPost]
        public ActionResult Index(UserRoleVM list)
        {
            try
            {
                list.UserRoleList = _dropDownList.UserRoleList();
                list.BranchList = _dropDownList.BranchList();
                list.DepartmentList = _dropDownList.DepartmentList();
                list.UserRoeVMList = _dapperrepo.GetAllUserRole(list.BranchId, list.role_id, (int?)list.Department_id ?? 0);
                return View(list);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
        public ActionResult AsignRole(string assignrole, int Id, string UserName, string remarks)
        {
            UserRoleVM ur = new UserRoleVM();
            ur.UserRoleList = _dropDownList.UserRoleList();
            ur.UserRoleList.Remove(ur.UserRoleList.First());
            ur.AssignedRole = assignrole;
            ur.row_id = Id;
            ur.UserName = UserName;
            ur.Remarks = remarks;
            return View(ur);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignRole(UserRoleVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var existingUserRole = _unitofWork.UserRole.GetById(data.row_id);

                    if (existingUserRole != null)
                    {
                        existingUserRole.role_id = data.role_id;
                        existingUserRole.Remarks = data.Remarks;
                        existingUserRole.user_id = CodeService.GetUserIdFromUserRole(data.row_id);

                        _unitofWork.UserRole.Update(existingUserRole);
                        _unitofWork.Save();

                        TempData["Success"] = "Role Successfully Assigned";
                        TempData["Title"] = "<strong>Role Assigned</strong> <br />";
                        TempData["Icon"] = "fa fa-check";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Fail"] = "User Role not found.";
                        return View(data);
                    }
                }
                else
                {

                    return View(data);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                TempData["Fail"] = "Failed to Assign Role. Please try again later.";
                TempData["Title"] = "<strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError( "Cannot process the request at the moment. Please try again later.", ex.InnerException.Message);

                // Return the view with the data to display error messages
                return View(data);
            }
        }

        public JsonResult GetRemarks(int id)
        {
            var userRole = _unitofWork.UserRole.GetById(id);
            if (userRole != null)
            {
                return Json(new { Remarks = userRole.Remarks }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Remarks = string.Empty }, JsonRequestBehavior.AllowGet);
        }


    }
}