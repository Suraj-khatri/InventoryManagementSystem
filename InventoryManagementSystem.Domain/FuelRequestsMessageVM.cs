using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
   public class FuelRequestsMessageVM
    {
        public int Id { get; set; }
        public string FuelRequestNo { get; set; }
        public string Item { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime Requested_Date { get; set; }

        public int Requested_By { get; set; }
      
        [StringLength(500)]

        public string Requested_Message { get; set; }

        public int Recommended_By { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? Recommended_Date { get; set; }
        [StringLength(500)]

        public string Recommended_Message { get; set; }

        public int? Approved_By { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime? Approved_Date { get; set; }
        [StringLength(500)]

        public string Approved_Message { get; set; }
        public int? Rejected_By { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Rejected_Date { get; set; }
        [StringLength(500)]
        public string Rejected_Message { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int Branch_id { get; set; }

        [StringLength(500)]
        public string Coupon_No { get; set; }

        public string BranchName { get; set; }
        public string RequestedBy { get; set; }
        public string RecommendedBy { get; set; }
        public string ApprovedBy { get; set; }
        public int Forwarded_To { get; set; }
        public int priority_id { get; set; } 
        public int vehiclecat_id { get; set; } 
        public int fuelcat_id { get; set; }
        public List<FuelRequestsVM> inreqList { set; get; }
        public List<SelectListItem> SuperVisorList { set; get; }
        public List<SelectListItem> ApproverList { set; get; }
        public List<SelectListItem> BranchNameList { get; set; }  
        public List<SelectListItem> PriorityList { set; get; }
        public List<SelectListItem> VendorList { get; set; }
        public List<SelectListItem> FuelCategoryList { get; set; }
        public List<SelectListItem> VehicleCategoryList { get; set; }
        public List<FuelRequestsVM> FuelRequestsVMList { get; set; }
        public List<FuelRequestsMessageVM> FuelRequestsMessageVMList { get; set; }

    }
}
