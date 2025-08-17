using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
   public class SerialProductTransferBranchVM
    {
        public int Id { get; set; }

        public int reqid { get; set; }

        public int sn_from { get; set; }

        public int sn_to { get; set; }

        public int productid { get; set; }

        public int qty { get; set; }

        public int fbranchid { get; set; }

        public int tbranchid { get; set; }

        public DateTime TransferDate { get; set; }
    }
}
