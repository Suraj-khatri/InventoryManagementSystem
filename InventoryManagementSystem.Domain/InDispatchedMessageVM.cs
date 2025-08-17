using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class InDispatchedMessageVM
    {
        public int id { get; set; }

        public int req_id { get; set; }

        [StringLength(500)]
        public string dispatch_message { get; set; }

        [Required]
        [StringLength(50)]
        public string dispatched_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dispatched_date { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        [StringLength(10)]
        public string stkFlag { get; set; }
    }
}
