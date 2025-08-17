namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user_function
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user_function()
        {
            user_function1 = new HashSet<user_function>();
        }

        [Key]
        public int sno { get; set; }

        [Required]
        [StringLength(50)]
        public string function_name { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string link_file { get; set; }

        [StringLength(50)]
        public string main_menu { get; set; }

        public int dis_order { get; set; }

        [StringLength(10)]
        public string menu_group { get; set; }

        [StringLength(100)]
        public string Icon { get; set; }

        public bool IsActive { get; set; }

        [StringLength(100)]
        public string ControllerName { get; set; }

        public int? ParentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user_function> user_function1 { get; set; }

        public virtual user_function user_function2 { get; set; }
    }
}
