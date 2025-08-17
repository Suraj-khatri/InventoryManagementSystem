using InventoryManagementSystem.Domain.RoleSetupVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IRoleDetailsRepo:IDisposable
    {
        List<RoleDetailsVM> GetAll();
        RoleDetailsVM GetById(int id);
        List<RoleDetailsVM> GetByRoleId(int roleId);
        void Insert(RoleDetailsVM data);
        void Update(RoleDetailsVM data);
        void Delete(int roleid);
        void Save();
    }
}
