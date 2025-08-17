namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ASSET_REQUISITION
    {
        public int id { get; set; }

        public int asset_id { get; set; }

        public int qty { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        public int requistion_message_id { get; set; }

        public int approved_qty { get; set; }

        public int? order_qty { get; set; }

        public int? received_qty { get; set; }

        public virtual ASSET_REQUISITION_MESSAGE ASSET_REQUISITION_MESSAGE { get; set; }
    }
}
