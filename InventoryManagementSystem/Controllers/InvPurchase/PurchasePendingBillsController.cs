using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
   
    public class PurchasePendingBillsController : Controller
    {
        // GET: PurchasePendingBills
        private IUnitOfWork _unitofWork;
        private DapperRepoServices _dapperrepo;
        public PurchasePendingBillsController()
        {
            _unitofWork = new UnitOfWork();
            _dapperrepo = new DapperRepoServices();
        }

        [UserAuthorize(menuId: 23)]
        public ActionResult Index()
        {
            var list = _dapperrepo.GetAllUnpaidBill();
            return View(list);
        }
        public ActionResult IsPaid(int id)
        {
            var Item = _unitofWork.BillInfo.GetById(id);
            Item.paid_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString(); ;
            Item.paid_date = DateTime.Now.Date;
            _unitofWork.BillInfo.Update(Item);
            _unitofWork.Save();
            TempData["Success"] = "<p>Purchase  :  Succesfully Paid</p>";
            TempData["Title"] = "<strong>Bill Paid</strong> <br />";
            TempData["Icon"] = "fa fa-check";
            return RedirectToAction("Index");
        }
    }
}