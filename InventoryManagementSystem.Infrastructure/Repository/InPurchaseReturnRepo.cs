using InventoryManagementSystem.Data;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class InPurchaseReturnRepo : IInPurchaseReturnRepo
    {
        private Entities _context;

        public InPurchaseReturnRepo()
        {
            _context = new Entities();
        }
        public InPurchaseReturnRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public void Insert(InPurchaseReturnVM data)
        {
            _context.In_PurchaseReturn.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
