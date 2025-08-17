using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class VendorRepo : IVendorRepo
    {
        private Entities _context;

        public VendorRepo()
        {
            _context = new Entities();
        }
        public VendorRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.Customer.Find(id).IsActive = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<CustomerVM> GetAll()
        {
            return Mapper.Convert(_context.Customer.Where(x=>x.IsActive==true).ToList());
        }

        public CustomerVM GetById(int id)
        {
            return Mapper.Convert(_context.Customer.Find(id));
        }

        public void Insert(CustomerVM data)
        {
            _context.Customer.Add(Mapper.Convert(data));
        }

        public bool IsVendorNameExists(CustomerVM data)
        {
            if (_context.Customer.Where(x => x.CustomerName == data.CustomerName && x.ID != data.ID && x.IsActive==true).Count() > 0)
                return true;
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(CustomerVM data)
        {
            Customer record = Mapper.Convert(data);
            Customer existing = _context.Customer.Find(data.ID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Customer.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
