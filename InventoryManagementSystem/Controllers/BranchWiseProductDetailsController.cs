using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
   
    public class BranchWiseProductDetailsController : Controller
    {
        // GET: BranchWiseProductDetails
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public BranchWiseProductDetailsController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 16)]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var record = new INBranchVM();
            record.BranchList = _dropDownList.BranchList();
            record.ProductGroupList = _dropDownList.ProductGroupList();
            record.BRANCH_ID = Convert.ToInt32(Session["BranchId"]);
            record.INBranchVMList = _dapperrepo.GetAllBranchAssign(record.BRANCH_ID, 0);
            return View(record);
        }

        [HttpPost]
        public ActionResult Index(INBranchVM data)
        {
            data.BranchList = _dropDownList.BranchList();
            data.ProductGroupList = _dropDownList.ProductGroupList();
            data.INBranchVMList = _dapperrepo.GetAllBranchAssign(data.BRANCH_ID, data.ProdGrpId);
            return View(data);
        }
        [UserAuthorize(menuId: 16)]
        public ActionResult Edit(int pid, int branchid)
        {
            var data = _unitofWork.InBranchAssign.GetItemById(pid, branchid);
            return View("Edit", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(INBranchVM data)
        {
            try
            {
                if (ModelState.IsValidField("InBranchAssign"))
                {
                    data.BRANCH_ID = data.Branches.BRANCH_ID;
                    data.PRODUCT_ID = data.INPRODUCTs.id;
                    _unitofWork.InBranchAssign.Update(data);
                    _unitofWork.Save();
                    TempData["Success"] = "<p>Succesfully Updated</p>";
                    TempData["Title"] = "<strong>Data Updated</strong> <br />";
                    TempData["Icon"] = "fa fa-check";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Fail"] = "Something Went Wrong. Please try again later.";
                    TempData["Title"] = " <strong>Error</strong> <br />";
                    TempData["Icon"] = "fa fa-exclamation-circle";
                    return View("Edit", data);
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
            return View("Edit", data);
        }
    }
}