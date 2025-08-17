using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvPurchase
{
   
    public class OtherBillsInfoController : Controller
    {
        // GET: OtherBills
        private IUnitOfWork _unitofWork;

        public OtherBillsInfoController()
        {
            _unitofWork = new UnitOfWork();
        }
        [UserAuthorize(menuId: 26)]
        public ActionResult Index()
        {
            var list = _unitofWork.OtherBillsInfo.GetAll();
            return View(list);
        }
        [UserAuthorize(menuId: 26)]
        public ActionResult Create()
        {
            var record = new OtherBillsInfoVM();
            record.bill_date = DateTime.Now.Date;
            record.entered_date = DateTime.Now.Date;
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OtherBillsInfoVM data)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    bool billExists = _unitofWork.OtherBillsInfo.BillNoExists(data.billno.Trim());
                    if (billExists)
                    {
                        ModelState.AddModelError("billno", $"Bill No. {data.billno} already exists.");
                        return View(data);
                    }
                    data.vat_amt = (Convert.ToDecimal(0.13) * data.bill_amount);
                    _unitofWork.OtherBillsInfo.Insert(data);
                    _unitofWork.Save();
                    TempData["Success"] = "Bill Information Succesfully added";
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
        public ActionResult Edit(int Id)
        {
            var data = _unitofWork.OtherBillsInfo.GetById(Id);
            return View("Create", data);
        }

        [UserAuthorize(menuId: 26)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OtherBillsInfoVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    data.modified_date = DateTime.Now;
                    data.modified_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString(); ;
                    _unitofWork.OtherBillsInfo.Update(data);
                    _unitofWork.Save();
                    TempData["Success"] = "<p>Other Bills Info :  Succesfully Updated</p>";
                    TempData["Title"] = "<strong>Data Updated</strong> <br />";
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
                TempData["Fail"] = "Failed to Update Data. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
            }
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                _unitofWork.OtherBillsInfo.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Bills No : " + _unitofWork.OtherBillsInfo.GetById(id).billno + "</p>";
                TempData["Title"] = "<strong>Bill Removed</strong> <br />";
                TempData["Icon"] = "fa fa-check";
                return RedirectToAction("Index");
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                TempData["Fail"] = "Failed to remove. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
            }
            return RedirectToAction("Index");
        }
        public ActionResult IsPaid(int id)
        {
            var Item = _unitofWork.OtherBillsInfo.GetById(id);
            Item.paid_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString(); ;
            Item.paid_date = DateTime.Now.Date;
            Item.is_paid = true;
            _unitofWork.OtherBillsInfo.Update(Item);
            _unitofWork.Save();
            TempData["Success"] = "<p>Purchase  :  Succesfully Paid</p>";
            TempData["Title"] = "<strong>Bill Paid</strong> <br />";
            TempData["Icon"] = "fa fa-check";
            return RedirectToAction("Index");
        }
    }
}