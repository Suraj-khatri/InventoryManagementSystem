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
    public class AssetRequisitionController : Controller
    {
        // GET: AssetRequisition
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        public AssetRequisitionController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Index()
        {
            var list = _unitofWork.AssetRequisitionMessage.GetAll();
            return View(list);
        }
        public ActionResult Create()
        {
            var record = new AssetRequisitionMessageVM();
            record.AssetProductList = _dropDownList.AssetProductList();
            record.PriorityList = _dropDownList.PriorityList();
            record.BranchNameList = _dropDownList.BranchList();
            record.BranchNameList.Remove(record.BranchNameList.First());
            record.EmployeeList = _dropDownList.EmployeeList();
            return View(record);
        }
    }
}