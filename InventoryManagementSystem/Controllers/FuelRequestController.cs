using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [UserAuthorize]
    public class FuelRequestController : Controller
    {
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;

        public FuelRequestController()
        {
            _dapperrepo = new DapperRepoServices();
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        // GET: FuelRequest
        [UserAuthorize(menuId:1098)]
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var assignedrole = Session["AssignRole"].ToString();
            if (assignedrole == "StoreKeeper(HeadOffice)" || assignedrole == "Admin_User" || assignedrole.Trim() == "Administrator" || assignedrole.Trim() == "Fuel Manager")
            {
                var list = _dapperrepo.GetAllFuelRequisition(0, 0);
                return View(list);
            }
            else
            {
                var list = _dapperrepo.GetAllFuelRequisition(branchid, userid);
                return View(list);
            }
        }

        [UserAuthorize(menuId: 1098)]
        public ActionResult Create()
        {
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var record = new FuelRequestsMessageVM();
            record.Branch_id = branchid;
            record.BranchNameList = _dropDownList.BranchList();
            record.PriorityList = _dropDownList.PriorityList();
            record.FuelCategoryList = _dropDownList.FuelCategoryList();
            record.VehicleCategoryList = _dropDownList.VehicleCategoryList();
            record.VendorList = _dropDownList.FuelVendorList();
            record.SuperVisorList = _dropDownList.BranchWiseSupervisorList(branchid);
            record.SuperVisorList.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" });
            return View(record);

        }

        [UserAuthorize(menuId: 1098)]
        public ActionResult ViewRequestedDetails(int id)
        {
            var record = _unitofWork.FuelRequestMessage.GetById(id);
            record.inreqList = _unitofWork.FuelRequest.GetItemById(id);
            return View(record);
        }
        [HttpGet]
        public ActionResult GetPreviousKMRun(string vehicleNo)
        {
            var data = _dapperrepo.GetPreviousKmRUn(vehicleNo);
            if (data == null)
            {
                data = "";
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult VerifyFuelStatus(string vehicleNo)
        {
            var assignedrole = Session["AssignRole"].ToString();
            var res = _dapperrepo.getFuelStatusByVehicleNo(vehicleNo);
            if (assignedrole == "StoreKeeper(HeadOffice)" || res == "Approved" || res == "Rejected" || res == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult CheckIfRequestExists( string couponNO)
        {
            bool isDuplicateCoupon = _dapperrepo.IsDuplicateCoupon(couponNO);
        
            if (isDuplicateCoupon)
            {
                return Json(new { success = false, message = "Coupon number is already in use. Please use a different coupon." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}