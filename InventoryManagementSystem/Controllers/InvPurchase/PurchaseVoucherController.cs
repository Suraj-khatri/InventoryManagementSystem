using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Helper;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
   // [UserAuthorize(menuId: 18)]

    public class PurchaseVoucherController : Controller
    {
        // GET: PurchaseVoucher
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public PurchaseVoucherController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }

        [UserAuthorize(menuId: 18)]

        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32(Session["BranchId"]);
            var assignedrole = Session["AssignRole"].ToString();

            List<BillInfoVM> list = new List<BillInfoVM>();

            if (assignedrole == "StoreKeeper(HeadOffice)" || assignedrole == "Admin_User")
            {
                list = _dapperrepo.GetAllPurchaseVoucherRequisition();
            }     

            return View(list);
        }

        [UserAuthorize(menuId: 18)]
        public ActionResult Create()
            {
            var branchId = Convert.ToInt32(Session["BranchId"]);
            var record = new BillInfoVM();
            record.VendorList = _dropDownList.VendorList();
            record.ProductNameFromVendorList = _dropDownList.ProductNameFromVendorList();
            record.bill_date = DateTime.Now.Date;
            record.EmployeeList=_dropDownList.BranchWiseSupervisorandAdminsList(branchId);
            record.branch_id = branchId;
           // record.party_code = record.VendorList.FirstOrDefault().Value;
            return View(record);
        }


        public JsonResult CheckIfSerailNoExists(int snf, int snt, string tpname)
        {
            var pid = CodeService.GetProductIdFromProductName(tpname.Trim());
            var getSerialNo = _dapperrepo.getSerialNoFromSerialProductStock(snf, snt, pid);
            return Json(getSerialNo);
        }

        public JsonResult CheckIfSerailNoExistsInEachBranch(int snf, int snt, string tpname)
        {
            var branchid = Convert.ToInt32(Session["BranchId"]);
            var pid = CodeService.GetProductIdFromProductName(tpname.Trim());
            var getSerialNo = _dapperrepo.getSerialNoFromSerialProductStockWithBranch(snf, snt, pid, branchid);
            return Json(getSerialNo);

        }
        public JsonResult CheckIfSerailNoExistsInReturnedBranch(int snf, int snt, int branchid, string tpname)
        {
            var pid = CodeService.GetProductIdFromProductName(tpname.Trim());
            var getSerialNo = _dapperrepo.getSerialNoFromSerialProductStockWithBranch(snf, snt, pid, branchid);
            return Json(getSerialNo);
        }
    }
}