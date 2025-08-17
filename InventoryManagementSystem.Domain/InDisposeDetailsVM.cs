using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
  public  class InDisposeDetailsVM
    {
        public int Id { get; set; }

        public int DisposeMessageId { get; set; }

        public int ProductId { get; set; }

        public int RequestedQty { get; set; }

        public decimal Rate { get; set; }

        public decimal Amount { get; set; }

        public int? DisposeQty { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public double StockInHand { get; set; }
        public virtual InDisposeMessageVM InDisposeMessageVM { get; set; }
    }
}
