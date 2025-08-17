using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class SerialProductStockRepo : ISerialProductStockRepo
    {
        private Entities _context;

        public SerialProductStockRepo()
        {
            _context = new Entities();
        }
        public SerialProductStockRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.SerialProductStock.Remove(Mapper.Convert(GetById(id)));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public SerialProductStockVM GetById(int id)
        {
            return Mapper.Convert(_context.SerialProductStock.Find(id));
        }

        public SerialProductStockVM GetByPIdAndSequenceNo(int pid,int seqno)
        {
            return Mapper.Convert(_context.SerialProductStock.Where(x=>x.productId==pid && x.sequenceNum==seqno).FirstOrDefault());
        }

        public void Insert(SerialProductStockVM data)
        {
            _context.SerialProductStock.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(SerialProductStockVM data)
        {
            SerialProductStock record = Mapper.Convert(data);
            SerialProductStock existing = _context.SerialProductStock.Find(data.batchNum);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.SerialProductStock.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
