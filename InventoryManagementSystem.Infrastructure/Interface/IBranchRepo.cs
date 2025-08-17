using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IBranchRepo : IDisposable
    {
        List<BranchVM> GetAll();
        BranchVM GetById(int id);
        void Insert(BranchVM data);
        void Update(BranchVM data);
        bool IsBranchNameExists(BranchVM data);
        bool IsBranchShortNameExists(BranchVM data);
        bool IsBranchCodeExists(BranchVM data);
        void Delete(int id);
        void Save();
    }
}
