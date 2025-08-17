using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
    public class PurchaseOrderMessageVM
    {
        public int id { get; set; }

        [StringLength(200)]
        public string order_no { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Order Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime order_date { get; set; }
        public string productname { get; set; }
        public string vendorname { get; set; }
        public string approvername { get; set; }
        public string receivername { get; set; }
        public string orderby { get; set; }
        public string forwardedto { get; set; }
        public string approveorforward { get; set; }
        public int prod_code { get; set; }
        public int vendor_code { get; set; }

        [Required]
        [StringLength(1000)]
        public string remarks { get; set; }

        [DisplayName("Forwarded For Approval To")]
        public int forwarded_to { get; set; }

        [Column(TypeName = "money")]
        public decimal vat_amt { get; set; }

        public int? created_by { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? created_date { get; set; }

        public int? approved_by { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? approved_date { get; set; }

        public int? received_by { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? received_date { get; set; }

        [StringLength(500)]
        public string received_desc { get; set; }

        public int? cancelled_by { get; set; }

        public DateTime? cancelled_date { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        public int? modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        public DateTime? forwarded_date { get; set; }

        public int branch_id { get; set; }

        public int temppurchaseid { get; set; }
        public int department_id { get; set; }
        public string prod_specfic { get; set; }
        public string appropiate_cond { get; set; }

        public decimal SubTotal { get; set; }
        public decimal NetAmount { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Last Date of Delivery")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime delivery_date { get; set; }
        public bool IsVatablePO { get; set; }
        public List<int> pCodelist = new List<int>();

        public List<string> pNamelist = new List<string>();
        public List<PurchaseOrderVM> poList { set; get; }
        public List<SelectListItem> VendorList { set; get; }
        public List<SelectListItem> ProductNameFromVendorList { set; get; }
        public List<SelectListItem> EmployeeList { set; get; }
    }
}
