using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
    public class InterBranchTransferACKVM
    {
        public int Id { get; set; } // Primary Key, Identity(1,1)
        public string FromBranch { get; set; }
        public string ToBranch { get; set; }
        public int GroupId { get; set; }
        public string TransferedBy { get; set; } // varchar(10)
        public string TransferedTo { get; set; } // varchar(10)
        public string Narration { get; set; } // varchar(200)
        public string ToDepartment { get; set; }
        public string FromDepartment { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TransferedDate { get; set; } // date
        public string TransferType { get; set; } // varchar(50)
        public DateTime? AckDate { get; set; } // date, nullable
        public string Status { get; set; } // varchar(20)

        ///////////////////////////////////////
        public int Tid { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } // varchar(100)
        public int Qty { get; set; }
        public int Stock { get; set; }
        public string IsSerial { get; set; } // varchar(5)
        public decimal Rate { get; set; } // money
        public string Unit { get; set; } // varchar(10)
        public int Remain { get; set; }
        public long SerialNoFrom { get; set; }
        public long SerialNoTo { get; set; }
        public int Inter_Branch_TransferId { get; set; }

        public List<InterBranchTransferACKVM> InterBranchTransferACKVMs { get; set; }
    }
}
