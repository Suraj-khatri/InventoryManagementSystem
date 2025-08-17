namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class In_Dispose_Message
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public In_Dispose_Message()
        {
            In_Dispose_Details = new HashSet<In_Dispose_Details>();
        }

        public int Id { get; set; }

        public int ForwardedForApproval { get; set; }

        [Required]
        [StringLength(200)]
        public string DisposeReason { get; set; }

        public int? RequestBy { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModidiedDate { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public int? RejectedBy { get; set; }

        public DateTime? RejectionDate { get; set; }

        public int? DisposedBy { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? DisposedDate { get; set; }

        public int? DisposingBranchId { get; set; }

        public int? DisposingDepartmentId { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? VatAmount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<In_Dispose_Details> In_Dispose_Details { get; set; }
    }
}
