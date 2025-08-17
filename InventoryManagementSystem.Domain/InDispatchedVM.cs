using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class InDispatchedVM
    {
        public int id { get; set; }

        public int dispatch_msg_id { get; set; }

        public int product_id { get; set; }

        public int approveqty { get; set; }
        public int dispatched_qty { get; set; }

        public int received_qty { get; set; }

        public int remain { get; set; }

        public int from_branch { get; set; }

        public int to_branch { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dispatched_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? received_date { get; set; }

        [Column(TypeName = "money")]
        public decimal? rate { get; set; }
        public decimal p_rate { get; set; }

        public string productname { get; set; }
        public string unit { get; set; }
        public bool IsRowCheck { get; set; }
        //public virtual InDispatchedMessageVM InDispatchedMessage { get; set; }
    }
}
