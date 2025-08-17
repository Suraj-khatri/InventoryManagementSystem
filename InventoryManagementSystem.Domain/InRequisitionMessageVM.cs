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
   public class InRequisitionMessageVM
    {
        public int id { get; set; }

        public int Requ_by { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Requ_date { get; set; }

        [StringLength(500)]
        [DisplayName("Requested Message")]
        public string Requ_Message { get; set; }

        public int recommed_by { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? recommed_date { get; set; }

        [DisplayName("Recommend Message")]
        public string recommed_message { get; set; }

        public int? Approver_id { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Approved_Date { get; set; }
        [DisplayName("Approved Message")]
        public string Approver_message { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Dispatch Date")]
        public DateTime? Delivered_Date { get; set; }

        public int? Delivered_By { get; set; }

        [DisplayName("Dispatch Message")]
        public string Delivery_Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Acknowledged_Date { get; set; }

        public int? Acknowledged_By { get; set; }

        public string Acknowledged_Message { get; set; }

        public int Forwarded_To { get; set; }

        public int branch_id { get; set; }

        public int dept_id { get; set; }
        public int vendor_id { get; set; }
        public string priority { get; set; }

        [StringLength(50)]
        public string rejected_by { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? rejected_date { get; set; }
        [DisplayName("Reject Message (if any)")]
        public string rejected_message { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        [StringLength(10)]
        public string IS_SCHEDULE { get; set; }
        public string UNSCHEDULE_REASON { get; set; }
        public int prod_code { get; set; }  
        public string prodname { get; set; }
        public int priority_id { get; set; }
        public string RequestingContactNo { get; set; }
        public string RequestingBranch { get; set; }
        public string ForwardedBranch { get; set; }
        public string ForwardedDepartment { get; set; }
        public string RequestedBy { get; set; }
        public string RecommendedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string DispatchedBy { get; set; }
        public string ReceivedBy { get; set; }
        public int dispatchedid { get; set; }
        public string RequestingDepartment { get; set; }
        public string AssignedRole { get; set; }
        [StringLength(50)]
        public string Req_no { get; set; }
        //PO 
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDateOfDelivery { get; set; }
        //[DisplayName("Forwarded For Approval To")]
        //public int forwarded_to { get; set; }
        public string PONotes { get; set; }
        public string POSpecification { get; set; }
        //End
        public int Requistion_message_id { get; set; }
        public int ProdGroupId { get; set; }
        public List<InRequisitionVM> inreqList { set; get; }
        public List<InDispatchedVM> indispList { set; get; }
        public List<InReceivedVM> inrecList { set; get; }
        public List<InRequisitionMessageVM> InRequisitionMessageVMList { set; get; }
        public List<SelectListItem> ProductNameList { set; get; }
        public List<SelectListItem> ProductGroupList { set; get; }
        public List<SelectListItem> BranchNameList { set; get; }
        public List<SelectListItem> DepartmentList { set; get; }
        public List<SelectListItem> VendorList { set; get; }
        public List<SelectListItem> PriorityList { set; get; }
        public List<SelectListItem> EmployeeList { set; get; }
        public List<SelectListItem> SuperVisorList { set; get; }
    }
}
