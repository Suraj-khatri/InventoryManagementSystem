using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
    public class NotificationUserVM
    {
        public int Id { get; set; }

        public DateTime? ReadDate { get; set; }

        public int NotificationId { get; set; }

        public int EmployeeId { get; set; }

        public virtual EmployeeVM Employees { get; set; }

        public virtual NotificationVM Notifications { get; set; }
    }
}
