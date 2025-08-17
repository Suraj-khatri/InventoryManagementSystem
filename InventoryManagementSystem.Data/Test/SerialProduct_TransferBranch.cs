namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SerialProduct_TransferBranch
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
