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
    public class AssetProductController : Controller
    {
        // GET: AssetProduct
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public AssetProductController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Index(int id)
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
        public ActionResult Create(int id)
        {
            var record = new AssetProductVM();
            bool isactive = CodeService.GetAssetItemStatus(id);
            if (isactive == true)
            {
                ViewBag.ItemName = CodeService.GetAssetItemName(id);
                record.Is_Active = true;
                return View(record);
            }
            else
            {
                TempData["Fail"] = "Failed To Add Product. Asset Group Is not Active.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index", "AssetGroupAndTypeSetup");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssetProductVM data)
        {
            try
            {
                ViewBag.ItemName = CodeService.GetAssetItemName(data.id);
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.AssetProduct.IsAssetNameExists(data))
                    {
                        if (!_unitofWork.AssetProduct.IsAssetDescExists(data))
                        {
                            //InItem added
                            AssetItemVM aitem = new AssetItemVM();
                            aitem.item_name = data.porduct_code;
                            aitem.item_desc = data.product_desc;
                            aitem.created_date = DateTime.Now;
                            aitem.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            aitem.is_product = true;
                            aitem.Is_Active = data.Is_Active;
                            aitem.parent_id = data.id;
                            _unitofWork.AssetItem.Insert(aitem);
                            _unitofWork.Save();

                            //Inproduct added
                            data.id = 0;
                            data.created_date = DateTime.Now;
                            data.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.item_id = CodeService.GetAssetItemId();
                            data.modified_date = DateTime.Now;
                            data.Is_Active = true;
                            _unitofWork.AssetProduct.Insert(data);
                            _unitofWork.Save();

                            TempData["Success"] = "Product Successfully added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index", new { id = aitem.parent_id });
                        }
                        else
                        {
                            data.id = 0;
                            ModelState.AddModelError("product_desc", "Product Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        data.id = 0;
                        ModelState.AddModelError("porduct_code", "Product Name already exists !.");
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
        public ActionResult Edit(int Id)
        {
            var data = _unitofWork.AssetProduct.GetById(Id);
            ViewBag.ItemName = CodeService.GetAssetItemNameByParentId(data.item_id);
            return View("Create", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssetProductVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.AssetProduct.IsEditAssetNameExists(data))
                    {
                        if (!_unitofWork.AssetProduct.IsEditAssetDescExists(data))
                        {
                            //InItem updated
                            data.AssetItems = _unitofWork.AssetItem.GetById(data.item_id);
                            data.AssetItems.item_name = data.porduct_code;
                            data.AssetItems.item_desc = data.product_desc;
                            data.AssetItems.modified_date = DateTime.Now;
                            data.AssetItems.modified_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.AssetItems.is_product = true;
                            data.AssetItems.Is_Active = true;
                            data.AssetItems.parent_id = CodeService.GetAssetItemParentId(data.id);
                            _unitofWork.AssetItem.Update(data.AssetItems);
                            _unitofWork.Save();

                            //Inproduct updated
                            data.modified_date = DateTime.Now;
                            data.modified_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.modified_date = DateTime.Now;
                            data.Is_Active = true;
                            data.id = _unitofWork.AssetProduct.GetByItemId(data.item_id).id;
                            _unitofWork.AssetProduct.Update(data);

                            _unitofWork.Save();

                            TempData["Success"] = "<p>Product :  Succesfully Updated</p>";
                            TempData["Title"] = "<strong>Data Updated</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index", new { id = data.AssetItems.parent_id });
                        }
                        else
                        {
                            ModelState.AddModelError("product_desc", "Product Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("porduct_code", "Product Name already exists !.");
                        return View("Create", data);
                    }
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
            return View("Create", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int parentid = CodeService.GetAssetItemParentId(id);
            try
            {
                int itemid = CodeService.GetAssetProductId(id);
                _unitofWork.AssetProduct.Delete(itemid);
                _unitofWork.AssetItem.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Product : " + _unitofWork.AssetProduct.GetByItemId(id).porduct_code + "</p>";
                TempData["Title"] = "<strong>Poduct Removed</strong> <br />";
                TempData["Icon"] = "fa fa-check";
                return RedirectToAction("Index", new { id = parentid });
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                TempData["Fail"] = "Failed to remove. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
            }
            return RedirectToAction("Index", new { id = parentid });
        }
    }
}