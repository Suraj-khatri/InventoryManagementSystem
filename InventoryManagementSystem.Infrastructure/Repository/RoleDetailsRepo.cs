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
    public  class RoleDetailsRepo: IRoleDetailsRepo
    {
        private Entities _context;

        public RoleDetailsRepo()
        {
            _context = new Entities();
        }
        public RoleDetailsRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int roleid)
        {
            _context.roles_detail.RemoveRange(_context.roles_detail.Where(x => x.role_id == roleid));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<RoleDetailsVM> GetAll()
        {
            return Mapper.Convert(_context.roles_detail.ToList());
        }

        public RoleDetailsVM GetById(int id)
        {
            return Mapper.Convert(_context.roles_detail.Find(id));
        }

        public List<RoleDetailsVM> GetByRoleId(int roleId)
        {
            return Mapper.Convert(_context.roles_detail.Where(x => x.role_id == roleId).ToList());
        }

        public void Insert(RoleDetailsVM data)
        {
            _context.roles_detail.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(RoleDetailsVM data)
        {
            roles_detail record = Mapper.Convert(data);
            roles_detail existing = _context.roles_detail.Find(data.rowid);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.roles_detail.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
