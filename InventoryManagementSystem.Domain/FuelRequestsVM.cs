using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class FuelRequestsVM
    {
        public int Id { get; set; }
        public int Item { get; set; }
        [StringLength(20)]
        public string Unit { get; set; }
        [StringLength(50)]
        public string Vehicle_Category { get; set; }
        public string Fuel_Category { get; set; }
        public int Fuel_Requests_Message_Id { get; set; }
        public decimal? Requested_Quantity { get; set; }
        public decimal? Recommended_Quantity { get; set; }
        public decimal? Approved_Quantity { get; set; }
        public decimal? Received_Quantity { get; set; }
        [StringLength(50)]
        public string Vendor { get; set; }

        [StringLength(50)]
        public string KM_Run { get; set; }
        public string FilePath { get; set; }

        [StringLength(50)]
        public string Vehicle_No { get; set; }
        public string Coupon_No { get; set; }
        public string Previous_KM_Run { get; set; }
        public virtual FuelRequestsMessageVM FuelRequestsMessage { get; set; }
        public string ProductName { get; set; }
        public List<FuelRequestsVM> inreqList { get; set; }
        public List<FuelRequestsMessageVM> FuelRequestsMessageVMList { get; set; }
    }
}
