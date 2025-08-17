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
    public class EmployeeLogRepo : IEmployeeLogRepo
    {
        private Entities _context;

        public EmployeeLogRepo()
        {
            _context = new Entities();
        }
        public EmployeeLogRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<EmployeeLogVM> GetAll()
        {
            return Mapper.Convert(_context.emp_log.ToList());
        }

        public EmployeeLogVM GetById(int id)
        {
            return Mapper.Convert(_context.emp_log.Find(id));
        }

        public void Insert(EmployeeLogVM data)
        {
            _context.emp_log.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(EmployeeLogVM data)
        {
            emp_log record = Mapper.Convert(data);
            emp_log existing = _context.emp_log.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.emp_log.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
