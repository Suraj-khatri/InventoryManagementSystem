using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.RoleSetupVM
{
    public class InsertRolesVM
    {
        public int FunctionId { get; set; }
        public bool IsActive { get; set; }
        public string Fname { get; set; }
        public string ParentName { get; set; }
        public int RoleId { get; set; }
        public int? ParentId { get; set; }
    }
}
