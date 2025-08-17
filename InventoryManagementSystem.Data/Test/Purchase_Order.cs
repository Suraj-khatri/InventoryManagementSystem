namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase_Order
    {
        public int id { get; set; }

        public int order_message_id { get; set; }

        public int product_code { get; set; }

        public int qty { get; set; }

        [Column(TypeName = "money")]
        public decimal rate { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        public int? Received_Qty { get; set; }

        public int? bill_id { get; set; }

        public virtual Bill_info Bill_info { get; set; }

        public virtual Purchase_Order_Message Purchase_Order_Message { get; set; }
    }
}
