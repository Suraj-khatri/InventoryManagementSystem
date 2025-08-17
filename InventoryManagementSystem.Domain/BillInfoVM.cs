using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
    public class BillInfoVM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bill_id { get; set; }
        [StringLength(100)]
        public string party_code { get; set; }
        public int vendor_code { get; set; }
        public int prod_code { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Bill No.")]
        public string billno { get; set; }

        [DisplayName("Bill Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime bill_date { get; set; } = DateTime.Now;

        [Column(TypeName = "money")]
        public decimal? taxable_amt { get; set; }

        [Column(TypeName = "money")]
        public decimal? nontax_amt { get; set; }

        [Column(TypeName = "money")]
        public decimal? vat_amt { get; set; }

        [Column(TypeName = "money")]
        public decimal? bill_amount { get; set; }

        [Column(TypeName = "money")]
        public decimal? bill_discount { get; set; }

        [Column(TypeName = "money")]
        public decimal? paid_amount { get; set; }

        [StringLength(1)]
        public string bill_type { get; set; }

        public DateTime? last_paid_date { get; set; }

        [Required]
        [DisplayName("Remarks")]
        public string bill_notes { get; set; }

        [DisplayName("Approve Message")]
        public string Approved_Message { get; set; }

        [StringLength(50)]
        public string entered_by { get; set; }
        public string orderby { get; set; }
        public string paidby { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? entered_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string paid_by { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? paid_date { get; set; }
        public string VendorName { get; set; }

        [DisplayName("Forwarded For Approval To")]
        public int? forwarded_to { get; set; }
        public string forwardedToName { get; set; }
       // public string RequestMessage { get; set; }
        public string PartyName { get; set; }
        public decimal SubTotal { get; set; }
        public int pomid { get; set; }
        public bool IsVatablePO { get; set; }
        public string Status { get; set; }
        public int? branch_id { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public int? CancelledBy { get; set; }
        public string CancelledByName { get; set; }
        public DateTime? CancelledDate { get; set; }
        public List<PurchaseOrderVM> poList { set; get; }
        public List<SelectListItem> VendorList { set; get; }
        public List<SelectListItem> ProductNameFromVendorList { set; get; }
        public List<SelectListItem> ProductList { set; get; }
        public List<SelectListItem> EmployeeList { set; get; }

    }
}
