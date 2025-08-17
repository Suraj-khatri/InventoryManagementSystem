using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.Fixed_Asset.AssetAquisition
{
    [UserAuthorize]
    public class ApproveAssetRequisitionController : Controller
    {
        // GET: ApproveAssetRequisition
        private IUnitOfWork _unitofWork;
        public ApproveAssetRequisitionController()
        {
            _unitofWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            var list = _unitofWork.AssetRequisitionMessage.GetAllRequested();
            return View(list);
        }
        public ActionResult ViewRequested(int id)
        {
            var record = _unitofWork.AssetRequisitionMessage.GetById(id);
            record.inreqList = _unitofWork.AssetRequisition.GetItemById(id);
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewRequested(AssetRequisitionMessageVM data)
        {
            if (ModelState.IsValid)
            {
                var Item = _unitofWork.AssetRequisitionMessage.GetById(data.id);
                Item.approved_date = DateTime.Now.Date;
                Item.approval_message = data.approval_message;
                Item.approved_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                Item.status = "Approved";
                _unitofWork.AssetRequisitionMessage.Update(Item);
                _unitofWork.Save();
                TempData["Success"] = "<p>Data :  Succesfully Approved</p>";
                TempData["Title"] = "<strong>Data Approved</strong> <br />";
                TempData["Icon"] = "fa fa-check";
                return RedirectToAction("Index");
            }
            else
            {
                return View(data);
            }
        }
    }
}