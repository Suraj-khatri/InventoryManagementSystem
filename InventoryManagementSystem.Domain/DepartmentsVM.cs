using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
  public  class DepartmentsVM
    {
        public int DEPARTMENT_ID { get; set; }

        public int BRANCH_ID { get; set; }

        [Required(ErrorMessage = "Department Short Name is Required")]
        [StringLength(200)]
        [DisplayName("Department Short Name")]
        public string DEPARTMENT_SHORT_NAME { get; set; }

        [StringLength(200)]
        public string DEPARTMENT_NAME { get; set; }

        [StringLength(10)]
        public string PHONE_EXTENSION { get; set; }

        [StringLength(50)]
        public string PHONE { get; set; }

        [StringLength(50)]
        public string FAX { get; set; }

        [StringLength(150)]
        public string EMAIL { get; set; }

        public int? DEPARTMENT_HEAD { get; set; }

        [StringLength(50)]
        public string MOBILE_DEPARTMENT_HEAD { get; set; }

        [StringLength(150)]
        public string EMAIL_DEPARTMENT_HEAD { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }
        public string BranchName { get; set; }
        public int STATIC_ID { get; set; }
        public int ROWID { get; set; }//dept id
        public List<SelectListItem> BranchList { set; get; }
        public List<SelectListItem> DepartmentList { set; get; }
        public List<SelectListItem> DepartmentShortNameList { set; get; }
        public StaticDataDetailVM StaticDataDetail { set; get; }
    }
}
