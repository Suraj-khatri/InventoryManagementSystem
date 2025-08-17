namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fuel_Requests_Message
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fuel_Requests_Message()
        {
            Fuel_Requests = new HashSet<Fuel_Requests>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FuelRequestNo { get; set; }

        public int Requested_By { get; set; }

        [Column(TypeName = "date")]
        public DateTime Requested_Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Requested_Message { get; set; }

        public int Recommended_By { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Recommended_Date { get; set; }

        [StringLength(500)]
        public string Recommended_Message { get; set; }

        public int? Approved_By { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Approved_Date { get; set; }

        [StringLength(500)]
        public string Approved_Message { get; set; }

        public int? Rejected_By { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Rejected_Date { get; set; }

        [StringLength(500)]
        public string Rejected_Message { get; set; }

        [StringLength(1)]
        public string Priority { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public int Branch_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fuel_Requests> Fuel_Requests { get; set; }
    }
}
