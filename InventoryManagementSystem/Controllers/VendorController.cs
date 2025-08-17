using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{

    public class VendorController : Controller
    {
        // GET: Vendor
        private IUnitOfWork _unitofWork;
        public VendorController()
        {
            _unitofWork = new UnitOfWork();
        }
        [UserAuthorize(menuId: 13)]
        public ActionResult Index()
        {
            var assignedrole = Session["AssignRole"].ToString();
            var record = new CustomerVM();
            record.CustomerVMList = _unitofWork.InVendor.GetAll();
            record.assignedrole = assignedrole;
            return View(record);
        }
        [UserAuthorize(menuId: 13)]
        public ActionResult Create()
        {
            var record = new CustomerVM();
            record.IsActive = true;
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVM data)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InVendor.IsVendorNameExists(data))
                    {
                        data.CreatedDate = DateTime.Now;
                        data.CreatedBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                        data.CustomerCode = CodeService.GenerateVendorCode();
                        _unitofWork.InVendor.Insert(data);
                        _unitofWork.Save();
                        TempData["Success"] = "Vendor Successfully added";
                        TempData["Title"] = "<strong>Data Added</strong> <br />";
                        TempData["Icon"] = "fa fa-check";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("CustomerName", "Vendor Name already exists !.");
                        return View("Create", data);
                    }

                }
                else
                {
                    return View(data);
                }

            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                TempData["Fail"] = "Failed to Add Data. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
                return View(data);
            }
        }
        [UserAuthorize(menuId: 13)]
        public ActionResult Edit(int Id)
        {
            var data = _unitofWork.InVendor.GetById(Id);
            return View("Create", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InVendor.IsVendorNameExists(data))
                    {
                        data.ModifiedDate = DateTime.Now;
                        data.ModifiedBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                        data.CustomerCode = CodeService.GetVendorCode(data.ID);
                        _unitofWork.InVendor.Update(data);
                        _unitofWork.Save();
                        TempData["Success"] = "Vendor Successfully Updated";
                        TempData["Title"] = "<strong>Data Updated</strong> <br />";
                        TempData["Icon"] = "fa fa-check";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("CustomerName", "Vendor Name already exists !.");
                        return View("Create", data);
                    }
                }
                else
                {
                    return View(data);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                //todo not deleted
                _unitofWork.InVendor.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Vendor : " + _unitofWork.InVendor.GetById(id).CustomerName + "</p>";
                TempData["Title"] = "<strong>Vendor Removed</strong> <br />";
                TempData["Icon"] = "fa fa-check";
                return RedirectToAction("Index");
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                TempData["Fail"] = "Failed to remove. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
            }
            return RedirectToAction("Index");
        }
    }
}