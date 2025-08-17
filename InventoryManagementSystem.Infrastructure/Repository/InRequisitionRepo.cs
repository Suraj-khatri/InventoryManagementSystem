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
   public class InRequisitionRepo: IInRequisitionRepo
    {
        private Entities _context;

        public InRequisitionRepo()
        {
            _context = new Entities();
        }
        public InRequisitionRepo(Entities context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InRequisitionVM> GetAll()
        {
            return Mapper.Convert(_context.IN_Requisition.OrderByDescending(x => x.id).ToList());
        }

        public InRequisitionVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_Requisition.Find(id));
        }

        public InRequisitionVM GetByMessageIdandProductId(int mesid, int pid)
        {
            return Mapper.Convert(_context.IN_Requisition.Where(x => x.Requistion_message_id == mesid && x.item==pid).FirstOrDefault());
        }

        public List<InRequisitionVM> GetItemById(int id)
        {
            return Mapper.Convert(_context.IN_Requisition.Where(x => x.Requistion_message_id == id).ToList());
        }
        public void Insert(InRequisitionVM data)
        {
            _context.IN_Requisition.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(InRequisitionVM data)
        {
            IN_Requisition record = Mapper.Convert(data);
            IN_Requisition existing = _context.IN_Requisition.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_Requisition.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
