using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
    public class InReceivedVM
    {
        public int id { get; set; }

        public int received_msg_id { get; set; }

        public int product_id { get; set; }
        public decimal p_rate { get; set; }

        public int received_qty { get; set; }

        [StringLength(50)]
        public string unit { get; set; }

        //public virtual InReceivedMessageVM InReceivedMessage { get; set; }
    }
}
