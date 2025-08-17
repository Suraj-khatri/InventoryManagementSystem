using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
    public class VendorBidPriceVM
    {
        public int id { get; set; }

        public int vendor_id { get; set; }

        public int product_id { get; set; }

        [Column(TypeName = "money")]
        [DisplayName("Amount")]
        public decimal price { get; set; }
        [DisplayName("Is Active")]
        public bool is_active { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsRowCheck { get; set; }
        public virtual CustomerVM Customers { get; set; }
        public virtual In_ProductVM INPRODUCTs { get; set; }
        public List<SelectListItem> VendorList { set; get; }
        public IEnumerable<VendorBidPriceVM> VendorBidPriceVMList { get; set; }
    }
}
