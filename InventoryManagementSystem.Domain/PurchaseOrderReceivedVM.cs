using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Domain
{
    public class PurchaseOrderReceivedVM
    {
        public int Id { get; set; }

        public int Order_Message_Id { get; set; }

        public int Product_Id { get; set; }

        public int Qty { get; set; }

        [Column(TypeName = "money")]
        public decimal Rate { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public int? Received_By { get; set; }

        public DateTime? Received_Date { get; set; }

        public int? foc_qty { get; set; }

        public virtual PurchaseOrderMessageVM PurchaseOrderMessages { get; set; }
    }
}
