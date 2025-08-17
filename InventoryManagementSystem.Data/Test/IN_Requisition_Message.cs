namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_Requisition_Message
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IN_Requisition_Message()
        {
            IN_DISPATCH_MESSAGE = new HashSet<IN_DISPATCH_MESSAGE>();
            IN_RECEIVED_MESSAGE = new HashSet<IN_RECEIVED_MESSAGE>();
            IN_Requisition = new HashSet<IN_Requisition>();
            IN_Requisition_Detail = new HashSet<IN_Requisition_Detail>();
            IN_SALES = new HashSet<IN_SALES>();
        }

        public int id { get; set; }

        public int Requ_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime Requ_date { get; set; }

        [Required]
        [StringLength(500)]
        public string Requ_Message { get; set; }

        public int recommed_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? recommed_date { get; set; }

        public string recommed_message { get; set; }

        public int? Approver_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Approved_Date { get; set; }

        public string Approver_message { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Delivered_Date { get; set; }

        public int? Delivered_By { get; set; }

        public string Delivery_Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Acknowledged_Date { get; set; }

        public int? Acknowledged_By { get; set; }

        public string Acknowledged_Message { get; set; }

        public int Forwarded_To { get; set; }

        public int branch_id { get; set; }

        public int dept_id { get; set; }

        [StringLength(1)]
        public string priority { get; set; }

        [StringLength(50)]
        public string rejected_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? rejected_date { get; set; }

        public string rejected_message { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        [Required]
        [StringLength(10)]
        public string IS_SCHEDULE { get; set; }

        public string UNSCHEDULE_REASON { get; set; }

        [StringLength(50)]
        public string Req_no { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_DISPATCH_MESSAGE> IN_DISPATCH_MESSAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_RECEIVED_MESSAGE> IN_RECEIVED_MESSAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_Requisition> IN_Requisition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_Requisition_Detail> IN_Requisition_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_SALES> IN_SALES { get; set; }
    }
}
