namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_RECEIVED
    {
        public int id { get; set; }

        public int received_msg_id { get; set; }

        public int product_id { get; set; }

        public int received_qty { get; set; }

        [StringLength(50)]
        public string unit { get; set; }
    }
}
