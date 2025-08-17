using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class TempReturnProductVM
    {
        public int id { get; set; }
        public int? productid { get; set; }

        [StringLength(200)]
        public string sn_from { get; set; }

        [StringLength(200)]
        public string sn_to { get; set; }
        public int? qty { get; set; }
    }
}
