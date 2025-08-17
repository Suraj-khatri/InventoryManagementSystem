using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;

namespace InventoryManagementSystem.Controllers.InvMovement
{
    [UserAuthorize]
    public class AcknowledgeRequisitionController : Controller
    {
        // GET: AcknowledgeRequisition
        private IUnitOfWork _unitofWork;
        private DapperRepoServices _dapperrepo;
        public AcknowledgeRequisitionController()
        {
            _unitofWork = new UnitOfWork();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 33)]
        public ActionResult Index()
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var assignedrole = Session["AssignRole"].ToString();
            if (assignedrole == "Admin_User" || assignedrole.Trim() == "Administrator")
            {
                var list = _dapperrepo.GetAllDispatchPlaceRequisition(0, 0);
                return View(list);
            }
            else
            {
                var list = _dapperrepo.GetAllDispatchPlaceRequisition(branchid, userid);
                return View(list);
            }
        }
    
        public ActionResult ViewDispatched(int id)
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            var assignedrole = Session["AssignRole"].ToString();
       
            IEnumerable<InRequisitionMessageVM> acknowledgeMenuItems;
            if (assignedrole.Trim() == "Admin_User" || assignedrole.Trim() == "Administrator")
            {
                acknowledgeMenuItems = _dapperrepo.GetAllDispatchPlaceRequisition(0, 0);
            }
            else if (assignedrole.Trim() == "StoreKeeper(HeadOffice)")
            {
                acknowledgeMenuItems = _dapperrepo.GetAllDispatchPlaceRequisition(branchid, 0);
            }
            else
            {
                acknowledgeMenuItems = _dapperrepo.GetAllDispatchedRequisitionOfOwnBranch(branchid, userid);
            }

            if (!acknowledgeMenuItems.Any(r => r.id == id))
            {
                return RedirectToAction("Index", "Error");
            }

            var record = _unitofWork.InRequisitionMessage.GetById(id);
            if (record != null)
            {
                record.indispList = _unitofWork.InDispatch.GetAllByDispMesId(record.dispatchedid);
                return View(record);
            }

            TempData["Fail"] = "Invalid requisition.";
            TempData["Title"] = "<strong>Error!</strong><br />";
            TempData["Icon"] = "fa fa-exclamation-circle";
            return RedirectToAction("Index", "Home");
        }

    }
}