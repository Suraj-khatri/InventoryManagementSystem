namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase_Order_Message_History
    {
        public int id { get; set; }

        public int Ord_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Requ_date { get; set; }

        [Required]
        [StringLength(100)]
        public string from_user { get; set; }

        [Required]
        [StringLength(100)]
        public string to_user { get; set; }

        [StringLength(100)]
        public string narration { get; set; }

        public virtual Purchase_Order_Message Purchase_Order_Message { get; set; }
    }
}
