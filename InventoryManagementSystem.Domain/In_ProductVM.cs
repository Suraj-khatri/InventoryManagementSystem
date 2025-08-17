using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
    public class In_ProductVM
    {
        public int id { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Product Name")]
        public string porduct_code { get; set; }

        public int item_id { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Description")]
        public string product_desc { get; set; }

        public bool? is_tangible { get; set; }
        [DisplayName("Is Taxable")]
        public bool is_taxable { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Package Unit")]
        public string package_unit { get; set; }

        [StringLength(50)]
        [DisplayName("Single Unit")]
        public string single_unit { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Unit Discount")]
        public decimal? unit_discount { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Bulk Discount")]
        public decimal? bulk_discount { get; set; }

        [DisplayName("Conversion Rate")]
        public double? conversion_rate { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Purchase Base Price")]
        public decimal? purchase_base_price { get; set; }

        [DisplayName("Purchase Tolerance (+)")]
        public double? purchase_tolerence_plus { get; set; }

        [DisplayName("Purchase Tolerance (-)")]
        public double? purchase_tolerence_minus { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Sales Base Price")]
        public decimal? sales_base_price { get; set; }

        [DisplayName("Sales Tolerance (-)")]
        public double? sales_tolerence_minus { get; set; }

        [DisplayName("Sales Tolerance (+)")]
        public double? sales_tolerence_plus { get; set; }

        public double? reorder_level { get; set; }
        [DisplayName("Batch Condition")]
        public bool? batch_condition { get; set; }

        [DisplayName("Is Serialized")]
        public bool serial_no { get; set; }

        [StringLength(100)]
        [DisplayName("Make")]
        public string make { get; set; }

        [StringLength(100)]
        [DisplayName("Model")]
        public string model { get; set; }

        [DisplayName("Is Active")]
        public bool is_active { get; set; }

        [StringLength(100)]
        [DisplayName("Extra Field 1")]
        public string ext_fld1 { get; set; }

        [StringLength(100)]
        [DisplayName("Extra Field 2")]
        public string ext_fld2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime created_date { get; set; }

        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime modified_date { get; set; }

        public string modified_by { get; set; }
        public int ProductGroupId { get; set; }
        public List<SelectListItem> ProductGroupList { set; get; }
        public virtual In_ItemVM INITEMVM { get; set; }
    }
}
