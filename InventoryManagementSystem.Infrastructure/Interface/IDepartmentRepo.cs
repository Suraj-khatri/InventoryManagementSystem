using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IDepartmentRepo: IDisposable
    {
        List<DepartmentsVM> GetAll();
        DepartmentsVM GetById(int id);
        void Insert(DepartmentsVM data);
        void Update(DepartmentsVM data);
        bool IsDepartmentAssigned(DepartmentsVM data);
        void Save();
    }
}
