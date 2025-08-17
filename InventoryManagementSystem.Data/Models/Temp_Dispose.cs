namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temp_Dispose
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Amount { get; set; }

        public int? DisposedMessageId { get; set; }
    }
}
