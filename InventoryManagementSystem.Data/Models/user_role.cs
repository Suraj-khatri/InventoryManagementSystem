namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user_role
    {
        [Key]
        public int row_id { get; set; }

        public int user_id { get; set; }

        public int role_id { get; set; }
        public int? Department_id { get; set; }
        public string Remarks { get; set; }

        public virtual Admins Admins { get; set; }
    }
}
