using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.InvMovement
{
    [UserAuthorize]
    [UserAuthorize(menuId: 30)]
    public class RecommendRequisitionController : Controller
    {
        // GET: RecommendRequisition
        private IUnitOfWork _unitofWork;
        public RecommendRequisitionController()
        {
            _unitofWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var list = _unitofWork.InRequisitionMessage.GetAllRequested(userid, branchid);
            return View(list);
        }
        public ActionResult ViewRequested(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.inreqList = _unitofWork.InRequisition.GetItemById(id);
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewRequested(InRequisitionMessageVM data)
        {
            if (data.recommed_message is null)
            {
                data.recommed_message = "";
            }
            if (ModelState.IsValid)
            {
                if (data.recommed_message != "")
                {
                    var Item = _unitofWork.InRequisitionMessage.GetById(data.id);
                    Item.recommed_date = DateTime.Now.Date;
                    Item.recommed_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
                    Item.recommed_message = data.recommed_message;
                    Item.status = "Recommended";
                    _unitofWork.InRequisitionMessage.Update(Item);
                    _unitofWork.Save();
                    TempData["Success"] = "<p>Data :  Succesfully Recommended</p>";
                    TempData["Title"] = "<strong>Data Recommended</strong> <br />";
                    TempData["Icon"] = "fa fa-check";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Fail"] = "Please Give Recommend Message";
                    TempData["Title"] = " <strong>Error</strong> <br />";
                    TempData["Icon"] = "fa fa-exclamation-circle";
                    return RedirectToAction("ViewRequested", new { id = data.id });
                }
            }
            else
            {
                return View(data);
            }
        }
        public ActionResult RejectRequisition(int id)
        {
            var Item = _unitofWork.InRequisitionMessage.GetById(id);
            Item.rejected_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
            Item.rejected_date = DateTime.Now.Date;
            Item.status = "Rejected";
            _unitofWork.InRequisitionMessage.Update(Item);
            _unitofWork.Save();
            TempData["Success"] = "<p>Data :  Succesfully Rejected </p>";
            TempData["Title"] = "<strong>Data Rejected</strong> <br />";
            TempData["Icon"] = "fa fa-check";
            return RedirectToAction("Index");
        }
    }
}