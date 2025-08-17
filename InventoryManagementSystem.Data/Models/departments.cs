namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class departments
    {
        [Key]
        public int DEPARTMENT_ID { get; set; }

        public int BRANCH_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string DEPARTMENT_SHORT_NAME { get; set; }

        [Required]
        [StringLength(200)]
        public string DEPARTMENT_NAME { get; set; }

        [StringLength(10)]
        public string PHONE_EXTENSION { get; set; }

        [StringLength(50)]
        public string PHONE { get; set; }

        [StringLength(50)]
        public string FAX { get; set; }

        [StringLength(150)]
        public string EMAIL { get; set; }

        public int? DEPARTMENT_HEAD { get; set; }

        [StringLength(50)]
        public string MOBILE_DEPARTMENT_HEAD { get; set; }

        [StringLength(150)]
        public string EMAIL_DEPARTMENT_HEAD { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        public int STATIC_ID { get; set; }
    }
}
