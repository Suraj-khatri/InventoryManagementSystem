using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain.ReportingViewModel
{
    public class ReportParameterVM
    {
        public ReportParameterVM()
        {
            DateFrom = DateTime.Now.ToString("yyyy-MM-dd");
            DateTo = DateTime.Now.ToString("yyyy-MM-dd");
        }

        [Display(Name = "From Date")]
        [DataType(DataType.Date)]
        public string DateFrom { get; set; }
        [Display(Name = "To Date")]
        [DataType(DataType.Date)]
        public string DateTo { get; set; }
        public int BillId { get; set; }
        public string BillNo { get; set; }
        public int OrderId { get; set; }

        [Display(Name = "Branch Name")]
        public int BranchId { get; set; }

        [Display(Name = "Department Name")]
        public int DepartmentId { get; set; }

        [Display(Name = "Product Name")]
        public int ProductId { get; set; }

        [Display(Name = "Vendor Name")]
        public int VendorId { get; set; }

        [Display(Name = "Product Group Name")]
        public int ProdGroupId { get; set; }

        [Display(Name = "Fiscal Year")]
        public int FiscalId { get; set; }
        public string BranchName { get; set; }
        public string ProductName { get; set; }
        public string ProductGroupName { get; set; }

        [Display(Name = "User Name")]

        public string UserName { get; set; }

        [Display(Name = "User Access Type")]
        public string UserAccess { get; set; }

        [Display(Name = "From Branch")]
        public int fBranch { get; set; }

        [Display(Name = "To Branch")]
        public int tBranch { get; set; }

        [Display(Name = "Payment Status")]
        public string PaymentStatus { get; set; }

        [Display(Name = "Aquisition Type")]
        public string aquisitiontype { get; set; }

        [Display(Name = "Asset Group Name")]
        public int AssetGroupId { get; set; }

        public int? UserId { get; set; }

        [Display(Name = "Vehicle Category")]
        public string VehicleCategory { get; set; }

        [Display(Name = "Fuel Category")]
        public string FuelCategory { get; set; }

        [Display(Name = "Fuel Status")]
        public string FuelStatus { get; set; }
        public string FuelVendor { get; set; }
        public string CouponNo { get; set; }

        public List<SelectListItem> BranchList { set; get; }
        public List<SelectListItem> DepartmentList { set; get; }
        public List<SelectListItem> ProductList { set; get; }
        public List<SelectListItem> SerialProductList { set; get; }
        public List<SelectListItem> ProductGroupList { set; get; }
        public List<SelectListItem> VendorList { set; get; }
        public List<SelectListItem> FiscalYearList { set; get; }
        public List<SelectListItem> UserAccessList { set; get; }
        public List<SelectListItem> UserNameList { set; get; }
        public List<SelectListItem> PaymentStatusList { set; get; }
        public List<SelectListItem> AquisitionTypeList { set; get; }
        public List<SelectListItem> AssetGroupNameList { set; get; }
        public List<SelectListItem> VehicleCategoryList { set; get; }
        public List<SelectListItem> FuelCategoryList { set; get; }
        public List<SelectListItem> EmployeeList { set; get; }
        public List<SelectListItem> FuelStatusList { set; get; }
        public List<SelectListItem> FuelVendorList { set; get; }
    }
}
