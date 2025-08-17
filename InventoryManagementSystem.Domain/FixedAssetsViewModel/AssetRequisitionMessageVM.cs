using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain.FixedAssetsViewModel
{
  public  class AssetRequisitionMessageVM
    {
        public int id { get; set; }

        public int? approved_by { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? approved_date { get; set; }

        [StringLength(500)]
        public string approval_message { get; set; }

        public int forwarded_branch { get; set; }

        public int branch_id { get; set; }

        public int dept_id { get; set; }

        [Required]
        [StringLength(20)]
        public string priority { get; set; }

        public int? created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        [StringLength(1000)]
        public string narration { get; set; }

        [StringLength(10)]
        public string status { get; set; }

        public int? modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_date { get; set; }
        public int asset_code { get; set; }
        public int priority_id { get; set; }
        public string BranchName { get; set; }
        public string ForwardedTo { get; set; }
        public List<AssetRequisitionVM> inreqList { set; get; }
        public List<SelectListItem> AssetProductList { get; set; }
        public List<SelectListItem> PriorityList { get; set; }
        public List<SelectListItem> BranchNameList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
    }
}
