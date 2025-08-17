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
    public class InDisposeDetailsRepo : IInDisposeDetailsRepo
    {
        private Entities _context;

        public InDisposeDetailsRepo()
        {
            _context = new Entities();
        }
        public InDisposeDetailsRepo(Entities context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InDisposeDetailsVM> GetAll()
        {
            return Mapper.Convert(_context.In_Dispose_Details.OrderByDescending(x => x.Id).ToList());
        }

        public InDisposeDetailsVM GetById(int id)
        {
            return Mapper.Convert(_context.In_Dispose_Details.Find(id));
        }

        public InDisposeDetailsVM GetByMessageIdandProductId(int mesid, int pid)
        {
            return Mapper.Convert(_context.In_Dispose_Details.Where(x => x.DisposeMessageId == mesid && x.ProductId == pid).FirstOrDefault());
        }

        public List<InDisposeDetailsVM> GetItemById(int id)
        {
            return Mapper.Convert(_context.In_Dispose_Details.Where(x => x.DisposeMessageId == id).ToList());

        }

        public void Insert(InDisposeDetailsVM data)
        {
            _context.In_Dispose_Details.Add(Mapper.Convert(data));

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(InDisposeDetailsVM data)
        {
            In_Dispose_Details record = Mapper.Convert(data);
            In_Dispose_Details existing = _context.In_Dispose_Details.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.In_Dispose_Details.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
