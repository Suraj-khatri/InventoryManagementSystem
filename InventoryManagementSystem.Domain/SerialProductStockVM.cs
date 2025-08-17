using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class SerialProductStockVM
    {
        public int batchNum { get; set; }

        public long? sequenceNum { get; set; }

        [StringLength(20)]
        public string cardNum { get; set; }

        public int? lastmovementId { get; set; }

        public int? branchId { get; set; }

        public int? customerId { get; set; }

        public DateTime? issuedDate { get; set; }

        [StringLength(50)]
        public string issuedBy { get; set; }

        [StringLength(10)]
        public string status { get; set; }

        public long? purId { get; set; }

        public long? productId { get; set; }

        public long? reqId { get; set; }
        public int? departmentid { get; set; }
        public int? disposemesid { get; set; }
    }
}
