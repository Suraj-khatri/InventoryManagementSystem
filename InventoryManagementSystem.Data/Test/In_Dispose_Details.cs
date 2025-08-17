namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class In_Dispose_Details
    {
        public int Id { get; set; }

        public int DisposeMessageId { get; set; }

        public int ProductId { get; set; }

        public int RequestedQty { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount { get; set; }

        public int? DisposeQty { get; set; }

        public virtual In_Dispose_Message In_Dispose_Message { get; set; }
    }
}
