using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IEmployeeLogRepo:IDisposable
    {
        List<EmployeeLogVM> GetAll();
        EmployeeLogVM GetById(int id);
        void Insert(EmployeeLogVM data);
        void Update(EmployeeLogVM data);
        void Save();
    }
}
