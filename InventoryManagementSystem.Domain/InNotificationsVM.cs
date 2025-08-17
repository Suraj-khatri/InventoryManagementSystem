using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
    public class InNotificationsVM
    {
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string Subject { get; set; }

        public int Forwardedby { get; set; }

        public int Forwardedto { get; set; }

        [StringLength(150)]
        public string URL { get; set; }

        public DateTime? ReadDate { get; set; }

        public int? SpecialId { get; set; }

        [StringLength(30)]
        public string Icon { get; set; }

        public bool Status { get; set; }
    }
}
