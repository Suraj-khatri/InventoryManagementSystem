using InventoryManagementSystem.Domain.RoleSetupVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IUserFunctionRepo:IDisposable
    {
        List<UserFunctionVM> GetAll();
        UserFunctionVM GetById(int id);
        List<UserFunctionVM> GetByFunId(int funid);
        void Insert(UserFunctionVM data);
        void Update(UserFunctionVM data);
        void Save();
    }
}
