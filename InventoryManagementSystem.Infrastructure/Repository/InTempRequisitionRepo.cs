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
    public class InTempRequisitionRepo : IInTempRequisitionRepo
    {
        private Entities _context;

        public InTempRequisitionRepo()
        {
            _context = new Entities();
        }
        public InTempRequisitionRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.IN_Temp_Requisition.Remove(_context.IN_Temp_Requisition.Where(x=>x.id==id).FirstOrDefault());
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public List<InTempRequitionVM> GetAll()
        {
            return Mapper.Convert(_context.IN_Temp_Requisition.ToList());
        }
        public InTempRequitionVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_Temp_Requisition.Find(id));
        }

        public void Insert(InTempRequitionVM data)
        {
            _context.IN_Temp_Requisition.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
