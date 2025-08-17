using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class CompanyRepo : ICompanyRepo
    {
        private Entities _context;

        public CompanyRepo()
        {
            _context = new Entities();
        }
        public CompanyRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public CompanyVM GetAll()
        {
            return Mapper.Convert(_context.COMPANY.FirstOrDefault());
        }

        public CompanyVM GetById(int id)
        {
            return Mapper.Convert(_context.COMPANY.Find(id));
        }

        public void Insert(CompanyVM data)
        {
            _context.COMPANY.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(CompanyVM data)
        {
            COMPANY record = Mapper.Convert(data);
            COMPANY existing = _context.COMPANY.Find(data.COMP_ID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.COMPANY.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
