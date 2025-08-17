namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SerialProductStockHistory")]
    public partial class SerialProductStockHistory
    {
        [Key]
        public int batchNum { get; set; }

        public long? fsequenceNum { get; set; }

        public long? tsequenceNum { get; set; }

        [StringLength(20)]
        public string cardNum { get; set; }

        public int? lastmovementId { get; set; }

        public int? branchId { get; set; }

        public int? customerId { get; set; }

        public DateTime? issuedDate { get; set; }

        [StringLength(50)]
        public string issuedBy { get; set; }

        [StringLength(10)]
        public string status { get; set; }

        public long? purId { get; set; }

        public long? productId { get; set; }

        public long? reqId { get; set; }
    }
}
