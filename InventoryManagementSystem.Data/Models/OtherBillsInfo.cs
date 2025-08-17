namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OtherBillsInfo")]
    public partial class OtherBillsInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string billno { get; set; }

        public DateTime bill_date { get; set; }

        [Column(TypeName = "money")]
        public decimal? vat_amt { get; set; }

        [Column(TypeName = "money")]
        public decimal? bill_amount { get; set; }

        [Column(TypeName = "money")]
        public decimal? bill_discount { get; set; }

        [Required]
        public string bill_notes { get; set; }

        [StringLength(50)]
        public string entered_by { get; set; }

        public DateTime? entered_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string paid_by { get; set; }

        public DateTime? paid_date { get; set; }

        [Required]
        [StringLength(100)]
        public string VendorName { get; set; }

        [StringLength(50)]
        public string Invoice { get; set; }

        [StringLength(50)]
        public string Received_By { get; set; }

        public DateTime? Received_Date { get; set; }

        public bool is_paid { get; set; }

        public bool deletevoucher { get; set; }
    }
}
