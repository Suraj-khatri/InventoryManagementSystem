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
    public class InPurchaseReturnDetailsRepo : IInPurchaseReturnDetailsRepo
    {
        private Entities _context;

        public InPurchaseReturnDetailsRepo()
        {
            _context = new Entities();
        }
        public InPurchaseReturnDetailsRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Insert(InPurchaseReturnDetailsVM data)
        {
            _context.In_PurchaseReturnDetails.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
