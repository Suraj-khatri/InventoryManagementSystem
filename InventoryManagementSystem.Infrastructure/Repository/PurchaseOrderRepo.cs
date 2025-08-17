using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class PurchaseOrderRepo : IPurchaseOrderRepo
    {
        private Entities _context;

        public PurchaseOrderRepo()
        {
            _context = new Entities();
        }
        public PurchaseOrderRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.Purchase_Order.Remove(Mapper.Convert(GetById(id)));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<PurchaseOrderVM> GetAll()
        {
            return Mapper.Convert(_context.Purchase_Order.ToList());
        }

        public PurchaseOrderVM GetById(int id)
        {
            return Mapper.Convert(_context.Purchase_Order.Find(id));
        }
        public List<PurchaseOrderVM> GetItemById(int id)
        {
            return Mapper.Convert(_context.Purchase_Order.Where(x => x.order_message_id == id).ToList());
        }
        public List<PurchaseOrderVM> GetItemByBillId(int id)
            {
            using (var _context = new Entities())
            {
                var record = _context.Purchase_Order.Where(x => x.bill_id == id).ToList();
                return record.Select(r => Mapper.Convert(r)).ToList();
            }
        }

        public void Insert(PurchaseOrderVM data)
        {
            _context.Purchase_Order.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(PurchaseOrderVM data)
        {
            Purchase_Order record = Mapper.Convert(data);
            Purchase_Order existing = _context.Purchase_Order.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Purchase_Order.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
