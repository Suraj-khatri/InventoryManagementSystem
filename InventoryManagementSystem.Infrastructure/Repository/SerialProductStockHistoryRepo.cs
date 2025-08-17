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
    public class SerialProductStockHistoryRepo : ISerialProductStockHistoryRepo
    {
        private Entities _context;

        public SerialProductStockHistoryRepo()
        {
            _context = new Entities();
        }
        public SerialProductStockHistoryRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.SerialProductStockHistory.Remove(Mapper.Convert(GetById(id)));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public SerialProductStockHistoryVM GetById(int id)
        {
            return Mapper.Convert(_context.SerialProductStockHistory.Find(id));
        }

        public void Insert(SerialProductStockHistoryVM data)
        {
            _context.SerialProductStockHistory.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
