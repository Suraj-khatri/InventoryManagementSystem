using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{

    public class GroupWiseProductDetailsListController : Controller
    {
        // GET: GroupWiseProductDetailsList
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public GroupWiseProductDetailsListController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }

        [UserAuthorize(menuId: 79)]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var record = new INBranchVM();
            record.BranchList = _dropDownList.BranchList();
            record.ProductGroupList = _dropDownList.ProductGroupList();
            record.BRANCH_ID = Convert.ToInt32(Session["BranchId"]);
            record.INBranchVMList = _dapperrepo.GetAllBranchAssignGroupWise(0,record.BRANCH_ID);
            return View(record);
        }

        [HttpPost]
        public ActionResult Index(INBranchVM data)
        {
            data.BranchList = _dropDownList.BranchList();
            data.ProductGroupList = _dropDownList.ProductGroupList();
            data.INBranchVMList = _dapperrepo.GetAllBranchAssignGroupWise((int)data.ProductGroupId, data.BRANCH_ID);
            return View(data);
        }
    }
}