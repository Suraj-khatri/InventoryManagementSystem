using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.Fixed_Asset.AssetManagement
{
    [UserAuthorize]
    public class ApprovalNotificationController : Controller
    {
        // GET: ApprovalNotification
        private IUnitOfWork _unitofWork;

        public ApprovalNotificationController()
        {
            _unitofWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            var list = _unitofWork.ChangesApprovalQueue.GetAll();
            return View(list);
        }
        public ActionResult ViewAssetBooking(int id)
        {
            var list = _unitofWork.AssetInventoryTemp.GetById(id);
            list.reqmode = "New Record";
            list.reqtype = "ASSET BOOKING REQUEST";
            return View(list);
        }
    }
}