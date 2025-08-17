using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain.RoleSetupVM
{
   public class RoleDetailsVM
    {
        public RoleDetailsVM()
        {
            RoleDetailsVMList = new List<RoleDetailsVM>();
        }
        public int rowid { get; set; }

        public int role_id { get; set; }

        public int function_id { get; set; }
        public bool IsActive { get; set; }
        public List<SelectListItem> UserRoleList { get; set; }
        public List<UserFunctionVM> UserFunctionVMList { get; set; }
        public List<RoleDetailsVM> RoleDetailsVMList { get; set; }
    }
}
