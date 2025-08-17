namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee_Contract
    {
        [Key]
        public int RowID { get; set; }

        public int EMPLOYEE_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Cont_DateFrm { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Cont_DateTo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Created_Date { get; set; }

        [StringLength(50)]
        public string Created_By { get; set; }

        public int branch_id { get; set; }

        public int dept_id { get; set; }

        public int position_id { get; set; }

        public int emp_type { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_date { get; set; }

        [StringLength(200)]
        public string flag { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
