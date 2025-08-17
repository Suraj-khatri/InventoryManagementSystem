namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temp_Purchase_Other
    {
        public int id { get; set; }

        public int? temp_purchase_id { get; set; }

        [Required]
        [StringLength(200)]
        public string sn_from { get; set; }

        [Required]
        [StringLength(200)]
        public string sn_to { get; set; }

        [StringLength(200)]
        public string batch { get; set; }

        public int qty { get; set; }

        [StringLength(1)]
        public string is_approved { get; set; }

        [Required]
        [StringLength(200)]
        public string session_id { get; set; }

        public bool IsActive { get; set; }
    }
}
