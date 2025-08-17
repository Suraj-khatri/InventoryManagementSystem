namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ASSET_ITEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASSET_ITEM()
        {
            ASSET_PRODUCT = new HashSet<ASSET_PRODUCT>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(500)]
        public string item_name { get; set; }

        [Required]
        [StringLength(500)]
        public string item_desc { get; set; }

        [StringLength(50)]
        public string Product_Code { get; set; }

        public int? parent_id { get; set; }

        public bool is_product { get; set; }

        public double? depre_pct { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public bool Is_Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASSET_PRODUCT> ASSET_PRODUCT { get; set; }
    }
}
