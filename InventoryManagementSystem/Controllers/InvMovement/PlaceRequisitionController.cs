using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvMovement
{
    //[UserAuthorize(menuId:29)]
    public class PlaceRequisitionController : Controller
    {
        // GET: PlaceRequisition
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;

        public PlaceRequisitionController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 29)]
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var assignedrole = Session["AssignRole"].ToString();
            @ViewBag.GroupList = _unitofWork.InItem.GetAllByParentId();
            if (assignedrole == "StoreKeeper(HeadOffice)" || assignedrole == "Admin_User" || assignedrole.Trim() == "Administrator")
            {
                var list = _dapperrepo.GetAllPlaceRequisition(0, 0);
                return View(list);
            }
            else
            {
                var list = _dapperrepo.GetAllPlaceRequisition(branchid, userid);
                return View(list);
            }

        }
        [UserAuthorize(menuId: 29)]
        public ActionResult Create(int groupid)
        {
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var assignedrole = Session["AssignRole"].ToString().Trim();
            var allowedRoles = new List<string> { "StoreKeeper(HeadOffice)", "Admin_User", "Administrator" };

            if (!allowedRoles.Contains(assignedrole) && CodeService.HasPendingAcknowledgmentsForBranch(branchid) && branchid!=12)
            {
                TempData["Fail"] = "Please Acknowledge  all Previous Placed Requisitions in branch For New Requisition.";
                TempData["Title"] = "<strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index");
            }
            else
            {
                var record = new InRequisitionMessageVM();
                record.AssignedRole = assignedrole;
                record.ProductNameList = _dropDownList.ProductList();
                record.ProductGroupList = _dropDownList.ProductGroupListForplaceRequisition(groupid);
                record.ProductGroupList.Remove(record.ProductGroupList.First());
                record.BranchNameList = _dropDownList.BranchListForRequestWithBranch();
                record.PriorityList = _dropDownList.PriorityList();
                record.SuperVisorList = _dropDownList.SuperVisorAssignForEmployee(branchid, userid);
                return View(record);
            }
        }

        public JsonResult GetUnitForProduct(int prodname)
        {
            var data = CodeService.GetUnitForProductName(prodname);
            return Json(data);
        }
        public JsonResult GetProductNameFromProductGroupName(int groupid)
        {
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var data = _dropDownList.ProductWithProductGroupAndBranchAssignList(groupid, branchid).ToList();
            data.Remove(data.First());
            data.Insert(0, new SelectListItem { Text = "--Select--", Value = "--Select--" });
            return Json(data);
        }
    }
}