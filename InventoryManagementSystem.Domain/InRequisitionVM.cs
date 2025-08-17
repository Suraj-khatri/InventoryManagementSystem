using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class InRequisitionVM
    {
        public int id { get; set; }

        public int item { get; set; }

        public int quantity { get; set; }
        public decimal p_rate { get; set; }
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string unit { get; set; }

        public int Requistion_message_id { get; set; }

        public int Approved_Quantity { get; set; }

        public int? Delivered_Quantity { get; set; }

        public int? Received_Quantity { get; set; }

        public double? REMAIN { get; set; }

        public string ProductName { get; set; }
        public bool serialstatus { get; set; }
        public int detailid { get; set; }
        public double stockinhand { get; set; }
        public double branchstock { get; set; }
        public virtual InRequisitionMessageVM INRequisitionMessage { get; set; }
    }
}
