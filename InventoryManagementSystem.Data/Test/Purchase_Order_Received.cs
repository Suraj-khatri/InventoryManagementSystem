namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase_Order_Received
    {
        public int Id { get; set; }

        public int Order_Message_Id { get; set; }

        public int Product_Id { get; set; }

        public int Qty { get; set; }

        [Column(TypeName = "money")]
        public decimal Rate { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public int? Received_By { get; set; }

        public DateTime? Received_Date { get; set; }

        public int? foc_qty { get; set; }

        public virtual Purchase_Order_Message Purchase_Order_Message { get; set; }
    }
}
