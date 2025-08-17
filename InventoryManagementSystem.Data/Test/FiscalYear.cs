namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FiscalYear")]
    public partial class FiscalYear
    {
        [Key]
        [Column(Order = 0)]
        public int FISCAL_YEAR_ID { get; set; }

        [StringLength(20)]
        public string FISCAL_YEAR_ENGLISH { get; set; }

        [StringLength(20)]
        public string FISCAL_YEAR_NEPALI { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime EN_YEAR_START_DATE { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime EN_YEAR_END_DATE { get; set; }

        [StringLength(50)]
        public string NP_YEAR_START_DATE { get; set; }

        [StringLength(50)]
        public string NP_YEAR_END_DATE { get; set; }

        public DateTime? CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        public DateTime? MODIFIED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        public bool? FLAG { get; set; }
    }
}
