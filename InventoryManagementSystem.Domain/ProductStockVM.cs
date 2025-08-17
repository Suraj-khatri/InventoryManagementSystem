using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
    public class ProductStockVM
    {
        public int BRANCH_ID { get; set; }         // Maps to 'BRANCH_ID'
        public int PRODUCT_ID { get; set; }        // Maps to 'PRODUCT_ID'
        public string BranchName { get; set; }    // Maps to 'BranchName'
        public string ProductName { get; set; }   // Maps to 'ProductName'
        public string package_unit { get; set; }   // Maps to 'package_unit'
        public decimal qty { get; set; }     // Maps to 'qty'
        public decimal rate { get; set; }         // Maps to 'rate'

        public bool isserial { get; set; }        // Maps to 'isserial'
    }

}
