using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.Fixed_Asset.AssetAquisition
{
    public class InsuranceOfPurchaseOrderController : Controller
    {
        // GET: InsuranceOfPurchaseOrder
        private IUnitOfWork _unitofWork;
        public InsuranceOfPurchaseOrderController()
        {
            _unitofWork = new UnitOfWork();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}