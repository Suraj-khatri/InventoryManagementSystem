using InventoryManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain.RoleSetupVM
{
   public class UserRoleVM
    {
        public int row_id { get; set; }

        public int user_id { get; set; }

        public int role_id { get; set; }
        public int? Department_id { get; set; }
        public string DepartmentName { get; set; }
        public string AssignedRole { get; set; }
        public int AdminId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool UserStatus { get; set; }
        public string Remarks { get; set; }
        public virtual AdminVM Admins { get; set; }
        public List<UserRoleVM> UserRoeVMList { get; set; }
        public List<SelectListItem> UserRoleList { get; set; }
        public List<SelectListItem> BranchList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
    }
}
