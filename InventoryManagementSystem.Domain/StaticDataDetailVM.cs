using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
  public  class StaticDataDetailVM
    {
        public int ROWID { get; set; }

        public int TYPE_ID { get; set; }

        [Required]
        [DisplayName("Title")]
        public string DETAIL_TITLE { get; set; }

        [Required]
        [DisplayName("Description")]
        public string DETAIL_DESC { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(50)]
        public string MODIFIED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(50)]
        public string IS_DELETE { get; set; }

        [StringLength(10)]
        public string add_PF { get; set; }

        [StringLength(20)]
        public string value { get; set; }

        [StringLength(2)]
        public string applyOT { get; set; }

        [StringLength(20)]
        public string CEAvalue { get; set; }

        public bool isdistrict { get; set; }

        public bool iszone { get; set; }

        public bool isdepartment { get; set; }
        public bool IsActive { get; set; }
        public virtual StaticDataTypeVM StaticDataType { get; set; }
    }
}
