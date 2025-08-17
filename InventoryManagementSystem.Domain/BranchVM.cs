using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
    public class BranchVM
    {
        public int BRANCH_ID { get; set; }

        public int? COMPANY_ID { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Branch Name")]
        public string BRANCH_NAME { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Branch Short Name")]
        public string BRANCH_SHORT_NAME { get; set; }

        [StringLength(100)]
        public string BRANCH_CITY { get; set; }

        [Required]
        [StringLength(1000)]
        [DisplayName("Address")]
        public string BRANCH_ADDRESS { get; set; }

        [Required]
        [StringLength(1000)]
        [DisplayName("Phone")]
        //[Phone(ErrorMessage = "Please Enter a Valid Phone No.")]
        public string BRANCH_PHONE { get; set; }

        [StringLength(500)]
        [DisplayName("Fax")]
        public string BRANCH_FAX { get; set; }

        [StringLength(50)]
        public string BRANCH_POST_BOX { get; set; }

        [StringLength(50)]
        public string EPS { get; set; }

        [StringLength(50)]
        public string BRANCH_MOBILE { get; set; }

        [StringLength(150)]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string BRANCH_EMAIL { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Country")]
        public string BRANCH_COUNTRY { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Municipality")]
        public string BRANCH_Municipality { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("District")]
        public string BRANCH_DISTRICT { get; set; }

        [StringLength(200)]
        [DisplayName("Contact Person Mobile")]
        public string CONTACT_PERSON { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(20)]
        public string IBT_Account { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("Branch Code")]
        public string Batch_Code { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Province No.")]
        public string BRANCH_GROUP { get; set; }

        [StringLength(100)]
        [DisplayName("Stock A/C")]
        public string stockAc { get; set; }

        [StringLength(100)]
        [DisplayName("Expenses A/C")]
        public string expensesAc { get; set; }

        [StringLength(100)]
        [DisplayName("Stock In Transit A/C")]
        public string transitAc { get; set; }
        [DisplayName("Is Direct Expense")]
        public bool isDirectExp { get; set; }
        [DisplayName("Is Active")]
        public bool Is_Active { get; set; }
        [DisplayName("Extension No.")]
        public int? ExtNo { get; set; }
        public List<SelectListItem> CountryList { set; get; }
        public List<SelectListItem> ProvinceList { set; get; }
        public List<SelectListItem> MunicipalityList { set; get; }
        public List<SelectListItem> DistrictList { set; get; }

    }
}
