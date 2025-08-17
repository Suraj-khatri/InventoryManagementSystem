using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
   
    public class BranchAssignGroupWiseController : Controller
    {
        // GET: BranchAssignGroupWise
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public BranchAssignGroupWiseController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 15)]
        public ActionResult Create()
        {
            var record = new INBranchVM();
            record.BranchList = _dropDownList.BranchList();
            record.ProductGroupList = _dropDownList.ProductGroupList();
            record.IS_ACTIVE = true;
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(INBranchVM data)
        {
            try
            {
                data.BranchList = _dropDownList.BranchList();
                data.ProductGroupList = _dropDownList.ProductGroupList();
                //List<In_ItemVM> initemlist = CodeService.GetAllInItems();
                //List<In_ItemVM> initemlist1 = CodeService.GetAllInItemswithParentId(data.ProdGrpId);

                if (ModelState.IsValid)
                {
                     _dapperrepo.InsertProductAssignmentWithGroup(data.BRANCH_ID,data.ProdGrpId);
                    //if (data.ProdGrpId == 0 && data.BRANCH_ID == 0)
                    //{
                    //    foreach (var itm in initemlist)
                    //    {
                    //        int prodid = CodeService.GetInProductId(itm.id);
                    //        foreach (var itm1 in data.BranchList)
                    //        {
                    //            if (itm1.Value != "0")
                    //            {
                    //                if (!_unitofWork.InBranchAssign.IsBranchAssigned(prodid, Int32.Parse(itm1.Value)))
                    //                {
                    //                    data.ProductGroupId = CodeService.GetParentId(itm.id);
                    //                    data.PRODUCT_ID = CodeService.GetInProductId(itm.id);
                    //                    data.BRANCH_ID = Int32.Parse(itm1.Value);
                    //                    _unitofWork.InBranchAssign.Insert(data);
                    //                    _unitofWork.Save();
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //else if (data.ProdGrpId > 0 && data.BRANCH_ID == 0)
                    //{
                    //    foreach (var itm1 in initemlist1)
                    //    {
                    //        int prodid1 = CodeService.GetInProductId(itm1.id);
                    //        foreach (var itm in data.BranchList)
                    //        {
                    //            if (itm.Value != "0")
                    //            {
                    //                if (!_unitofWork.InBranchAssign.IsBranchAssigned(prodid1, Int32.Parse(itm.Value)))
                    //                {
                    //                    data.ProductGroupId = CodeService.GetParentId(itm1.id);
                    //                    data.PRODUCT_ID = CodeService.GetInProductId(itm1.id);
                    //                    data.BRANCH_ID = Int32.Parse(itm.Value);
                    //                    _unitofWork.InBranchAssign.Insert(data);
                    //                    _unitofWork.Save();
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //else if (data.ProdGrpId == 0 && data.BRANCH_ID > 0)
                    //{
                    //    foreach (var itm in initemlist)
                    //    {
                    //        int prodid = CodeService.GetInProductId(itm.id);
                    //        if (!_unitofWork.InBranchAssign.IsBranchAssigned(prodid, data.BRANCH_ID))
                    //        {
                    //            data.ProductGroupId = CodeService.GetParentId(itm.id);
                    //            data.PRODUCT_ID = CodeService.GetInProductId(itm.id);
                    //            data.BRANCH_ID = data.BRANCH_ID;
                    //            _unitofWork.InBranchAssign.Insert(data);
                    //            _unitofWork.Save();
                    //        }

                    //    }
                    //}
                    //else if (data.ProdGrpId > 0 && data.BRANCH_ID > 0)
                    //{
                    //    foreach (var itm1 in initemlist1)
                    //    {
                    //        int prodid1 = CodeService.GetInProductId(itm1.id);
                    //        if (!_unitofWork.InBranchAssign.IsBranchAssigned(prodid1, data.BRANCH_ID))
                    //        {
                    //            data.ProductGroupId = CodeService.GetParentId(itm1.id);
                    //            data.PRODUCT_ID = CodeService.GetInProductId(itm1.id);
                    //            data.BRANCH_ID = data.BRANCH_ID;
                    //            _unitofWork.InBranchAssign.Insert(data);
                    //            _unitofWork.Save();
                    //        }
                    //    }
                    //}
                    TempData["Success"] = "Branch : Successfully Assigned for Product Group";
                    TempData["Title"] = "<strong>Data Assigned</strong> <br />";
                    TempData["Icon"] = "fa fa-check";
                    return RedirectToAction("Index", "GroupWiseProductDetailsList");
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
    }
}