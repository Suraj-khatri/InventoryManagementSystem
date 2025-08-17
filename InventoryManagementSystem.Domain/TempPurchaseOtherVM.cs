using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain
{
    public class TempPurchaseOtherVM
    {
        public int id { get; set; }

        public int? temp_purchase_id { get; set; }

        [Required]
        [StringLength(200)]
        public string sn_from { get; set; }

        [Required]
        [StringLength(200)]
        public string sn_to { get; set; }

        [StringLength(200)]
        public string batch { get; set; }

        public int qty { get; set; }

        [StringLength(1)]
        public string is_approved { get; set; }

        [StringLength(200)]
        public string session_id { get; set; }
        public bool serialstatus { get; set; }
        public bool IsActive { get; set; }
    }
}
