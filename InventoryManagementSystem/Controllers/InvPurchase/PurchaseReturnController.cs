using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
    [UserAuthorize]
    public class PurchaseReturnController : Controller
    {
        // GET: PurchaseReturn
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public PurchaseReturnController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Index(int id)
        {
            var data = new InPurchaseVM();
            data.InPurchaseList = _unitofWork.InPurchase.GetAllByBillId(id);
            data.BillNo = data.InPurchaseList.FirstOrDefault().BillNo;
            data.VendorName = data.InPurchaseList.FirstOrDefault().VendorName;
            data.bill_id = id;
            return View(data);
        }
    }
}