using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [UserAuthorize]
    public class VendorAssignController : Controller
    {
        // GET: VendorAssign
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public VendorAssignController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Create(int id)
        {
            bool isactive = CodeService.GetInItemStatus(id);
            int parentid = CodeService.GetParentId(id);
            if (isactive == true)
            {
                var record = new VendorBidPriceVM();
                record.INPRODUCTs = _unitofWork.InProduct.GetById(id);
                record.VendorList = _dropDownList.VendorList();
                record.VendorBidPriceVMList = _unitofWork.InVendorAssign.GetAllByProductId(record.INPRODUCTs.id);
                record.is_active = true;
                return View(record);
            }
            else
            {
                TempData["Fail"] = "Failed To Assign Vendor. Product Is not Active.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index", "InProduct", new { id = parentid });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendorBidPriceVM data)
        {
            try
            {
                data.INPRODUCTs = _unitofWork.InProduct.GetById(data.id);
                data.VendorList = _dropDownList.VendorList();
                data.VendorBidPriceVMList = _unitofWork.InVendorAssign.GetAllByProductId(data.INPRODUCTs.id);
                int prodid = CodeService.GetInProductId(data.id);
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InVendorAssign.IsVendorAssigned(prodid, data.vendor_id))
                    {
                        data.product_id = prodid;
                        data.ModifiedBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name; ;
                        data.ModifiedDate = DateTime.Now.Date;
                        _unitofWork.InVendorAssign.Insert(data);
                        _unitofWork.Save();
                        TempData["Success"] = "Vendor : Successfully Assigned for this product";
                        TempData["Title"] = "<strong>Data Assigned</strong> <br />";
                        TempData["Icon"] = "fa fa-check";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        ModelState.AddModelError("vendor_id", "Vendor already assigned for this product.");
                        return View("Create", data);
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
                TempData["Fail"] = "Failed to Create Data. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
                return View("Create", data);
            }
        }
        public ActionResult Edit(int Id)
        {
            var data = _unitofWork.InVendorAssign.GetById(Id);
            data.VendorList = _dropDownList.VendorList();
            data.VendorBidPriceVMList = _unitofWork.InVendorAssign.GetAllByProductId(data.INPRODUCTs.id);
            return View("Create", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VendorBidPriceVM data)
        {
            data.VendorList = _dropDownList.VendorList();
            data.VendorBidPriceVMList = _unitofWork.InVendorAssign.GetAllByProductId(data.product_id);
            data.INPRODUCTs = _unitofWork.InProduct.GetByItemId(data.product_id);
            int itemid = CodeService.GetInItemIdByVendorAssignId(data.id);
            try
            {
                if (ModelState.IsValid)
                {

                    data.is_active = true;//this is the latest price
                    data.ModifiedBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                    data.ModifiedDate = DateTime.Now.Date;
                    _unitofWork.InVendorAssign.Update(data);
                    _unitofWork.Save();
                    TempData["Success"] = "<p>Vendor Assigned :  Succesfully Updated</p>";
                    TempData["Title"] = "<strong>Data Updated</strong> <br />";
                    TempData["Icon"] = "fa fa-check";
                    return RedirectToAction("Create", new { id = itemid });
                }
                else
                {
                    return RedirectToAction("Create", new { id = itemid });
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
            int itemid = CodeService.GetInItemIdByVendorAssignId(id);
            try
            {
                _unitofWork.InVendorAssign.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Vendor Assigned for Product : " + _unitofWork.InBranch.GetById(id).BRANCH_NAME + "</p>";
                TempData["Title"] = "<strong>Vendor Assigned for Product Removed</strong> <br />";
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
            return RedirectToAction("Create", new { id = itemid });
        }
    }
}