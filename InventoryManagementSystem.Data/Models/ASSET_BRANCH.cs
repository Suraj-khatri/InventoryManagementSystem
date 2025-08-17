namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ASSET_BRANCH
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string ASSET_AC { get; set; }

        [StringLength(50)]
        public string DEPRECIATION_EXP_AC { get; set; }

        [StringLength(50)]
        public string ACCUMULATED_DEP_AC { get; set; }

        [StringLength(50)]
        public string WRITE_OFF_EXP_AC { get; set; }

        [StringLength(50)]
        public string SALES_PROFIT_LOSS_AC { get; set; }

        [StringLength(50)]
        public string MAINTAINANCE_EXP_AC { get; set; }

        public int BRANCH_ID { get; set; }

        public int PRODUCT_ID { get; set; }

        public bool IS_ACTIVE { get; set; }

        [Required]
        [StringLength(20)]
        public string ASSET_NEXT_NUM { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }
    }
}
