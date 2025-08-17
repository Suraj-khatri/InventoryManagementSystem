namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_Requisition_Detail
    {
        public int id { get; set; }

        public int item { get; set; }

        public int quantity { get; set; }

        [Required]
        [StringLength(50)]
        public string unit { get; set; }

        public int Requistion_message_id { get; set; }

        public int Approved_Quantity { get; set; }

        public int Delivered_Quantity { get; set; }

        public int Received_Quantity { get; set; }

        public int? remain { get; set; }

        [Required]
        [StringLength(50)]
        public string session_id { get; set; }

        public virtual IN_Requisition_Message IN_Requisition_Message { get; set; }
    }
}
