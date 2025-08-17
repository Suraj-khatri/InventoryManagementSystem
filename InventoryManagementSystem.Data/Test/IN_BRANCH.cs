namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IN_BRANCH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IN_BRANCH()
        {
            In_Branch_Department = new HashSet<In_Branch_Department>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string SALES_AC { get; set; }

        [StringLength(50)]
        public string PURCHASE_AC { get; set; }

        [StringLength(50)]
        public string INVENTORY_AC { get; set; }

        [StringLength(50)]
        public string COMM_AC { get; set; }

        public int BRANCH_ID { get; set; }

        public int PRODUCT_ID { get; set; }

        public bool IS_ACTIVE { get; set; }

        public int? REORDER_LEVEL { get; set; }

        public int? EXCESS_LEVEL { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }

        public double stock_in_hand { get; set; }

        public int? REORDER_QTY { get; set; }

        public int? MAX_HOLDING_QTY { get; set; }

        public int? ProductGroupId { get; set; }

        public int? departmentid { get; set; }

        public virtual Branches Branches { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<In_Branch_Department> In_Branch_Department { get; set; }

        public virtual IN_PRODUCT IN_PRODUCT { get; set; }
    }
}
