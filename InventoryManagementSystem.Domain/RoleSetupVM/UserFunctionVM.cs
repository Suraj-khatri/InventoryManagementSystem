using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.RoleSetupVM
{
   public class UserFunctionVM
    {
        public int sno { get; set; }

        [Required]
        [StringLength(50)]
        public string function_name { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string link_file { get; set; }

        [StringLength(50)]
        public string main_menu { get; set; }

        public int dis_order { get; set; }

        [StringLength(10)]
        public string menu_group { get; set; }

        [StringLength(100)]
        public string Icon { get; set; }

        public bool IsActive { get; set; }

        [StringLength(100)]
        public string ControllerName { get; set; }
        public int? ParentId { get; set; }
        public UserFunctionVM Parent { get; set; }
        public List<UserFunctionVM> Child { get; set; }
    }
}
