namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_Temp_Requisition
    {
        public int id { get; set; }

        public int Item { get; set; }

        [StringLength(50)]
        public string Product_Code { get; set; }

        public int quantity { get; set; }

        [Required]
        [StringLength(50)]
        public string unit { get; set; }

        [Required]
        [StringLength(500)]
        public string session_id { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }
    }
}
