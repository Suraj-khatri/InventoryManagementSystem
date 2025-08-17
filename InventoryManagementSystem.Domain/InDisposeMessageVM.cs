using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
    public class InDisposeMessageVM
    {
        public int Id { get; set; }

        public int ForwardedForApproval { get; set; }

        [Required]
        [StringLength(200)]
        public string DisposeReason { get; set; }

        public int? RequestBy { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Request Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? RequestDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModidiedDate { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public int? RejectedBy { get; set; }

        public DateTime? RejectionDate { get; set; }

        public int? DisposedBy { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? DisposedDate { get; set; }

        public int? DisposingBranchId { get; set; }

        public int? DisposingDepartmentId { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? VatAmount { get; set; }
        public int ProductId { get; set; }
        public string RequestByName { get; set; }
        public string ApproveByName { get; set; }
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public List<SelectListItem> ProductNameList { set; get; }
        public List<SelectListItem> EmployeeNameList { set; get; }
        public List<InDisposeDetailsVM> disposedetailsList { set; get; }

    }
}
