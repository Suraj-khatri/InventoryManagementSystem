using Dapper;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private Entities _context;    
        public EmployeeRepo()
        {
            _context = new Entities();
         
        }
        public EmployeeRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<EmployeeVM> GetAll()
        {
           return Mapper.Convert(_context.Employee.OrderByDescending(x => x.EMPLOYEE_ID).ToList());
        }

        public EmployeeVM GetById(int id)
        {
            return Mapper.Convert(_context.Employee.Find(id));
        }

        public void Insert(EmployeeVM data)
        {
            _context.Employee.Add(Mapper.Convert(data));
        }

        public bool IsEmployeeCodeExist(EmployeeVM data)
        {
            if (_context.Employee.Where(x => x.EMP_CODE == data.EMP_CODE && x.EMPLOYEE_ID != data.EMPLOYEE_ID).Count() > 0)
                return true;
            return false;
        }

        public bool IsEmployeeEmailExist(EmployeeVM data)
        {
            if (_context.Employee.Where(x => x.OFFICIAL_EMAIL == data.OFFICIAL_EMAIL && x.EMPLOYEE_ID != data.EMPLOYEE_ID).Count() > 0)
                return true;
            return false;
        }
        public EmployeeVM GetByEmail(string email)
        {
            var data = _context.Employee.First(x => x.OFFICIAL_EMAIL
                  .Equals(email, StringComparison.OrdinalIgnoreCase));
            return Mapper.Convert(data);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(EmployeeVM data)
        {
            Employee record = Mapper.Convert(data);
            Employee existing = _context.Employee.Find(data.EMPLOYEE_ID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Employee.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
        public List<EmployeeVM> IndexFilter(EmployeeVM result)
        {
            try
            {
                var list = Mapper.Convert(_context.Employee.Where(
                                            x => (x.BRANCH_ID == result.BRANCH_ID || result.BRANCH_ID == 0)
                                            && (x.TEMP_PROVINCE == result.TEMP_PROVINCE || result.TEMP_PROVINCE == "0")).ToList());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
