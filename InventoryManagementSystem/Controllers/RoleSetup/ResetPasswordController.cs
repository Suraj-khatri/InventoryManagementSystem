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

namespace InventoryManagementSystem.Controllers.RoleSetup
{
    [UserAuthorize(menuId: 3)]
    public class ResetPasswordController : Controller
    {
        // GET: ResetPassword
        private IUnitOfWork _unitofWork;

        public ResetPasswordController()
        {
            _unitofWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            var data = new AdminVM();
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(AdminVM record)
        {
            if (string.IsNullOrEmpty(record.UserName))
            {
                ModelState.AddModelError("UserName", "UserName is Required !!");
                return View(record);
            }
            if (string.IsNullOrEmpty(record.UserPassword))
            {
                ModelState.AddModelError("UserPassword", "Password is Required !!");
                return View(record);
            }

            if (record.UserPassword.Contains(" "))
            {
                ModelState.AddModelError("UserPassword", "Password Contains Spaces. Please Avoid it !!");
                return View(record);
            }

            var user = _unitofWork.Admin.GetUserForResetPasword(record.UserName);

            if (user != null)
            {
                user.UserPassword = HashService.Hash(record.UserPassword);
                _unitofWork.Admin.Update(user);
                _unitofWork.Save();

                TempData["Success"] = "<p>Password successfully Reset for </p>" + record.UserName + "";
                TempData["Title"] = "<strong>Password Changed</strong><br />";
                TempData["Icon"] = "fa fa-lock fa-2x";

                return RedirectToAction("Index", "ResetPassword");
            }

            ModelState.AddModelError("UserName", "User Name does not Exist !!");
            return View(record);

        }
    }
}