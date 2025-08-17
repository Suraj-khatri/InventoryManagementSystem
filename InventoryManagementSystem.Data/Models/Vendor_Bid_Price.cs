namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vendor_Bid_Price
    {
        public int id { get; set; }

        public int vendor_id { get; set; }

        public int product_id { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        public bool is_active { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IN_PRODUCT IN_PRODUCT { get; set; }
    }
}
