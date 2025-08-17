using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain.RoleSetupVM;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class UserFunctionRepo : IUserFunctionRepo
    {
        private Entities _context;

        public UserFunctionRepo()
        {
            _context = new Entities();
        }
        public UserFunctionRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<UserFunctionVM> GetAll()
        {
            return Mapper.Convert(_context.user_function.Where(x=>x.IsActive==true).ToList());
        }

        public List<UserFunctionVM> GetByFunId(int funid)
        {
            return Mapper.Convert(_context.user_function.Where(x => x.ParentId == funid).ToList());
        }

        public UserFunctionVM GetById(int id)
        {
            return Mapper.Convert(_context.user_function.Find(id));
        }

        public void Insert(UserFunctionVM data)
        {
            _context.user_function.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(UserFunctionVM data)
        {
            user_function record = Mapper.Convert(data);
            user_function existing = _context.user_function.Find(data.sno);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.user_function.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
