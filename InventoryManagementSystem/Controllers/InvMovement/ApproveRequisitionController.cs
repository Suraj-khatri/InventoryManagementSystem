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
    public class ApproveRequisitionController : Controller
    {
        // GET: ApproveRequisition
        private IUnitOfWork _unitofWork;
        public ApproveRequisitionController()
        {
            _unitofWork = new UnitOfWork();
        }
        [UserAuthorize(menuId:31)]
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var list = _unitofWork.InRequisitionMessage.GetAllRecommend(userid,branchid);
            return View(list);
        }
        public ActionResult ViewRecommended(int id)
        {
            var record = _unitofWork.InRequisitionMessage.GetById(id);
            record.inreqList = _unitofWork.InRequisition.GetItemById(id);
            return View(record);
        }
        public ActionResult RejectRequisition(int id)
        {
            var Item = _unitofWork.InRequisitionMessage.GetById(id);
            Item.rejected_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
            Item.rejected_date = DateTime.Now.Date;
            Item.status = "Rejected";
            _unitofWork.InRequisitionMessage.Update(Item);

            var notificationdata = _unitofWork.InNotifications.GetByPOMId(id);
            if (notificationdata != null)
            {
                notificationdata.Status = true;
                _unitofWork.InNotifications.Update(notificationdata);
            }

            _unitofWork.Save();

            TempData["Success"] = "<p>Data :  Succesfully Rejected </p>";
            TempData["Title"] = "<strong>Data Rejected</strong> <br />";
            TempData["Icon"] = "fa fa-check";
            return RedirectToAction("Index");
        }
    }
}