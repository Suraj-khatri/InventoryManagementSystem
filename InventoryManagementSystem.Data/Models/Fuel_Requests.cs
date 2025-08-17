namespace InventoryManagementSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fuel_Requests
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; }

        [StringLength(50)]
        public string Coupon_No { get; set; }

        [StringLength(50)]
        public string Vehicle_Category { get; set; }

        [StringLength(50)]
        public string Fuel_Category { get; set; }

        [StringLength(50)]
        public string Vendor { get; set; }

        [StringLength(50)]
        public string KM_Run { get; set; }

        [StringLength(50)]
        public string Vehicle_No { get; set; }

        public int Fuel_Requests_Message_Id { get; set; }

        public decimal? Requested_Quantity { get; set; }

        public decimal? Recommended_Quantity { get; set; }

        public decimal? Approved_Quantity { get; set; }

        public decimal? Received_Quantity { get; set; }

        [StringLength(50)]
        public string Previous_KM_Run { get; set; }

        public string FilePath { get; set; }



        public virtual Fuel_Requests_Message Fuel_Requests_Message { get; set; }
    }
}
