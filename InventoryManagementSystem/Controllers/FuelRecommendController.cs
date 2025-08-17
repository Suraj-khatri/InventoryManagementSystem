using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [UserAuthorize]
    public class FuelRecommendController : Controller
    {
        // GET: FuelApprove
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public FuelRecommendController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId:1099)]
        public ActionResult Index()
        {
            var assignedrole = Session["AssignRole"].ToString();
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            if (assignedrole == "Supervisor" || assignedrole == "StoreKeeper(HeadOffice)" || assignedrole == "Admin_User" || assignedrole.Trim() == "Administrator" || assignedrole.Trim() == "Fuel Manager")
            {
                var list = _dapperrepo.GetAllFuelRequestForRecommendation(0, 0);
                return View(list);
            }
            else
            {
                var list = _dapperrepo.GetAllFuelRequestForRecommendation(userid, branchid);
                return View(list);
            }
        }

        [UserAuthorize(menuId: 1099)]
        public ActionResult ViewRequested(int id=0)
        {
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var record = _unitofWork.FuelRequestMessage.GetById(id);
            record.ApproverList = _dropDownList.ApproverAssignForEmployee(userid);
            record.inreqList = _unitofWork.FuelRequest.GetItemById(id);
            return View(record);
        }
  
    }
}