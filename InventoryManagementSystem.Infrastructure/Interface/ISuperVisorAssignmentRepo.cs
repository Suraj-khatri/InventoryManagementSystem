using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface ISuperVisorAssignmentRepo:IDisposable
    {
        List<SuperVisorAssignmentVM> GetAll();
        SuperVisorAssignmentVM GetById(int id);
        List<SuperVisorAssignmentVM> GetByEmpIdandBranchId(int id,int bid);
        void Insert(SuperVisorAssignmentVM data);
        void Update(SuperVisorAssignmentVM data);
        bool IsEmployeeAssigned(SuperVisorAssignmentVM data);
        void Delete(int id);
        void Save();
    }
}
