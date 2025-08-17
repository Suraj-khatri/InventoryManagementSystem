using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain
{
    public class CompanyVM
    {
        [Key]
        public int COMP_ID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Company Name")]
        public string COMP_NAME { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Short Name")]
        public string COMP_SHORT_NAME { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Address1")]
        public string COMP_ADDRESS { get; set; }

        [StringLength(50)]
        [DisplayName("Address2")]
        public string COMP_ADDRESS2 { get; set; }

        [StringLength(50)]
        public string POST_BOX { get; set; }

        [StringLength(50)]
        public string EPS { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Phone")]
        [Phone(ErrorMessage = "Please Enter a Valid Phone No.")]
        public string COMP_PHONE_NO { get; set; }

        [StringLength(50)]
        [DisplayName("Fax")]
        public string COMP_FAX_NO { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Contact Person")]
        public string COMP_CONTACT_PERSON { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("PAN / VAT No:")]
        public string COMP_MAP_CODE { get; set; }

        [DisplayName("Status")]
        public bool COMP_STATUS { get; set; }

        [Required]
        [StringLength(150)]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string COMP_EMAIL { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Company URL")]
        public string COMP_URL { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }
        public int? BranchId { get; set; }
        public virtual BranchVM Branches { get; set; }
    }
}
