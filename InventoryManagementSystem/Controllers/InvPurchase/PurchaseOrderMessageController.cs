using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
   
    public class PurchaseOrderMessageController : Controller
    {
        // GET: PurchaseOrderMessage
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public PurchaseOrderMessageController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }

        [UserAuthorize(menuId: 19)]
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var assignedrole = Session["AssignRole"].ToString();
            if (assignedrole == "StoreKeeper(HeadOffice)" || assignedrole == "Admin_User")
            {
                var list = _dapperrepo.GetAllPurchaseOrderMessageRequisition(0, 0);
                return View(list);
            }
            else
            {
                var list = _dapperrepo.GetAllPurchaseOrderMessageRequisition(branchid, userid);
                return View(list);
            }

        }
        [UserAuthorize(menuId: 19)]
        public ActionResult Create()
        {
            var record = new PurchaseOrderMessageVM();
            record.VendorList = _dropDownList.VendorList();
            record.EmployeeList = _dropDownList.EmployeeList();
            record.ProductNameFromVendorList = _dropDownList.ProductNameFromVendorList();
            record.remarks = "Please state P.O. No. in your delivery challan and invoice.~" + "\r" +
                  "The Bank reserves the right to reject the items delivered beyond the delivery period mentioned above.~" + "\r" +
                  "The Bank reserves the right to reject the items delivered that do not match the sample provided with Bid or " +
                  "do not match the quality standard.";
            record.order_date = DateTime.Now.Date;
            record.delivery_date = DateTime.Now.Date.AddDays(7);
            return View(record);
        }
        public JsonResult GetProductNameForVendor(int vendorname)
        {
            var data = _dropDownList.ProductNameFromVendorList(vendorname).ToList();
            data.Insert(0, new SelectListItem { Text = "--Select--", Value = "--Select--" });
            return Json(data);
        }
        public JsonResult GetUnitandRateForProduct(int prodname, int vendorname)
        {
            var data = CodeService.GetUnitandRate(prodname, vendorname);
            return Json(data);
        }
        public JsonResult Getserialstatusforproduct(string proname)
        {
            var data = CodeService.GetSerialStatusForProduct(proname);
            return Json(data);
        }
        public JsonResult GetSerialStatusandTempPurchaseId(string proname, int tppid)
        {
            var data = CodeService.GetSerialStatusandTempPurchaseId(proname, tppid);
            return Json(data);
        }
        public void RemoveTempPurchase(int id)
        {
            _unitofWork.TempPurchaseOther.Delete(id);
            _unitofWork.TempPurchase.Delete(id);
            _unitofWork.Save();
        }
        public JsonResult GetReOrderQtyFromInBranch(int id)
        {
            int userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchname = @Session["BranchName"].ToString();
            var bid = CodeService.GetBranchIdFromBranchName(branchname);
            var data = CodeService.GetReOrderQtyForPid(id, bid,userid);
            return Json(data);
        }
        public JsonResult GetMaxHoldingQtyFromInBranch(int id)
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchname = @Session["BranchName"].ToString();
            var bid = CodeService.GetBranchIdFromBranchName(branchname);
            var data = CodeService.GetMaxHoldingQtyForPid(id,bid,userid);
            return Json(data);
        }
        public JsonResult GetTempPurIdFromTempPurchaseOther(int tempid)
        {
            var data = _unitofWork.TempPurchaseOther.GetAllTempPurchaseOtherFromTempPurId(tempid).ToList();
            return Json(data);
        }
        public JsonResult GetTempPurchaseData()
        {
            var data = _unitofWork.TempPurchase.GetAll();
            return Json(data);
        }
    }
}