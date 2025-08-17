using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.FixedAssetsViewModel
{
  public  class AssetRequisitionVM
    {
        public int id { get; set; }

        public int asset_id { get; set; }

        public int qty { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        public int requistion_message_id { get; set; }

        public int approved_qty { get; set; }

        public int? order_qty { get; set; }

        public int? received_qty { get; set; }
        public string assettype { get; set; }

        public virtual AssetRequisitionMessageVM AssetRequisitionMessage { get; set; }
    }
}
