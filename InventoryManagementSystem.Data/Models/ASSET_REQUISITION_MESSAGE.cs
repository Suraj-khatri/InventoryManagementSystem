namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ASSET_REQUISITION_MESSAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASSET_REQUISITION_MESSAGE()
        {
            ASSET_REQUISITION = new HashSet<ASSET_REQUISITION>();
        }

        public int id { get; set; }

        public int? approved_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? approved_date { get; set; }

        [StringLength(500)]
        public string approval_message { get; set; }

        public int forwarded_branch { get; set; }

        public int branch_id { get; set; }

        public int dept_id { get; set; }

        [Required]
        [StringLength(20)]
        public string priority { get; set; }

        public int? created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        [Required]
        [StringLength(1000)]
        public string narration { get; set; }

        [Required]
        [StringLength(10)]
        public string status { get; set; }

        public int? modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASSET_REQUISITION> ASSET_REQUISITION { get; set; }
    }
}
