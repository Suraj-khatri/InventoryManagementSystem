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
    public class AssetProductVM
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Asset Type")]
        public string porduct_code { get; set; }

        public int item_id { get; set; }

        [StringLength(500)]
        [DisplayName("Description")]
        public string product_desc { get; set; }

        [StringLength(50)]
        [DisplayName("Asset Code")]
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

        public virtual AssetItemVM AssetItems { get; set; }
    }
}
