namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ASSET_PRODUCT
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string porduct_code { get; set; }

        public int item_id { get; set; }

        [StringLength(500)]
        public string product_desc { get; set; }

        [StringLength(50)]
        public string ASSET_CODE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public bool Is_Active { get; set; }

        public virtual ASSET_ITEM ASSET_ITEM { get; set; }
    }
}
