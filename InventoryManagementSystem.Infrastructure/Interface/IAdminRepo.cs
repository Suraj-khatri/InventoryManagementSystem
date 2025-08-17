using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IAdminRepo:IDisposable
    {
        List<AdminVM> GetAll();
        AdminVM GetById(int id);
        void Insert(AdminVM data);
        void Update(AdminVM data);
        AdminVM GetUser(string uname);
        AdminVM GetByEmployeeSetupId(int id);
        AdminVM GetUserForIMS(string uname);
        AdminVM GetUserForResetPasword(string uname);
        void Save();
    }
}
