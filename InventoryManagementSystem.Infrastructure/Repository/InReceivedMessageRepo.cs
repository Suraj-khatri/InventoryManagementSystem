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
   public class InReceivedMessageRepo: IInReceivedMessageRepo
    {
        private Entities _context;

        public InReceivedMessageRepo()
        {
            _context = new Entities();
        }
        public InReceivedMessageRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InReceivedMessageVM> GetAll()
        {
            return Mapper.Convert(_context.IN_RECEIVED_MESSAGE.OrderByDescending(x => x.id).ToList());
        }

        public InReceivedMessageVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_RECEIVED_MESSAGE.Find(id));
        }

        public void Insert(InReceivedMessageVM data)
        {
            _context.IN_RECEIVED_MESSAGE.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(InReceivedMessageVM data)
        {
            IN_RECEIVED_MESSAGE record = Mapper.Convert(data);
            IN_RECEIVED_MESSAGE existing = _context.IN_RECEIVED_MESSAGE.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_RECEIVED_MESSAGE.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
