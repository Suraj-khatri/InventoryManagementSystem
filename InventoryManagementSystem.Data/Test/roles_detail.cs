namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class roles_detail
    {
        [Key]
        public int rowid { get; set; }

        public int role_id { get; set; }

        public int function_id { get; set; }

        public bool IsActive { get; set; }
    }
}
