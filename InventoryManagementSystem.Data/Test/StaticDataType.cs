namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StaticDataType")]
    public partial class StaticDataType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StaticDataType()
        {
            StaticDataDetail = new HashSet<StaticDataDetail>();
        }

        [Key]
        public int ROWID { get; set; }

        [Required]
        [StringLength(200)]
        public string TYPE_TITLE { get; set; }

        [Required]
        [StringLength(500)]
        public string TYPE_DESC { get; set; }

        public bool IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaticDataDetail> StaticDataDetail { get; set; }
    }
}
