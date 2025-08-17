using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Domain
{
    public class OtherBillsInfoVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Bill No")]
        public string billno { get; set; }

        [DisplayName("Bill Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime bill_date { get; set; }

        [Column(TypeName = "money")]
        public decimal? vat_amt { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Bill Amount")]
        public decimal? bill_amount { get; set; }

        [Column(TypeName = "money")]
        public decimal? bill_discount { get; set; }

        [Required]
        [DisplayName("Bill Notes")]
        public string bill_notes { get; set; }

        [StringLength(50)]
        [DisplayName("Entered By")]
        public string entered_by { get; set; }
        [DisplayName("Entered Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? entered_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string paid_by { get; set; }

        public DateTime? paid_date { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Vendor Name")]
        public string VendorName { get; set; }

        [StringLength(50)]
        public string Invoice { get; set; }

        [StringLength(50)]
        public string Received_By { get; set; }

        public DateTime? Received_Date { get; set; }
        [DisplayName("Is Paid")]
        public bool is_paid { get; set; }
        public bool deletevoucher { get; set; }
    }
}
