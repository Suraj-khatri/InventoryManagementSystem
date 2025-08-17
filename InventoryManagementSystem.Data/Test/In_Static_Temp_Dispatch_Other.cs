namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class In_Static_Temp_Dispatch_Other
    {
        public int Id { get; set; }

        public int? InStaticTempDispatchId { get; set; }

        public int? snf { get; set; }

        public int? snt { get; set; }

        public int? ProductId { get; set; }

        public int? Qty { get; set; }
    }
}
