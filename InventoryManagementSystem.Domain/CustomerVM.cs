using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain
{
    public class CustomerVM
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Vendor Name")]
        public string CustomerName { get; set; }

        [Required]
        [DisplayName("Address")]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Tel.No 1")]
        [Phone(ErrorMessage = "Please Enter a Valid Phone No.")]
        public string CustomerTelNo { get; set; }

        [StringLength(20)]
        [DisplayName("Tel.No 2")]
        public string CustomerTelNoSec { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("PAN / VAT No.")]
        public string CustomerPANNo { get; set; }

        [StringLength(20)]
        [DisplayName("Fax Number")]
        public string CustomeFax { get; set; }

        [StringLength(200)]
        [DisplayName("Contact Person-I")]
        public string ContactPersonFirst { get; set; }

        [StringLength(100)]
        [DisplayName("Mobile Number")]
        public string ContactPersonMobile1 { get; set; }

        [StringLength(50)]
        [DisplayName("E-mail")]
        public string ContactPersonEmail1 { get; set; }

        [StringLength(50)]
        [DisplayName("Contact Person-II")]
        public string ContactPersonSec { get; set; }

        [StringLength(20)]
        [DisplayName("Mobile Number")]
        public string ContactPersonMobile2 { get; set; }

        [StringLength(50)]
        [DisplayName("E-mail")]
        public string ContactPersonEmail2 { get; set; }

        [StringLength(50)]
        [DisplayName("Contact Person-III")]
        public string ContactPersonThird { get; set; }

        [StringLength(20)]
        [DisplayName("Mobile Number")]
        public string ContactPersonMobile3 { get; set; }

        [StringLength(50)]
        [DisplayName("E-mail")]
        public string ContactPersonEmail3 { get; set; }

        [StringLength(50)]
        [DisplayName("Email Address")]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [StringLength(50)]
        [DisplayName("Website")]
        public string CustomerWebsite { get; set; }

        [DisplayName("Business Details")]
        public string BusinessDetails { get; set; }

        public string FacilityDetails { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [StringLength(500)]
        public string CUSTOMERMOBILENO { get; set; }
        public string assignedrole { get; set; }
        public List<CustomerVM> CustomerVMList { get; set; }
    }
}
