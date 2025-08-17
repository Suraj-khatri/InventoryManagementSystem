using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.RoleSetupVM
{
    public class SideMenuVM
    {
        public List<UserFunctionVM> UserFunction { get; set; }
        public List<RoleDetailsVM> RoleDetails { get; set; }
        public List<UserRoleVM> UserRole { get; set; }
    }
}
