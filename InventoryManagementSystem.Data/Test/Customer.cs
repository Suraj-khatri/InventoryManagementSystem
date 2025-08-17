namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Vendor_Bid_Price = new HashSet<Vendor_Bid_Price>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(200)]
        public string CustomerTelNo { get; set; }

        [StringLength(20)]
        public string CustomerTelNoSec { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerPANNo { get; set; }

        [StringLength(20)]
        public string CustomeFax { get; set; }

        [StringLength(200)]
        public string ContactPersonFirst { get; set; }

        [StringLength(100)]
        public string ContactPersonMobile1 { get; set; }

        [StringLength(50)]
        public string ContactPersonEmail1 { get; set; }

        [StringLength(50)]
        public string ContactPersonSec { get; set; }

        [StringLength(20)]
        public string ContactPersonMobile2 { get; set; }

        [StringLength(50)]
        public string ContactPersonEmail2 { get; set; }

        [StringLength(50)]
        public string ContactPersonThird { get; set; }

        [StringLength(20)]
        public string ContactPersonMobile3 { get; set; }

        [StringLength(50)]
        public string ContactPersonEmail3 { get; set; }

        [StringLength(50)]
        public string CustomerEmail { get; set; }

        [StringLength(50)]
        public string CustomerWebsite { get; set; }

        public string BusinessDetails { get; set; }

        public string FacilityDetails { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        [StringLength(500)]
        public string CUSTOMERMOBILENO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vendor_Bid_Price> Vendor_Bid_Price { get; set; }
    }
}
