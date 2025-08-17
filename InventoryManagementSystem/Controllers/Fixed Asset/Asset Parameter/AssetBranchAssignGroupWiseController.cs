using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.Fixed_Asset.Asset_Parameter
{
    [UserAuthorize]
    public class AssetBranchAssignGroupWiseController : Controller
    {
        // GET: AssetBranchAssignGroupWise
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public AssetBranchAssignGroupWiseController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Create()
        {
            var record = new AssetBranchVM();
            record.BranchList = _dropDownList.BranchList();
            record.AssetGroupList = _dropDownList.ProductGroupList();
            record.IS_ACTIVE = true;
            return View(record);
        }
    }
}