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
    public class UserRoleRepo : IUserRoleRepo
    {
        private Entities _context;

        public UserRoleRepo()
        {
            _context = new Entities();
        }
        public UserRoleRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<UserRoleVM> GetAll()
        {
            return Mapper.Convert(_context.user_role.ToList());
        }

        public UserRoleVM GetById(int id)
        {
            return Mapper.Convert(_context.user_role.Find(id));
        }

        public void Insert(UserRoleVM data)
        {
            _context.user_role.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(UserRoleVM data)
        {
            user_role record = Mapper.Convert(data);
            user_role existing = _context.user_role.Find(data.row_id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.user_role.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
