using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.FixedAssetsViewModel
{
   public class AssetItemVM
    {
        public int id { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Asset Group")]
        public string item_name { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Description")]
        public string item_desc { get; set; }

        [StringLength(50)]
        public string Product_Code { get; set; }

        public int? parent_id { get; set; }

        public bool is_product { get; set; }

        [DisplayName("Depreciation Pct.")]
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
    }
}
