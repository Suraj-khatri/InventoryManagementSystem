using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class InPurchaseReturnDetailsVM
    {
        public int Id { get; set; }

        public int PR_Id { get; set; }

        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int ReturnQty { get; set; }

        [Column(TypeName = "money")]
        public decimal Rate { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public decimal? Vat { get; set; }
        public string ProductName { get; set; }
        public virtual InPurchaseReturnVM InPurchaseReturns { get; set; }
    }
}
