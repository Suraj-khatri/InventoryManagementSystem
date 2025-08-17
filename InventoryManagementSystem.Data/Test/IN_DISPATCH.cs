namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_DISPATCH
    {
        public int id { get; set; }

        public int dispatch_msg_id { get; set; }

        public int product_id { get; set; }

        public int dispatched_qty { get; set; }

        public int received_qty { get; set; }

        public int remain { get; set; }

        public int from_branch { get; set; }

        public int to_branch { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dispatched_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? received_date { get; set; }

        [Column(TypeName = "money")]
        public decimal? rate { get; set; }
    }
}
