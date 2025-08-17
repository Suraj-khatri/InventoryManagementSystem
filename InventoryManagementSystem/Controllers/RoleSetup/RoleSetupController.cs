using InventoryManagementSystem.Domain.RoleSetupVM;
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
    [UserAuthorize(menuId: 5)]
    public class RoleSetupController : Controller
    {
        // GET: RoleSetup
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public RoleSetupController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Manage()
        {
            var rolesetup = new RoleDetailsVM();
            rolesetup.UserFunctionVMList = _unitofWork.UserFunction.GetAll();
            rolesetup.UserRoleList = _dropDownList.UserRoleList();
            rolesetup.UserRoleList.Remove(rolesetup.UserRoleList.First());
            rolesetup.RoleDetailsVMList = _unitofWork.RoleDetails.GetAll();
            return View(rolesetup);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Manage(List<InsertRolesVM> data)
        {
            if (data==null)
            {
                return RedirectToAction("Manage");
            }
            var roleid = data.Select(x => x.RoleId).FirstOrDefault();
            RoleDetailsVM rd = new RoleDetailsVM();
            try
            {
                if (ModelState.IsValid)
                {
                    _unitofWork.RoleDetails.Delete(roleid);
                    _unitofWork.Save();

                    //for parent id insert
                    foreach (var item in data.Where(x => x.ParentId > 0))
                    {
                        rd.role_id = item.RoleId;
                        rd.function_id =Convert.ToInt32(item.ParentId);
                        rd.IsActive = true;
                        _unitofWork.RoleDetails.Insert(rd);
                        _unitofWork.Save();
                    }
                    //for child id insert  
                    foreach (var item in data.Where(x => x.IsActive == true))
                    {
                        rd.role_id = item.RoleId;
                        rd.function_id = item.FunctionId;
                        rd.IsActive = true;
                        _unitofWork.RoleDetails.Insert(rd);
                        _unitofWork.Save();
                        var child = _unitofWork.UserFunction.GetByFunId(item.FunctionId);
                        foreach (var item1 in child)
                        {
                            rd.role_id = item.RoleId;
                            rd.function_id = item1.sno;
                            rd.IsActive = true;
                            _unitofWork.RoleDetails.Insert(rd);
                            _unitofWork.Save();
                        }

                    }
                }
                return RedirectToAction("Manage");
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                TempData["Fail"] = "Failed to Assign Role. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
                return View(data);
            }
        }

        public JsonResult AssignedSno(int roleId)
        {
            return Json(_unitofWork.RoleDetails.GetByRoleId(roleId), JsonRequestBehavior.AllowGet);
        }
    }
}