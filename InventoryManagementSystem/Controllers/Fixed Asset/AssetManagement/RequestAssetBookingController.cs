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

namespace InventoryManagementSystem.Controllers.Fixed_Asset.AssetManagement
{
    [UserAuthorize]
    public class RequestAssetBookingController : Controller
    {
        // GET: RequestAssetBooking
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public RequestAssetBookingController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Index()
        {
            var list = _unitofWork.AssetInventoryTemp.GetAll();
            return View(list);
        }
        public ActionResult Create()
        {
            var record = new AssetInventoryTempVM();
            record.AssetStatusList = _dropDownList.AssetStatusList();
            record.VendorNameList = _dropDownList.VendorList();
            record.BranchNameList = _dropDownList.BranchList();
            record.BranchNameList.Remove(record.BranchNameList.First());
            record.EmployeeList = _dropDownList.EmployeeList();
            record.DepartmentList = _dropDownList.DepartmentList();
            record.AssetProductList = _dropDownList.AssetProductList();
            record.booked_date = DateTime.Now.Date;
            record.depr_start_date = record.booked_date.AddMonths(1);
            record.purchase_date = DateTime.Now.Date;
            record.is_amortised = true;
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AssetInventoryTempVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //AssetInventoryTemp added
                    data.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                    data.created_date = DateTime.Now.Date;
                    data.forwarded_date = DateTime.Now.Date;
                    data.status = "Requested";
                    data.IsActive = true;
                    data.group_id = data.product_id;
                    _unitofWork.AssetInventoryTemp.Insert(data);
                    _unitofWork.Save();

                    //changesapprovalqueue added
                    changesApprovalQueueVM caq = new changesApprovalQueueVM();
                    caq.dataid =CodeService.GetAssetInvTemId();
                    caq.tableName = "ASSET_INVENTORY_TEMP";
                    caq.identifierField = "ID";
                    caq.modType = "Insert";
                    caq.description = "ASSET BOOKING REQUEST";
                    caq.createdBy = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                    caq.createdDate = DateTime.Now.Date;
                    caq.forwarded_date= DateTime.Now.Date;
                    caq.forwarded_to = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                    _unitofWork.ChangesApprovalQueue.Insert(caq);
                    _unitofWork.Save();

                    TempData["Success"] = "Successfully Requested";
                    TempData["Title"] = "<strong>Data Added</strong> <br />";
                    TempData["Icon"] = "fa fa-check";
                    return RedirectToAction("Index");
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
    }
}