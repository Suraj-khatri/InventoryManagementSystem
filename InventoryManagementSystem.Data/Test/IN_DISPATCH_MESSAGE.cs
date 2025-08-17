namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_DISPATCH_MESSAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IN_DISPATCH_MESSAGE()
        {
            IN_RECEIVED_MESSAGE = new HashSet<IN_RECEIVED_MESSAGE>();
        }

        public int id { get; set; }

        public int req_id { get; set; }

        [StringLength(500)]
        public string dispatch_message { get; set; }

        [Required]
        [StringLength(50)]
        public string dispatched_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dispatched_date { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        [StringLength(10)]
        public string stkFlag { get; set; }

        public virtual IN_Requisition_Message IN_Requisition_Message { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IN_RECEIVED_MESSAGE> IN_RECEIVED_MESSAGE { get; set; }
    }
}
