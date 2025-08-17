using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain.FixedAssetsViewModel
{
   public class AssetBranchVM
    {
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("Asset A/C")]
        public string ASSET_AC { get; set; }

        [StringLength(50)]
        [DisplayName("Depreciation Expence A/C")]
        public string DEPRECIATION_EXP_AC { get; set; }

        [StringLength(50)]
        [DisplayName("Accumulated Depreciation A/C")]
        public string ACCUMULATED_DEP_AC { get; set; }

        [StringLength(50)]
        [DisplayName("Write-off A/C")]
        public string WRITE_OFF_EXP_AC { get; set; }

        [StringLength(50)]
        [DisplayName("Sales Profit/Loss A/C")]
        public string SALES_PROFIT_LOSS_AC { get; set; }

        [StringLength(50)]
        [DisplayName("Maintainance Expence A/C")]
        public string MAINTAINANCE_EXP_AC { get; set; }

        public int BRANCH_ID { get; set; }

        public int PRODUCT_ID { get; set; }
        [DisplayName("Is Active")]
        public bool IS_ACTIVE { get; set; }

        [StringLength(20)]
        public string ASSET_NEXT_NUM { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }
        public string BranchName { get; set; }
        public string ProductName { get; set; }
        public virtual BranchVM Branches { get; set; }
        public virtual AssetProductVM AssetProduct { get; set; }
        public List<SelectListItem> BranchList { set; get; }
        public IEnumerable<AssetBranchVM> AssetBranchVMList { get; set; }
        public List<SelectListItem> AssetGroupList { set; get; }
    }
}
