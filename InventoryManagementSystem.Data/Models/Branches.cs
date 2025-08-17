namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Branches
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Branches()
        {
            COMPANY = new HashSet<COMPANY>();
            IN_BRANCH = new HashSet<IN_BRANCH>();
        }

        [Key]
        public int BRANCH_ID { get; set; }

        public int? COMPANY_ID { get; set; }

        [Required]
        [StringLength(500)]
        public string BRANCH_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string BRANCH_SHORT_NAME { get; set; }

        [StringLength(100)]
        public string BRANCH_CITY { get; set; }

        [Required]
        [StringLength(1000)]
        public string BRANCH_ADDRESS { get; set; }

        [Required]
        [StringLength(1000)]
        public string BRANCH_PHONE { get; set; }

        [StringLength(500)]
        public string BRANCH_FAX { get; set; }

        [StringLength(50)]
        public string BRANCH_POST_BOX { get; set; }

        [StringLength(50)]
        public string EPS { get; set; }

        [StringLength(50)]
        public string BRANCH_MOBILE { get; set; }

        [StringLength(150)]
        public string BRANCH_EMAIL { get; set; }

        [Required]
        [StringLength(50)]
        public string BRANCH_COUNTRY { get; set; }

        [Required]
        [StringLength(50)]
        public string BRANCH_Municipality { get; set; }

        [Required]
        [StringLength(50)]
        public string BRANCH_DISTRICT { get; set; }

        [StringLength(200)]
        public string CONTACT_PERSON { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(20)]
        public string IBT_Account { get; set; }

        [Required]
        [StringLength(15)]
        public string Batch_Code { get; set; }

        [Required]
        [StringLength(100)]
        public string BRANCH_GROUP { get; set; }

        [StringLength(100)]
        public string stockAc { get; set; }

        [StringLength(100)]
        public string expensesAc { get; set; }

        [StringLength(100)]
        public string transitAc { get; set; }

        public bool isDirectExp { get; set; }

        public bool Is_Active { get; set; }

        public int? ExtNo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMPANY> COMPANY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_BRANCH> IN_BRANCH { get; set; }
    }
}
