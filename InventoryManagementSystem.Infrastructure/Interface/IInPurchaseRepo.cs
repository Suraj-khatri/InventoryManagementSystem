using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IInPurchaseRepo : IDisposable
    {
        List<InPurchaseVM> GetAll();
        List<InPurchaseVM> GetAllByBillId(int billid);
        List<InPurchaseVM> GetAllByBranchIdAndProdId(int branchid,int prodid);
        InPurchaseVM GetById(int id);
        InPurchaseVM GetByBillIdandPid(int bid,int pid);
        void Insert(InPurchaseVM data);
        void Update(InPurchaseVM data);
        void Delete(int id);
        void Save();
    }
}
