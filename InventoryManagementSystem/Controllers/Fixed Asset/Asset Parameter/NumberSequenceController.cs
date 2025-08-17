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
    public class NumberSequenceController : Controller
    {
        // GET: NumberSequence
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public NumberSequenceController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Create()
        {
            AssetNumberSequenceVM anseq = new AssetNumberSequenceVM();
            anseq.SeparatorList = _dropDownList.SeparatorList();
            anseq.DateFormatList = _dropDownList.DateFormatList();
            return View(anseq);
        }
    }
}