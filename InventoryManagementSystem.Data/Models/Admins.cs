namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admins
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admins()
        {
            user_role = new HashSet<user_role>();
        }

        [Key]
        public int AdminID { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserPassword { get; set; }

        [StringLength(50)]
        public string Post { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Cell_phone { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public int Name { get; set; }

        [StringLength(10)]
        public string agent_id { get; set; }

        public bool status { get; set; }

        [StringLength(50)]
        public string LOGINTIMEFROM { get; set; }

        [StringLength(50)]
        public string LOGINTIMETO { get; set; }

        [StringLength(50)]
        public string REPORTACCESS { get; set; }

        [StringLength(100)]
        public string session { get; set; }

        [Required]
        [StringLength(100)]
        public string user_type { get; set; }

        [StringLength(2)]
        public string new_user { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string approved_by { get; set; }

        public DateTime? approved_date { get; set; }

        [StringLength(5)]
        public string branch_level_access { get; set; }

        public DateTime? LastLogin { get; set; }

        public int? pwdChangeDays { get; set; }

        public int? PwdChangeWaringDays { get; set; }

        public DateTime? lastPwdChangedOn { get; set; }

        [StringLength(5)]
        public string forceChangePwd { get; set; }

        public bool IsTemporary { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_role> user_role { get; set; }
    }
}
