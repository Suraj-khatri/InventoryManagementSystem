using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class StaticTempDispatchOtherVM
    {
        public int Id { get; set; }
        public int? InStaticTempDispatchId { get; set; }
        public int? snf { get; set; }
        public int? snt { get; set; }
        public int? ProductId { get; set; }
        public int? Qty { get; set; }
    }
}
