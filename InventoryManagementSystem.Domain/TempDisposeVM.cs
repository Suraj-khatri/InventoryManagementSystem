using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class TempDisposeVM
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Amount { get; set; }

        public int? DisposedMessageId { get; set; }
        //other than db
        public string productname { get; set; }
        public string Unit { get; set; }
        public bool SerialStatus { get; set; }
    }
}
