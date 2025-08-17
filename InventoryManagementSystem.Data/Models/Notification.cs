namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Notification()
        {
            NotificationUser = new HashSet<NotificationUser>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationUser> NotificationUser { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}
