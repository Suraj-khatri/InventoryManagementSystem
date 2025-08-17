using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Domain
{
    public class PurchaseOrderMessageHistoryVM
    {
        public int id { get; set; }

        public int Ord_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Requ_date { get; set; }

        [Required]
        [StringLength(100)]
        public string from_user { get; set; }

        [Required]
        [StringLength(100)]
        public string to_user { get; set; }

        [StringLength(100)]
        public string narration { get; set; }

        public virtual PurchaseOrderMessageVM PurchaseOrderMessages { get; set; }
    }
}
