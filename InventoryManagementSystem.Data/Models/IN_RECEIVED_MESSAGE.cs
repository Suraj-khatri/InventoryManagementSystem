namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_RECEIVED_MESSAGE
    {
        public int id { get; set; }

        public int? req_msg_id { get; set; }

        public int? dis_msg_id { get; set; }

        [StringLength(50)]
        public string received_by { get; set; }

        public DateTime received_date { get; set; }

        [Required]
        [StringLength(500)]
        public string received_msg { get; set; }

        public virtual IN_DISPATCH_MESSAGE IN_DISPATCH_MESSAGE { get; set; }

        public virtual IN_Requisition_Message IN_Requisition_Message { get; set; }
    }
}
