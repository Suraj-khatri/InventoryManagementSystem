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
   public class InDispatchMessageRepo: IInDispatchMessageRepo
    {
        private Entities _context;

        public InDispatchMessageRepo()
        {
            _context = new Entities();
        }
        public InDispatchMessageRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public List<InDispatchedMessageVM> GetAll()
        {
            return Mapper.Convert(_context.IN_DISPATCH_MESSAGE.OrderByDescending(x => x.id).ToList());
        }
        public InDispatchedMessageVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_DISPATCH_MESSAGE.Find(id));
        }
        public InDispatchedMessageVM GetByInReqMesId(int inreqmesid)
        {
            return Mapper.Convert(_context.IN_DISPATCH_MESSAGE.FirstOrDefault(x => x.req_id == inreqmesid));
        }

        public void Insert(InDispatchedMessageVM data)
        {
            _context.IN_DISPATCH_MESSAGE.Add(Mapper.Convert(data));
        }

        public InDispatchedMessageVM InsertData(InDispatchedMessageVM data)
        {
           var record =  _context.IN_DISPATCH_MESSAGE.Add(Mapper.Convert(data));
            _context.SaveChanges();
            return Mapper.Convert(record);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(InDispatchedMessageVM data)
        {
            IN_DISPATCH_MESSAGE record = Mapper.Convert(data);
            IN_DISPATCH_MESSAGE existing = _context.IN_DISPATCH_MESSAGE.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_DISPATCH_MESSAGE.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
