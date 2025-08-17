namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_SALES
    {
        [Key]
        public int s_id { get; set; }

        [Required]
        [StringLength(50)]
        public string s_prod_id { get; set; }

        public int s_qty { get; set; }

        [Column(TypeName = "money")]
        public decimal s_rate { get; set; }

        [Column(TypeName = "money")]
        public decimal? s_discount { get; set; }

        [Column(TypeName = "money")]
        public decimal s_amt { get; set; }

        public int msg_id { get; set; }

        [StringLength(1)]
        public string sales_type { get; set; }

        [StringLength(50)]
        public string s_sn_from { get; set; }

        [StringLength(50)]
        public string s_sn_to { get; set; }

        [StringLength(10)]
        public string s_batch { get; set; }

        [Required]
        [StringLength(50)]
        public string entered_by { get; set; }

        public DateTime entered_date { get; set; }

        public int from_branch { get; set; }

        public int to_branch { get; set; }

        public virtual IN_Requisition_Message IN_Requisition_Message { get; set; }
    }
}
