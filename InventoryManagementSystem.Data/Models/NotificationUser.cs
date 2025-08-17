namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NotificationUser")]
    public partial class NotificationUser
    {
        public int Id { get; set; }

        public DateTime? ReadDate { get; set; }

        public int NotificationId { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Notification Notification { get; set; }
    }
}
