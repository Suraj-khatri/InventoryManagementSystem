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
    public class EmployeeContractRepo : IEmployeeContractRepo
    {
        private Entities _context;

        public EmployeeContractRepo()
        {
            _context = new Entities();
        }
        public EmployeeContractRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<EmployeeContractVM> GetAll()
        {
            return Mapper.Convert(_context.Employee_Contract.ToList());
        }

        public EmployeeContractVM GetById(int id)
        {
            return Mapper.Convert(_context.Employee_Contract.Find(id));
        }

        public void Insert(EmployeeContractVM data)
        {
            _context.Employee_Contract.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(EmployeeContractVM data)
        {
            Employee_Contract record = Mapper.Convert(data);
            Employee_Contract existing = _context.Employee_Contract.Find(data.RowID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Employee_Contract.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
