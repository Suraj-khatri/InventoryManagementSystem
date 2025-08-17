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
   public  class DepartmentRepo: IDepartmentRepo
    {
        private Entities _context;

        public DepartmentRepo()
        {
            _context = new Entities();
        }
        public DepartmentRepo(Entities context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<DepartmentsVM> GetAll()
        {
            return Mapper.Convert(_context.departments.ToList());
        }

        public DepartmentsVM GetById(int id)
        {
            return Mapper.Convert(_context.departments.Find(id));
        }
        public bool IsDepartmentAssigned(DepartmentsVM data)
        {
            if (_context.departments.Where(x => x.BRANCH_ID == data.BRANCH_ID && x.STATIC_ID == data.ROWID).Count() > 0)
                return true;
            return false;
        }
        public void Insert(DepartmentsVM data)
        {
            _context.departments.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(DepartmentsVM data)
        {
            departments record = Mapper.Convert(data);
            departments existing = _context.departments.Find(data.DEPARTMENT_ID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.departments.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
