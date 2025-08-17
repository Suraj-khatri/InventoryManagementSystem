using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IEmployeeContractRepo:IDisposable
    {
        List<EmployeeContractVM> GetAll();
        EmployeeContractVM GetById(int id);
        void Insert(EmployeeContractVM data);
        void Update(EmployeeContractVM data);
        void Save();
    }
}
