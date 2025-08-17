using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
  public  class InTempRequitionVM
    {
        public int id { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Item { get; set; }

        [StringLength(50)]
        public string Product_Code { get; set; }

        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int quantity { get; set; }

        [Column(Order = 3)]
        [StringLength(50)]
        public string unit { get; set; }

        [Column(Order = 4)]
        [StringLength(500)]
        public string session_id { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }
        public string productname { get; set; }
        public bool SerialStatus { get; set; }
    }
}
