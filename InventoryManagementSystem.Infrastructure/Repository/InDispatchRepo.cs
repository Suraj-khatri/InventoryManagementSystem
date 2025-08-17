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
  public  class InDispatchRepo: IInDispatchRepo
    {
        private Entities _context;

        public InDispatchRepo()
        {
            _context = new Entities();
        }
        public InDispatchRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InDispatchedVM> GetAll()
        {
            return Mapper.Convert(_context.IN_DISPATCH.OrderByDescending(x => x.id).ToList());
        }

        public List<InDispatchedVM> GetAllByDispMesId(int dispmesid)
        {
            return Mapper.Convert(_context.IN_DISPATCH.Where(x => x.dispatch_msg_id == dispmesid ).ToList());
        }

        public InDispatchedVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_DISPATCH.Find(id));
        }

        public void Insert(InDispatchedVM data)
        {
            _context.IN_DISPATCH.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(InDispatchedVM data)
        {
            IN_DISPATCH record = Mapper.Convert(data);
            IN_DISPATCH existing = _context.IN_DISPATCH.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_DISPATCH.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
