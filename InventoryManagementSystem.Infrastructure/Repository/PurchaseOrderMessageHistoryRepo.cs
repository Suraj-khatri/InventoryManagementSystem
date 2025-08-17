using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Data.Entity;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class PurchaseOrderMessageHistoryRepo : IPurchaseOrderMessageHistoryRepo
    {
        private Entities _context;

        public PurchaseOrderMessageHistoryRepo()
        {
            _context = new Entities();
        }
        public PurchaseOrderMessageHistoryRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public void Insert(PurchaseOrderMessageHistoryVM data)
        {
            _context.Purchase_Order_Message_History.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(PurchaseOrderMessageHistoryVM data)
        {
            Purchase_Order_Message_History record = Mapper.Convert(data);
            Purchase_Order_Message_History existing = _context.Purchase_Order_Message_History.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Purchase_Order_Message_History.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
