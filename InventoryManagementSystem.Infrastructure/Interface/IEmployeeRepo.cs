using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IEmployeeRepo:IDisposable
    {
        List<EmployeeVM> GetAll();
        List<EmployeeVM> IndexFilter(EmployeeVM result);
        EmployeeVM GetById(int id);
        void Insert(EmployeeVM data);
        void Update(EmployeeVM data);
        bool IsEmployeeCodeExist(EmployeeVM data);
        bool IsEmployeeEmailExist(EmployeeVM data);
        EmployeeVM GetByEmail(string email);
        void Save();
    }
}
