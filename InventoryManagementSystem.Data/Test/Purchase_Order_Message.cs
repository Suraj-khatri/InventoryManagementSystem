namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchase_Order_Message
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchase_Order_Message()
        {
            Purchase_Order = new HashSet<Purchase_Order>();
            Purchase_Order_Message_History = new HashSet<Purchase_Order_Message_History>();
            Purchase_Order_Received = new HashSet<Purchase_Order_Received>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(200)]
        public string order_no { get; set; }

        [Column(TypeName = "date")]
        public DateTime order_date { get; set; }

        public int vendor_code { get; set; }

        [Required]
        [StringLength(1000)]
        public string remarks { get; set; }

        public int forwarded_to { get; set; }

        [Column(TypeName = "money")]
        public decimal vat_amt { get; set; }

        public int? created_by { get; set; }

        public DateTime? created_date { get; set; }

        public int? approved_by { get; set; }

        public DateTime? approved_date { get; set; }

        public int? received_by { get; set; }

        public DateTime? received_date { get; set; }

        [StringLength(500)]
        public string received_desc { get; set; }

        public int? cancelled_by { get; set; }

        public DateTime? cancelled_date { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        public int? modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        public DateTime? forwarded_date { get; set; }

        public int branch_id { get; set; }

        public int department_id { get; set; }

        public string prod_specfic { get; set; }

        public string appropiate_cond { get; set; }

        [Column(TypeName = "date")]
        public DateTime delivery_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_Order> Purchase_Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_Order_Message_History> Purchase_Order_Message_History { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase_Order_Received> Purchase_Order_Received { get; set; }
    }
}
