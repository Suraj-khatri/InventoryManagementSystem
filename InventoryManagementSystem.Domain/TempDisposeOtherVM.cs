using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class TempDisposeOtherVM
    {
        public int Id { get; set; }
        public int? TempDisposedId { get; set; }
        public int sn_from { get; set; }
        public int sn_to { get; set; }
        public decimal? Qty { get; set; }
    }
}
