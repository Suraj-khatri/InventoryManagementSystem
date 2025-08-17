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
    public class AdminRepo : IAdminRepo
    {
        private Entities _context;
        public AdminRepo()
        {
            _context = new Entities();
        }
        public AdminRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public List<AdminVM> GetAll()
        {
            return Mapper.Convert(_context.Admins.ToList());
        }
        public AdminVM GetById(int id)
        {
            return Mapper.Convert(_context.Admins.Find(id));
        }
        public AdminVM GetByEmployeeSetupId(int id)
        {
            return Mapper.Convert(_context.Admins.FirstOrDefault(x => x.Name == id));
        }
        public AdminVM GetUser(string uname)
        {
            int employeeSetupId = _context.Employee.Where(x => x.OFFICIAL_EMAIL.Equals(uname, StringComparison.OrdinalIgnoreCase)).Select(x => x.EMPLOYEE_ID).SingleOrDefault();
            if (employeeSetupId > 0)
            {
                return Mapper.Convert(_context.Admins.Where(x => x.Name == employeeSetupId).SingleOrDefault());
            }
            return Mapper.Convert(_context.Admins.
                FirstOrDefault(x => ((x.UserName != null || x.UserName.Trim() != "")
                    && x.UserName.Equals(uname, StringComparison.OrdinalIgnoreCase))
                ));
        }
        public AdminVM GetUserForIMS(string uname)
        
        
        {
            AdminVM user;
            int employeeSetupId = _context.Employee.Where(x => x.OFFICIAL_EMAIL.Equals(uname, StringComparison.OrdinalIgnoreCase)).Select(x => x.EMPLOYEE_ID).SingleOrDefault();
            if (employeeSetupId > 0)
            {
                user = Mapper.Convert(_context.Admins.Where(x => x.Name == employeeSetupId && x.Employee.EMP_STATUS == "458").SingleOrDefault());
            }
            else
            {
                string empStatusId = _context.StaticDataDetail.OrderByDescending(x=>x.ROWID).FirstOrDefault(x => x.DETAIL_TITLE == "Active").ROWID.ToString();
                user = Mapper.Convert(_context.Admins.
                     FirstOrDefault(x => ((x.UserName != null || x.UserName.Trim() != "") && x.Employee.EMP_STATUS== empStatusId //"458" for laxmi
                         && x.UserName.Equals(uname, StringComparison.OrdinalIgnoreCase))
                     ));;
            }
            if(user!=null)
            {
                var results = (from ad in _context.Admins
                               select new
                               {
                                   ad
                               }).ToList();
                if(results.Count>0)
                {
                    return user;
                }
            }
            return null;
        }
        public AdminVM GetUserForResetPasword(string uname)
        {
            return Mapper.Convert(_context.Admins.FirstOrDefault(x => x.UserName.Trim() == uname.Trim()));
        }
        public void Insert(AdminVM data)
        {
            _context.Admins.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(AdminVM data)
        {
            Admins record = Mapper.Convert(data);
            Admins existing = _context.Admins.Find(data.AdminID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Admins.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
