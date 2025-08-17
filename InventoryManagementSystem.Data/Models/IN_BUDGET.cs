namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_BUDGET
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string FY { get; set; }

        public int? BRANCH_ID { get; set; }

        public int? PRODUCT_ID { get; set; }

        public int? BUDGET_QTY { get; set; }

        [Column(TypeName = "money")]
        public decimal? RATE { get; set; }

        public string REMARKS { get; set; }

        [StringLength(50)]
        public string IS_ACTIVE { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MODIFIED_DATE { get; set; }
    }
}
