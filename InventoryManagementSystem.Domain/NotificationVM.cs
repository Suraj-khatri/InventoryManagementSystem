using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class NotificationVM
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(150)]
        public string ActionType { get; set; }

        [StringLength(150)]
        public string Subject { get; set; }

        [StringLength(150)]
        public string Url { get; set; }

        public int SpecialId { get; set; }

        [StringLength(150)]
        public string Icon { get; set; }

        public int? NotificationTypeId { get; set; }

        public virtual NotificationTypeVM NotificationTypes { get; set; }
    }
}
