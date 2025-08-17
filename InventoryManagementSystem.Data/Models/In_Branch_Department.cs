namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class In_Branch_Department
    {
        public int Id { get; set; }

        public int InBranchId { get; set; }

        public int BranchId { get; set; }

        public int DepartmentId { get; set; }

        public int ProductId { get; set; }

        public int StockInHand { get; set; }

        public int IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? CreatedDate { get; set; }

        public virtual IN_BRANCH IN_BRANCH { get; set; }
    }
}
