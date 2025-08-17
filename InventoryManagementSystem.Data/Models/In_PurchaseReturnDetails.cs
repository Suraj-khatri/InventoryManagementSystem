namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class In_PurchaseReturnDetails
    {
        public int Id { get; set; }

        public int PR_Id { get; set; }

        public int ProductId { get; set; }

        public int ReturnQty { get; set; }

        [Column(TypeName = "money")]
        public decimal Rate { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public decimal? Vat { get; set; }

        public virtual In_PurchaseReturn In_PurchaseReturn { get; set; }
    }
}
