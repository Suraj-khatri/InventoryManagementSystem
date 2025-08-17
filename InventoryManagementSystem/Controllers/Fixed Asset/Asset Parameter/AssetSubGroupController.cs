using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.Fixed_Asset.Asset_Parameter
{
    [UserAuthorize]
    public class AssetSubGroupController : Controller
    {
        // GET: AssetProduct
        // GET: InProduct
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public AssetSubGroupController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult IndexDepAsset(int id)
        {
            bool isactive = CodeService.GetAssetItemStatus(id);
            if (isactive == true)
            {
                ViewBag.ItemName = CodeService.GetAssetItemName(id);
                ViewBag.ItemId = CodeService.GetAssetItemId(id);
                var list = _unitofWork.AssetItem.GetByParentId(id);
                return View(list);
            }
            else
            {
                TempData["Fail"] = "Failed To View Product. Asset Group Is not Active.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index", "AssetGroupAndTypeSetup");
            }

        }
        public ActionResult IndexNonDepAsset(int id)
        {
            bool isactive = CodeService.GetAssetItemStatus(id);
            if (isactive == true)
            {
                ViewBag.ItemName = CodeService.GetAssetItemName(id);
                ViewBag.ItemId = CodeService.GetAssetItemId(id);
                var list = _unitofWork.AssetItem.GetByParentId(id);
                return View(list);
            }
            else
            {
                TempData["Fail"] = "Failed To View Product. Product Group Is not Active.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index", "AssetGroupAndTypeSetup");
            }
        }
        public ActionResult CreateSubGroupDepAsset(int id)
        {
            var record = new AssetItemVM();
            ViewBag.ParentGroup = CodeService.GetAssetItemName(id);
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubGroupDepAsset(AssetItemVM data)
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
                            data.parent_id = data.id;
                            data.Is_Active = true;
                            _unitofWork.AssetItem.Insert(data);
                            _unitofWork.Save();
                            TempData["Success"] = "Asset Group Successfully added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("IndexDepAsset", new { id = data.id });
                        }
                        else
                        {
                            ViewBag.ParentGroup = CodeService.GetAssetItemName(data.id);
                            ModelState.AddModelError("item_desc", "Description already exists !.");
                            return View("CreateSubGroupDepAsset", data);
                        }
                    }
                    else
                    {
                        ViewBag.ParentGroup = CodeService.GetAssetItemName(data.id);
                        ModelState.AddModelError("item_name", "Group Name already exists !.");
                        return View("CreateSubGroupDepAsset", data);
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
        public ActionResult CreateSubGroupNonDepAsset(int id)
        {
            var record = new AssetItemVM();
            ViewBag.ParentGroup = CodeService.GetAssetItemName(id);
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubGroupNonDepAsset(AssetItemVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (!_unitofWork.AssetItem.IsGroupNameExists(data))
                    //{
                    //    if (!_unitofWork.AssetItem.IsDescExists(data))
                    //    {
                            data.created_date = DateTime.Now;
                            data.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.is_product = false;
                            data.parent_id = data.id;
                            data.Is_Active = true;
                            _unitofWork.AssetItem.Insert(data);
                            _unitofWork.Save();
                            TempData["Success"] = "Asset Group Successfully added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("IndexNonDepAsset");
                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("item_desc", "Description already exists !.");
                    //        return View("CreateSubGroupNonDepAsset", data);
                    //    }
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("item_name", "Group Name already exists !.");
                    //    return View("CreateSubGroupNonDepAsset", data);
                    //}
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
        public ActionResult EditSubGroupDepAsset(int Id)
        {
            ViewBag.ParentGroup = CodeService.GetAssetItemNameByParentId(Id);
            var data = _unitofWork.AssetItem.GetById(Id);
            return View("CreateSubGroupDepAsset", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubGroupDepAsset(AssetItemVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (!_unitofWork.AssetItem.IsGroupNameExists(data))
                    //{
                    //    if (!_unitofWork.AssetItem.IsDescExists(data))
                    //    {
                            data.modified_date = DateTime.Now;
                            data.is_product = false;
                            data.modified_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.parent_id =CodeService.GetAssetItemParentId(data.id);
                            data.Is_Active = true;
                            _unitofWork.AssetItem.Update(data);
                            _unitofWork.Save();
                            TempData["Success"] = "<p>Product Group :  Succesfully Updated</p>";
                            TempData["Title"] = "<strong>Data Updated</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("item_desc", "Description already exists !.");
                    //        return View("Create", data);
                    //    }
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("item_name", "Group Name already exists !.");
                    //    return View("Create", data);
                    //}
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
        public ActionResult EditSubGroupNonDepAsset(int Id)
        {
            ViewBag.ParentGroup = CodeService.GetAssetItemNameByParentId(Id);
            var data = _unitofWork.AssetItem.GetById(Id);
            return View("CreateSubGroupNonDepAsset", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubGroupNonDepAsset(AssetItemVM data)
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
                            data.parent_id = CodeService.GetAssetItemParentId(data.id);
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
    }
}