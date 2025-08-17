using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IBranchAssignRepo : IDisposable
    {
        List<INBranchVM> GetAll();
        List<INBranchVM> IndexFilter(INBranchVM result);
        List<INBranchVM> GetAllByProductId(int prodid);
        INBranchVM GetById(int id);
        INBranchVM GetItemById(int pid, int bid);
        void Insert(INBranchVM data);
        void Update(INBranchVM data);
        bool IsBranchAssigned(int productid, int branchid);
        void Delete(int id);
        void Save();
    }
}
