namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bill_info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bill_id { get; set; }

        [StringLength(100)]
        public string party_code { get; set; }

        [Required]
        [StringLength(200)]
        public string billno { get; set; }

        public DateTime bill_date { get; set; }

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
        public string bill_notes { get; set; }

        public string Approved_Message { get; set; }

        [StringLength(50)]
        public string entered_by { get; set; }

        public DateTime? entered_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string paid_by { get; set; }

        public DateTime? paid_date { get; set; }

        public string VendorName { get; set; }

        public int? branch_id { get; set; }
        public int? forwarded_to { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int? CancelledBy { get; set; }
        public DateTime? CancelledDate { get; set; }

        public string Status { get; set; }
    }
}
