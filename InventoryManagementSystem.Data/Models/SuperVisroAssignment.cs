namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuperVisroAssignment")]
    public partial class SuperVisroAssignment
    {
        [Key]
        public int SV_ASSIGN_ID { get; set; }

        public int? BRANCH { get; set; }

        public int? DEPT { get; set; }

        public int? EMP { get; set; }

        public int? SUPERVISOR { get; set; }

        [StringLength(1)]
        public string SUPERVISOR_TYPE { get; set; }

        [StringLength(1)]
        public string OPERATION_TYPE { get; set; }

        public int? CREATED_BY { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATED_DATE { get; set; }

        public int? MODIFY_BY { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MODIFY_DATE { get; set; }

        [StringLength(1)]
        public string record_status { get; set; }

        public int? POSITION { get; set; }

        public int? EMP_TYPE { get; set; }
    }
}
