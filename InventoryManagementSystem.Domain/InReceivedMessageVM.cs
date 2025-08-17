using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class InReceivedMessageVM
    {
        public int id { get; set; }
        public int? req_msg_id { get; set; }

        public int? dis_msg_id { get; set; }

        [Required]
        [StringLength(50)]
        public string received_by { get; set; }

        public DateTime received_date { get; set; }

        [Required]
        [StringLength(500)]
        public string received_msg { get; set; }
    }
}
