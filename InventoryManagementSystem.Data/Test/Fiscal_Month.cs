namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fiscal_Month
    {
        [Key]
        public int rowId { get; set; }

        [StringLength(50)]
        public string nplYear { get; set; }

        [Column(TypeName = "date")]
        public DateTime? engDateBaisakh { get; set; }

        public int? baisakh { get; set; }

        public int? jestha { get; set; }

        public int? ashadh { get; set; }

        public int? shrawan { get; set; }

        public int? bhadra { get; set; }

        public int? ashwin { get; set; }

        public int? kartik { get; set; }

        public int? mangshir { get; set; }

        public int? poush { get; set; }

        public int? magh { get; set; }

        public int? falgun { get; set; }

        public int? chaitra { get; set; }

        [StringLength(4)]
        public string DefaultYr { get; set; }
    }
}
