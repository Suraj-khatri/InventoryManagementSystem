using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class StaticDataTypeVM
    {
        public int ROWID { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Title")]
        public string TYPE_TITLE { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Description")]
        public string TYPE_DESC { get; set; }
        public bool IsActive { get; set; }
    }
}
