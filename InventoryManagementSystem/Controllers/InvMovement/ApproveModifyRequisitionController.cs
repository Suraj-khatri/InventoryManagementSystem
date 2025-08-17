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
    public class ApproveModifyRequisitionController : Controller
    {
        // GET: ApproveModifyRequisition
        private IUnitOfWork _unitofWork;
        public ApproveModifyRequisitionController()
        {
            _unitofWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}