namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class In_Purchase_Department
    {
        public int Id { get; set; }

        public int InPurchaseId { get; set; }

        public int? bill_id { get; set; }

        public int branch_id { get; set; }

        public int departmentId { get; set; }

        public int prod_code { get; set; }

        public int p_qty { get; set; }

        public decimal? p_rate { get; set; }

        public int? p_sn_from { get; set; }

        public int? p_sn_to { get; set; }

        public int p_stk_remain { get; set; }

        public int? CreatedBy { get; set; }

        public int? CreatedDate { get; set; }

        public int? order_mes_id { get; set; }

        public int? req_id { get; set; }

        public int? dispose_id { get; set; }

        public virtual IN_PURCHASE IN_PURCHASE { get; set; }
    }
}
