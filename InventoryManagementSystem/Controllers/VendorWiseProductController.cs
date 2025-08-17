using InventoryManagementSystem.Domain;
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
    public class VendorWiseProductController : Controller
    {
        // GET: VendorWiseProduct
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public VendorWiseProductController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        [UserAuthorize(menuId: 1095)]
        public ActionResult Index()
        {
            var data = new VendorBidPriceVM();
            data.VendorList = _dropDownList.VendorList();
            data.VendorBidPriceVMList = _unitofWork.InVendorAssign.GetAllByVendorId(data.vendor_id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(VendorBidPriceVM data)
        {
            data.VendorList = _dropDownList.VendorList();
            data.VendorBidPriceVMList = _unitofWork.InVendorAssign.GetAllByVendorId(data.vendor_id);
            return View(data);
        }

        [HttpPost]
        public ActionResult UpdateData(VendorBidPriceVM record)
        {
            var itmcount = record.VendorBidPriceVMList.Where(x => x.IsRowCheck == true).Count();
            if (itmcount > 0)
            {
                foreach (var item in record.VendorBidPriceVMList.Where(x => x.IsRowCheck == true && x.id > 0))
                {
                    try
                    {
                        var data = _unitofWork.InVendorAssign.GetById(item.id);
                        data.price = item.price;
                        _unitofWork.InVendorAssign.Update(data);
                        _unitofWork.Save();
                    }
                    catch (Exception Ex)
                    {
                        return Json(new { success = false, err = Ex + "Error Occured !!.", JsonRequestBehavior.AllowGet });
                    }
                }
                return Json(new { success = true, mes = "Save Successfully", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { success = false, err = "Please Select At Least One Product.", JsonRequestBehavior.AllowGet });
            }
        }
    }
}