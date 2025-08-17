namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temp_Return_Product
    {
        public int id { get; set; }

        public int? productid { get; set; }

        [StringLength(200)]
        public string sn_from { get; set; }

        [StringLength(200)]
        public string sn_to { get; set; }

        public int? qty { get; set; }
    }
}
