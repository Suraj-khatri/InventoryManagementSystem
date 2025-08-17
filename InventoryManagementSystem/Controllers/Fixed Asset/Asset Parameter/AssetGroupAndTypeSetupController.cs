using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.Fixed_Asset.Asset_Parameter
{
    [UserAuthorize]
    public class AssetGroupAndTypeSetupController : Controller
    {
        // GET: AssetGroupAndTypeSetup
        private IUnitOfWork _unitofWork;
        public AssetGroupAndTypeSetupController()
        {
            _unitofWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            var list = _unitofWork.AssetItem.GetAll();
            return View(list);
        }
        public ActionResult CreateDepAsset()
        {
            ViewBag.ParentGroup = "Depreciable Asset";
            var record = new AssetItemVM();
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDepAsset(AssetItemVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.AssetItem.IsGroupNameExists(data))
                    {
                        if (!_unitofWork.AssetItem.IsDescExists(data))
                        {
                            data.created_date = DateTime.Now;
                            data.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.is_product = false;
                            data.parent_id = 1;
                            data.Is_Active = true;
                            _unitofWork.AssetItem.Insert(data);
                            _unitofWork.Save();
                            TempData["Success"] = "Asset Group Successfully added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("item_desc", "Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("item_name", "Group Name already exists !.");
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
        public ActionResult CreateNonDepAsset()
        {
            ViewBag.ParentGroup = "Non Depreciable Asset";
            var record = new AssetItemVM();
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNonDepAsset(AssetItemVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.AssetItem.IsGroupNameExists(data))
                    {
                        if (!_unitofWork.AssetItem.IsDescExists(data))
                        {
                            data.created_date = DateTime.Now;
                            data.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.is_product = false;
                            data.parent_id = 2;
                            data.Is_Active = true;
                            _unitofWork.AssetItem.Insert(data);
                            _unitofWork.Save();
                            TempData["Success"] = "Asset Group Successfully added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("item_desc", "Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("item_name", "Group Name already exists !.");
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
        public ActionResult EditDepAsset(int Id)
        {
            ViewBag.ParentGroup = "Depreciable Asset";
            var data = _unitofWork.AssetItem.GetById(Id);
            return View("CreateDepAsset", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepAsset(AssetItemVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.AssetItem.IsGroupNameExists(data))
                    {
                        if (!_unitofWork.AssetItem.IsDescExists(data))
                        {
                            data.modified_date = DateTime.Now;
                            data.is_product = false;
                            data.modified_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.parent_id = 1;
                            data.Is_Active = true;
                            _unitofWork.AssetItem.Update(data);
                            _unitofWork.Save();
                            TempData["Success"] = "<p>Product Group :  Succesfully Updated</p>";
                            TempData["Title"] = "<strong>Data Updated</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("item_desc", "Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("item_name", "Group Name already exists !.");
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
        public ActionResult EditNonDepAsset(int Id)
        {
            ViewBag.ParentGroup = "Non Depreciable Asset";
            var data = _unitofWork.AssetItem.GetById(Id);
            return View("CreateNonDepAsset", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNonDepAsset(AssetItemVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.AssetItem.IsGroupNameExists(data))
                    {
                        if (!_unitofWork.AssetItem.IsDescExists(data))
                        {
                            data.modified_date = DateTime.Now;
                            data.is_product = false;
                            data.modified_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.parent_id = 2;
                            data.Is_Active = true;
                            _unitofWork.AssetItem.Update(data);
                            _unitofWork.Save();
                            TempData["Success"] = "<p>Product Group :  Succesfully Updated</p>";
                            TempData["Title"] = "<strong>Data Updated</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("item_desc", "Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("item_name", "Group Name already exists !.");
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
                _unitofWork.AssetItem.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Asset Group : " + _unitofWork.AssetItem.GetById(id).item_name + "</p>";
                TempData["Title"] = "<strong>Product Group Removed</strong> <br />";
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