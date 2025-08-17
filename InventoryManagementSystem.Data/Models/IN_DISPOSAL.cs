namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_DISPOSAL
    {
        [Key]
        public int rowl_id { get; set; }

        [StringLength(20)]
        public string disposal_id { get; set; }

        [StringLength(50)]
        public string prod_code { get; set; }

        public int? p_qty { get; set; }

        [Column(TypeName = "money")]
        public decimal? p_rate { get; set; }

        [StringLength(50)]
        public string p_sn_from { get; set; }

        [StringLength(50)]
        public string p_sn_to { get; set; }

        [StringLength(10)]
        public string p_batch { get; set; }

        public DateTime? p_expiry { get; set; }

        [StringLength(50)]
        public string entered_by { get; set; }

        public DateTime? entered_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        public int? branch_id { get; set; }
    }
}
