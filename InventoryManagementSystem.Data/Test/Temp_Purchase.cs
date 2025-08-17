namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temp_Purchase
    {
        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string session_id { get; set; }

        public int product_code { get; set; }

        [StringLength(100)]
        public string serial_start { get; set; }

        [StringLength(1000)]
        public string serial_end { get; set; }

        [StringLength(100)]
        public string batch { get; set; }

        public int qty { get; set; }

        [Column(TypeName = "money")]
        public decimal rate { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        [StringLength(200)]
        public string account_no { get; set; }

        [StringLength(10)]
        public string ac_type { get; set; }

        [Column(TypeName = "money")]
        public decimal? ac_amount { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        [StringLength(50)]
        public string created_date { get; set; }

        [StringLength(2)]
        public string flag { get; set; }

        public int? order_message_id { get; set; }

        public int? asset_req_id { get; set; }

        public int? foc_qty { get; set; }

        public bool IsActive { get; set; }
    }
}
