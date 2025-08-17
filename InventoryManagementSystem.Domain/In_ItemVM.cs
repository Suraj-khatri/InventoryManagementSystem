using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Domain
{
    public class In_ItemVM
    {
        public int id { get; set; }

        [Required]
        [DisplayName("Group Name")]
        public string item_name { get; set; }

        [Required]
        [DisplayName("Description")]
        public string item_desc { get; set; }

        public string Product_Code { get; set; }

        public int? parent_id { get; set; }

        public bool? is_product { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_date { get; set; }

        public string modified_by { get; set; }

        [DisplayName("Is Active")]
        public bool Is_Active { get; set; }
        public int productid { get; set; }
        public bool IsRowCheck { get; set; }
        public int? REORDER_LEVEL { get; set; }
        public int? REORDER_QTY { get; set; }
        public int? MAX_HOLDING_QTY { get; set; }
        public ICollection<In_ProductVM> InProducts { get; set; }

    }
}
