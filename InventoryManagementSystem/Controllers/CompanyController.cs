using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{

    public class CompanyController : Controller
    {
        // GET: Company
        private IUnitOfWork _unitofWork;
        public CompanyController()
        {
            _unitofWork = new UnitOfWork();
        }
        [UserAuthorize(menuId: 7)]
        public ActionResult Index()
        {
            return View(_unitofWork.ICompany.GetAll());
        }
        [UserAuthorize(menuId: 7)]
        public ActionResult Edit(int id)
        {
            var data = _unitofWork.ICompany.GetById(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitofWork.ICompany.Update(data);
                    _unitofWork.Save();
                    TempData["Success"] = "<p>Company :  Succesfully Updated</p>";
                    TempData["Title"] = "<strong>Data Updated</strong> <br />";
                    TempData["Icon"] = "fa fa-check";
                    return RedirectToAction("Index");
                }
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                TempData["Fail"] = "Failed to Update Data. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");

            }
            return View(data);
        }
    }
}