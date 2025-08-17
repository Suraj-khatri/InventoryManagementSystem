namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMPANY")]
    public partial class COMPANY
    {
        [Key]
        public int COMP_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string COMP_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string COMP_SHORT_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string COMP_ADDRESS { get; set; }

        [StringLength(50)]
        public string COMP_ADDRESS2 { get; set; }

        [StringLength(50)]
        public string POST_BOX { get; set; }

        [StringLength(50)]
        public string EPS { get; set; }

        [Required]
        [StringLength(50)]
        public string COMP_PHONE_NO { get; set; }

        [StringLength(50)]
        public string COMP_FAX_NO { get; set; }

        [Required]
        [StringLength(50)]
        public string COMP_CONTACT_PERSON { get; set; }

        [Required]
        [StringLength(50)]
        public string COMP_MAP_CODE { get; set; }

        public bool COMP_STATUS { get; set; }

        [Required]
        [StringLength(150)]
        public string COMP_EMAIL { get; set; }

        [Required]
        [StringLength(50)]
        public string COMP_URL { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        public int? BranchId { get; set; }

        public virtual Branches Branches { get; set; }
    }
}
