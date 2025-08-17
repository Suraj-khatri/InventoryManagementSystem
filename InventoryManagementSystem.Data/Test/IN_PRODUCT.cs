namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IN_PRODUCT()
        {
            IN_BRANCH = new HashSet<IN_BRANCH>();
            Vendor_Bid_Price = new HashSet<Vendor_Bid_Price>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(500)]
        public string porduct_code { get; set; }

        public int item_id { get; set; }

        [Required]
        [StringLength(500)]
        public string product_desc { get; set; }

        public bool? is_tangible { get; set; }

        public bool is_taxable { get; set; }

        [Required]
        [StringLength(50)]
        public string package_unit { get; set; }

        [StringLength(50)]
        public string single_unit { get; set; }

        [Column(TypeName = "money")]
        public decimal? unit_discount { get; set; }

        [Column(TypeName = "money")]
        public decimal? bulk_discount { get; set; }

        public double? conversion_rate { get; set; }

        [Column(TypeName = "money")]
        public decimal? purchase_base_price { get; set; }

        public double? purchase_tolerence_plus { get; set; }

        public double? purchase_tolerence_minus { get; set; }

        [Column(TypeName = "money")]
        public decimal? sales_base_price { get; set; }

        public double? sales_tolerence_minus { get; set; }

        public double? sales_tolerence_plus { get; set; }

        public double? reorder_level { get; set; }

        public bool batch_condition { get; set; }

        public bool serial_no { get; set; }

        [StringLength(100)]
        public string make { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        public bool is_active { get; set; }

        [StringLength(100)]
        public string ext_fld1 { get; set; }

        [StringLength(100)]
        public string ext_fld2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime created_date { get; set; }

        [Required]
        [StringLength(50)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime modified_date { get; set; }

        [Required]
        [StringLength(50)]
        public string modified_by { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_BRANCH> IN_BRANCH { get; set; }

        public virtual IN_ITEM IN_ITEM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vendor_Bid_Price> Vendor_Bid_Price { get; set; }
    }
}
