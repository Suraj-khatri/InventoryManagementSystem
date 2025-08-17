namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ASSET_INVENTORY_TEMP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASSET_INVENTORY_TEMP()
        {
            changesApprovalQueue = new HashSet<changesApprovalQueue>();
        }

        public int id { get; set; }

        [StringLength(500)]
        public string asset_number { get; set; }

        public DateTime booked_date { get; set; }

        public DateTime depr_start_date { get; set; }

        public DateTime? purchase_date { get; set; }

        [Column(TypeName = "money")]
        public decimal purchase_value { get; set; }

        [Column(TypeName = "money")]
        public decimal? acc_depriciation { get; set; }

        public string narration { get; set; }

        public int product_id { get; set; }

        public int branch_id { get; set; }

        public int dept_id { get; set; }

        public int? bill_id { get; set; }

        public int? ins_id { get; set; }

        public int? asset_holder { get; set; }

        public DateTime? warr_expiry { get; set; }

        public DateTime? next_maintenance_date { get; set; }

        [Required]
        [StringLength(20)]
        public string asset_status { get; set; }

        [StringLength(200)]
        public string asset_serial { get; set; }

        [StringLength(1)]
        public string in_out { get; set; }

        public double? depre_pct { get; set; }

        public int? receive_id { get; set; }

        public bool is_amortised { get; set; }

        public int? life_in_month { get; set; }

        public int? remain_month { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        public int group_id { get; set; }

        [StringLength(50)]
        public string asset_type { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_date { get; set; }

        [Required]
        [StringLength(50)]
        public string forwarded_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime forwarded_date { get; set; }

        [Required]
        [StringLength(50)]
        public string status { get; set; }

        public bool IsActive { get; set; }

        public string rejection_reason { get; set; }

        [StringLength(50)]
        public string old_asset_no { get; set; }

        [StringLength(50)]
        public string old_asset_code { get; set; }

        [StringLength(200)]
        public string model { get; set; }

        [StringLength(200)]
        public string brand { get; set; }

        public int? VendorId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<changesApprovalQueue> changesApprovalQueue { get; set; }
    }
}
