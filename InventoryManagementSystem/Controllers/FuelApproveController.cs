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
    public class FuelApproveController : Controller
    {
        // GET: FuelApprove
        private IUnitOfWork _unitofWork;
        private DapperRepoServices _dapperrepo;
        public FuelApproveController()
        {
            _unitofWork = new UnitOfWork();
           _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId:1100)]
        public ActionResult Index()
        {
            var assignedrole = Session["AssignRole"].ToString();
            var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            var branchid = Convert.ToInt32((Session["BranchId"]));
            if (assignedrole == "StoreKeeper(HeadOffice)" || assignedrole == "Admin_User" || assignedrole.Trim() == "Administrator" || assignedrole.Trim() == "Fuel Manager")
            {
                var list = _dapperrepo.GetAllFuelRecommendationForApproval(0, 0);
                return View(list);
            }
            else
            {
                var list = _dapperrepo.GetAllFuelRecommendationForApproval(userid, branchid);
                return View(list);
            }
        }
        [UserAuthorize(menuId: 1100)]
        public ActionResult ViewRecommended(int id)
        {
            
            var record = _unitofWork.FuelRequestMessage.GetById(id);
            record.inreqList = _unitofWork.FuelRequest.GetItemById(id);
            return View(record);
        }
    }
}