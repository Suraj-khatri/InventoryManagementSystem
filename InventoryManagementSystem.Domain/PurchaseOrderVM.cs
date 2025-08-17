using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Domain
{
    public class PurchaseOrderVM
    {
        public int id { get; set; }

        public int order_message_id { get; set; }

        public int product_code { get; set; }

        public string productname { get; set; }
        public int qty { get; set; }

        [Column(TypeName = "money")]
        public decimal rate { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }
        public int? Received_Qty { get; set; }
        public bool serialstatus { get; set; }
        public bool IsRowCheck { get; set; }
        public int? bill_id { get; set; }
        public virtual PurchaseOrderMessageVM PurchaseOrderMessages { get; set; }
        public virtual BillInfoVM BillInfoVM { get; set; }
    }
}