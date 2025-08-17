namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class emp_log
    {
        public int id { get; set; }

        public int emp_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? effective_date { get; set; }

        public int branch_id { get; set; }

        public int dept_id { get; set; }

        public int position_id { get; set; }

        public int emp_type { get; set; }

        [StringLength(50)]
        public string flag { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_date { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
