namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("changesApprovalQueue")]
    public partial class changesApprovalQueue
    {
        [Key]
        public int rowId { get; set; }

        public int dataid { get; set; }

        [Required]
        [StringLength(100)]
        public string tableName { get; set; }

        [Required]
        [StringLength(100)]
        public string identifierField { get; set; }

        [Required]
        [StringLength(6)]
        public string modType { get; set; }

        [StringLength(100)]
        public string description { get; set; }

        public int? functionId { get; set; }

        public int? AddEditfunctionId { get; set; }

        [StringLength(30)]
        public string createdBy { get; set; }

        public DateTime? createdDate { get; set; }

        [StringLength(1)]
        public string approveFlag { get; set; }

        public int? module { get; set; }

        [StringLength(50)]
        public string changeStatus { get; set; }

        [StringLength(50)]
        public string tableDescription { get; set; }

        public string reasonForRejection { get; set; }

        [StringLength(1000)]
        public string editLink { get; set; }

        [StringLength(50)]
        public string forwarded_to { get; set; }

        public DateTime? forwarded_date { get; set; }

        public bool IsActive { get; set; }

        public virtual ASSET_INVENTORY_TEMP ASSET_INVENTORY_TEMP { get; set; }
    }
}
