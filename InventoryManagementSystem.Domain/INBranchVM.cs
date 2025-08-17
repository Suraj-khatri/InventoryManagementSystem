using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
    public class INBranchVM
    {
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("Sales  A/C")]
        public string SALES_AC { get; set; }

        [StringLength(50)]
        [DisplayName("Purchase A/C")]
        public string PURCHASE_AC { get; set; }

        [StringLength(50)]
        [DisplayName("Inventory  A/C")]
        public string INVENTORY_AC { get; set; }

        [StringLength(50)]
        [DisplayName("Commission A/C ")]
        public string COMM_AC { get; set; }

        public int BRANCH_ID { get; set; }
        public int ProdGrpId { get; set; }
        public int PRODUCT_ID { get; set; }
        [DisplayName("Is Active")]
        public bool IS_ACTIVE { get; set; }
        [DisplayName("Re-Order Level")]
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
        [DisplayName("Re-Order QTY")]
        public int? REORDER_QTY { get; set; }
        [DisplayName("Max Holding QTY")]
        public int? MAX_HOLDING_QTY { get; set; }
        public int? ProductGroupId { get; set; }
        public int? departmentid { get; set; }

        public string ProductName { get; set; }
        public string Unit { get; set; }
        public string BRANCH_NAME { get; set; }
        public string ProductGroupName { get; set; }
        public virtual BranchVM Branches { get; set; }
        public virtual In_ProductVM INPRODUCTs { get; set; }
        public List<SelectListItem> BranchList { set; get; }
        public List<INBranchVM> INBranchVMList { get; set; }
        public List<In_ItemVM> INItemVMList { get; set; }
        public List<SelectListItem> ProductGroupList { set; get; }
    }
}
