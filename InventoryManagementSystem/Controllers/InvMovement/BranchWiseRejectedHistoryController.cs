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

namespace InventoryManagementSystem.Controllers.InvMovement
{
    [UserAuthorize]
    public class BranchWiseRejectedHistoryController : Controller
    {
        // GET: BranchWiseRejectedHistory
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public BranchWiseRejectedHistoryController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }

        [AllowAnonymous]
        [UserAuthorize(menuId:1093)]
        public ActionResult Index()
        {
            var record = new InRequisitionMessageVM();
            record.BranchNameList = _dropDownList.BranchList();
            record.branch_id = Convert.ToInt32(Session["BranchId"]);
            record.InRequisitionMessageVMList = _dapperrepo.GetAllBranchWiseRejectedHistory(record.branch_id);
            return View(record);
        }

        [HttpPost]
        public ActionResult Index(InRequisitionMessageVM data)
        {
            data.BranchNameList = _dropDownList.BranchList();
            data.InRequisitionMessageVMList = _dapperrepo.GetAllBranchWiseRejectedHistory(data.branch_id);
            return View(data);
        }
    }
}