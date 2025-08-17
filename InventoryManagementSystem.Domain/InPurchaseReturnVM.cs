using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class InPurchaseReturnVM
    {
        public int Id { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Vat { get; set; }

        [Column(TypeName = "money")]
        public decimal GrandTotal { get; set; }
        public int Bill_Id { get; set; }


        [StringLength(50)]
        public string Bill_No { get; set; }

        public int Vendor_Id { get; set; }

        [StringLength(50)]
        public string PR_No { get; set; }

        [StringLength(100)]
        public string Narration { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string VendorName { get; set; }
        public bool ReturnAll { get; set; }
        public List<InPurchaseReturnDetailsVM> InPurRetDetailList { get; set; }
    }
}
