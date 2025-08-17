using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class StaticTempDispatchVM
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? Qty { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ProductGroupId { get; set; }
        public decimal Rate { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public bool SerialStatus { get; set; }
        public double StockInHand { get; set; }
    }
}
