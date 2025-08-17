namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class In_PurchaseReturn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public In_PurchaseReturn()
        {
            In_PurchaseReturnDetails = new HashSet<In_PurchaseReturnDetails>();
        }

        public int Id { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Vat { get; set; }

        [Column(TypeName = "money")]
        public decimal GrandTotal { get; set; }

        public int Bill_Id { get; set; }

        [Required]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<In_PurchaseReturnDetails> In_PurchaseReturnDetails { get; set; }
    }
}
