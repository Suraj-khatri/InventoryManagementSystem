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
    public class AssetBranchAssignController : Controller
    {
        // GET: AssetBranchAssign
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public AssetBranchAssignController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Create(int id)
        {
            bool isactive = CodeService.GetAssetItemStatus(id);
            int parentid = CodeService.GetAssetItemParentId(id);
            if (isactive == true)
            {
                var record = new AssetBranchVM();
                record.AssetProduct = _unitofWork.AssetProduct.GetById(id);
                record.BranchList = _dropDownList.BranchList();
                record.AssetBranchVMList = _unitofWork.AssetBranch.GetAllByProductId(record.AssetProduct.id);
                record.IS_ACTIVE = true;
                return View(record);
            }
            else
            {
                TempData["Fail"] = "Failed To Assign Branch. Product Is not Active.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index", "InProduct", new { id = parentid });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssetBranchVM data)
        {
            try
            {
                data.AssetProduct = _unitofWork.AssetProduct.GetById(data.ID);
                data.BranchList = _dropDownList.BranchList();
                data.AssetBranchVMList = _unitofWork.AssetBranch.GetAllByProductId(data.AssetProduct.id);
                int prodid = CodeService.GetAssetProductId(data.ID);
                //data.ID = 0;
                if (ModelState.IsValid)
                {
                    if (data.BRANCH_ID > 0)
                    {
                        if (!_unitofWork.AssetBranch.IsBranchAssigned(prodid, data.BRANCH_ID))
                        {
                            data.PRODUCT_ID = prodid;
                            data.ASSET_NEXT_NUM = "1";
                            _unitofWork.AssetBranch.Insert(data);
                            _unitofWork.Save();
                            TempData["Success"] = "Branch : Successfully Assigned for this Product";
                            TempData["Title"] = "<strong>Data Assigned</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            data.ID = 0;
                            ModelState.AddModelError("BRANCH_ID", "Branch already assigned for this product.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        foreach (var itm in data.BranchList)
                        {
                            if (itm.Value != "0")
                            {
                                if (!_unitofWork.AssetBranch.IsBranchAssigned(prodid, Int32.Parse(itm.Value)))
                                {
                                    data.PRODUCT_ID = prodid;
                                    data.BRANCH_ID = Int32.Parse(itm.Value);
                                    data.ASSET_NEXT_NUM = "1";
                                    _unitofWork.AssetBranch.Insert(data);
                                    _unitofWork.Save();
                                }
                                //else
                                //{
                                //    ModelState.AddModelError("BRANCH_ID", "Branch already assigned for some or all product.");
                                //    return View("Create", data);
                                //}
                            }
                        }
                        TempData["Success"] = "Branch : Successfully Assigned for all the Product";
                        TempData["Title"] = "<strong>Data Assigned</strong> <br />";
                        TempData["Icon"] = "fa fa-check";
                        return RedirectToAction("Create");
                    }
                }
                else
                {
                    return View("Create", data);
                }
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                TempData["Fail"] = "Failed to Assign Data. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
                return View("Create", data);
            }
        }
        public ActionResult Edit(int id)
        {
            int itemid = CodeService.GetAssetItemIdByBranchAssignId(id);
            var data = _unitofWork.AssetBranch.GetById(id);
            data.AssetProduct = _unitofWork.AssetProduct.GetById(itemid);
            data.AssetBranchVMList = _unitofWork.AssetBranch.GetAllByProductId(data.AssetProduct.id);
            // data.INPRODUCTs = _unitofWork.InProduct.GetById(id);
            data.BranchList = _dropDownList.BranchList();
            return View("Create", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AssetBranchVM data)
        {
            int itemid = CodeService.GetAssetItemIdByBranchAssignId(data.ID);
            try
            {
                data.BranchList = _dropDownList.BranchList();
                data.AssetProduct = _unitofWork.AssetProduct.GetById(itemid);
                data.AssetBranchVMList = _unitofWork.AssetBranch.GetAllByProductId(data.AssetProduct.id);
                int prodid = CodeService.GetAssetProductId(itemid);
                if (ModelState.IsValid)
                {
                    data.BRANCH_ID = data.AssetBranchVMList.Select(x => x.BRANCH_ID).First();
                    data.PRODUCT_ID = prodid;
                    _unitofWork.AssetBranch.Update(data);
                    _unitofWork.Save();
                    TempData["Success"] = "<p>Branch Assigned :  Succesfully Updated</p>";
                    TempData["Title"] = "<strong>Data Updated</strong> <br />";
                    TempData["Icon"] = "fa fa-check";
                    return RedirectToAction("Create", new { id = itemid });
                }
                else
                {
                    return View("Create", data);
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
            int itemid = CodeService.GetAssetItemIdByBranchAssignId(id);
            try
            {
                _unitofWork.AssetBranch.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Branch Assigned for this product : " + "</p>";
                TempData["Title"] = "<strong> Removed</strong> <br />";
                TempData["Icon"] = "fa fa-check";
                return RedirectToAction("Create", new { id = itemid });
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                TempData["Fail"] = "Failed to remove. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
            }
            return RedirectToAction("Create");
        }
    }
}