using InventoryManagementSystem.Domain.RoleSetupVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IUserRoleRepo:IDisposable
    {
        List<UserRoleVM> GetAll();
        UserRoleVM GetById(int id);
        void Insert(UserRoleVM data);
        void Update(UserRoleVM data);
        void Save();
    }
}
