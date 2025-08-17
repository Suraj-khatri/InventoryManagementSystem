using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain.FixedAssetsViewModel
{
  public  class AssetNumberSequenceVM
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string SeqDateFormat { get; set; }

        [StringLength(30)]
        public string NumSequence { get; set; }

        public bool IsSequenceSep { get; set; }

        [Required]
        [StringLength(1)]
        public string SequenceSep { get; set; }

        public bool IsCompShortCode { get; set; }

        public bool IsCompSep { get; set; }

        [Required]
        [StringLength(1)]
        public string CompSeparator { get; set; }

        public bool IsBranchCode { get; set; }

        public bool IsBranchSep { get; set; }

        [Required]
        [StringLength(1)]
        public string BranchSeparator { get; set; }

        public bool IsAssetCode { get; set; }

        public bool IsAssetCodeSep { get; set; }

        [Required]
        [StringLength(1)]
        public string AssetSeparator { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsDateCode { get; set; }
        public List<SelectListItem> SeparatorList { get; set; }
        public List<SelectListItem> DateFormatList { get; set; }
    }
}
