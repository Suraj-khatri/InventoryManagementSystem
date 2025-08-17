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
   public class InRequisitionDetailRepo: IInRequisitionDetailRepo
    {
        private Entities _context;

        public InRequisitionDetailRepo()
        {
            _context = new Entities();
        }
        public InRequisitionDetailRepo(Entities context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InRequisitionDetailVM> GetAll()
        {
            return Mapper.Convert(_context.IN_Requisition_Detail.OrderByDescending(x => x.id).ToList());
        }

        public InRequisitionDetailVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_Requisition_Detail.Find(id));
        }

        public InRequisitionDetailVM GetByMessageIdandProductId(int mesid, int pid)
        {
            return Mapper.Convert(_context.IN_Requisition_Detail.Where(x => x.Requistion_message_id == mesid && x.item == pid).FirstOrDefault());
        }

        public void Insert(InRequisitionDetailVM data)
        {
            _context.IN_Requisition_Detail.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(InRequisitionDetailVM data)
        {
            IN_Requisition_Detail record = Mapper.Convert(data);
            IN_Requisition_Detail existing = _context.IN_Requisition_Detail.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_Requisition_Detail.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
