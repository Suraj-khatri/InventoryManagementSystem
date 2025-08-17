namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_PURCHASE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IN_PURCHASE()
        {
            In_Purchase_Department = new HashSet<In_Purchase_Department>();
        }

        [Key]
        public int pur_id { get; set; }

        public int? bill_id { get; set; }

        public int prod_code { get; set; }

        public int p_qty { get; set; }

        [Column(TypeName = "money")]
        public decimal p_rate { get; set; }

        [StringLength(50)]
        public string p_sn_from { get; set; }

        [StringLength(50)]
        public string p_sn_to { get; set; }

        [StringLength(10)]
        public string p_batch { get; set; }

        public int p_stk_remain { get; set; }

        public DateTime? p_expiry { get; set; }

        [StringLength(50)]
        public string entered_by { get; set; }

        public DateTime? entered_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        public int branch_id { get; set; }

        public int? req_id { get; set; }

        public int? foc_qty { get; set; }

        public int? order_msg_id { get; set; }

        public int? departmentid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<In_Purchase_Department> In_Purchase_Department { get; set; }
    }
}
