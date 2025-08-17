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
    public class InReceivedRepo : IInReceivedRepo
    {
        private Entities _context;

        public InReceivedRepo()
        {
            _context = new Entities();
        }
        public InReceivedRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InReceivedVM> GetAll()
        {
            return Mapper.Convert(_context.IN_RECEIVED.OrderByDescending(x => x.id).ToList());
        }

        public InReceivedVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_RECEIVED.Find(id));
        }

        public void Insert(InReceivedVM data)
        {
            _context.IN_RECEIVED.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(InReceivedVM data)
        {
            IN_RECEIVED record = Mapper.Convert(data);
            IN_RECEIVED existing = _context.IN_RECEIVED.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_RECEIVED.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
