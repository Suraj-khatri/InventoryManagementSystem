using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
  public  class InRequisitionDetailOtherVM
    {
        public int id { get; set; }

        public int detail_id { get; set; }

        [Required]
        [StringLength(200)]
        public string sn_from { get; set; }

        [Required]
        [StringLength(200)]
        public string sn_to { get; set; }

        [StringLength(200)]
        public string batch { get; set; }

        public int qty { get; set; }

        [StringLength(1)]
        public string is_approved { get; set; }

        [StringLength(200)]
        public string session_id { get; set; }

        public int productid { get; set; }
        public string productname { get; set; }
    }
}
