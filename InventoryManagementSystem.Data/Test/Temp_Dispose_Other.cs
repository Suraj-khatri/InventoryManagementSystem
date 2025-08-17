namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temp_Dispose_Other
    {
        public int Id { get; set; }

        public int? TempDisposedId { get; set; }

        public int sn_from { get; set; }

        public int sn_to { get; set; }

        public decimal? Qty { get; set; }
    }
}
