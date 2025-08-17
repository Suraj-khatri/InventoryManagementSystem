using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [UserAuthorize]
    public class BranchAssignController : Controller
    {
        // GET: BranchAssign
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public BranchAssignController()
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
                var record = new INBranchVM();
                record.INPRODUCTs = _unitofWork.InProduct.GetById(id);
                record.BranchList = _dropDownList.BranchList();
                record.INBranchVMList = _unitofWork.InBranchAssign.GetAllByProductId(record.INPRODUCTs.id);
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
        public ActionResult Create(INBranchVM data)
        {
            try
            {
                data.INPRODUCTs = _unitofWork.InProduct.GetById(data.ID);//data.ID is item Id Here
                data.BranchList = _dropDownList.BranchList();
                data.INBranchVMList = _unitofWork.InBranchAssign.GetAllByProductId(data.INPRODUCTs.id);
                int prodid = CodeService.GetInProductId(data.ID);
                int parentid = CodeService.GetParentId(data.ID);
                //data.ID = 0;
                if (ModelState.IsValid)
                {
                    if (data.BRANCH_ID > 0)
                    {
                        if (!_unitofWork.InBranchAssign.IsBranchAssigned(prodid, data.BRANCH_ID))
                        {
                            data.PRODUCT_ID = prodid;
                            data.ProductGroupId = parentid;
                            _unitofWork.InBranchAssign.Insert(data);
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
                                if (!_unitofWork.InBranchAssign.IsBranchAssigned(prodid, Int32.Parse(itm.Value)))
                                {
                                    data.PRODUCT_ID = prodid;
                                    data.BRANCH_ID = Int32.Parse(itm.Value);
                                    data.ProductGroupId = parentid;
                                    _unitofWork.InBranchAssign.Insert(data);
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
            int itemid = CodeService.GetInItemIdByBranchAssignId(id);
            var data = _unitofWork.InBranchAssign.GetById(id);
            data.INPRODUCTs = _unitofWork.InProduct.GetById(itemid);
            data.INBranchVMList = _unitofWork.InBranchAssign.GetAllByProductId(data.INPRODUCTs.id);
            // data.INPRODUCTs = _unitofWork.InProduct.GetById(id);
            data.BranchList = _dropDownList.BranchList();
            return View("Create", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(INBranchVM data)
        {
            int itemid = CodeService.GetInItemIdByBranchAssignId(data.ID);
            try
            {
                data.BranchList = _dropDownList.BranchList();
                data.INPRODUCTs = _unitofWork.InProduct.GetById(itemid);
                data.INBranchVMList = _unitofWork.InBranchAssign.GetAllByProductId(data.INPRODUCTs.id);
                int prodid = CodeService.GetInProductId(itemid);
                int parentid = CodeService.GetParentId(itemid);
                if (ModelState.IsValid)
                {
                    data.BRANCH_ID = data.Branches.BRANCH_ID;
                    data.PRODUCT_ID = prodid;
                    data.ProductGroupId = parentid;
                    _unitofWork.InBranchAssign.Update(data);
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
            int itemid = CodeService.GetInItemIdByBranchAssignId(id);
            try
            {
                _unitofWork.InBranchAssign.Delete(id);
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

        public ActionResult RemoveMultipleBranchAssign(int id)
        {
            var record = new INBranchVM();
            record.INPRODUCTs = _unitofWork.InProduct.GetByItemId(id);
            //record.BranchList = _dropDownList.BranchList();
            record.INBranchVMList = _unitofWork.InBranchAssign.GetAllByProductId(id);
            record.IS_ACTIVE = true;
            record.PRODUCT_ID = id;
            return View(record);
        }

        [HttpPost]
        public ActionResult RemoveMultipleBranchAssign(INBranchVM inbranch)
        {
            var itmcount = inbranch.INItemVMList.Where(x => x.IsRowCheck == true).Count();
            var itemid = CodeService.GetInItemIdFromProductId(inbranch.PRODUCT_ID);
            if (itmcount > 0)
            {
                foreach (var item in inbranch.INItemVMList.Where(x => x.IsRowCheck == true && x.id > 0))
                {
                    try
                    {
                        _unitofWork.InBranchAssign.Delete(item.id);
                        _unitofWork.Save();

                    }
                    catch (Exception Ex)
                    {
                        return Json(new { success = false, err = Ex + "Error Occured !!.", JsonRequestBehavior.AllowGet });
                    }
                }
                return Json(new { success = true, mes = "Branch Assigned Removed Successfully",pid= itemid, JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { success = false, err = "Please Select At Least One Branch To Remove.", JsonRequestBehavior.AllowGet });
            }
        }
    }
}