using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IPurchaseOrderMessageRepo : IDisposable
    {
        List<PurchaseOrderMessageVM> GetAll(int userid,int branchid);
        List<PurchaseOrderMessageVM> GetAllRequested(int userid, int branchid);
        List<PurchaseOrderMessageVM> GetAllDirectPurchaseRequested(int userid, int branchid);
        List<PurchaseOrderMessageVM> GetAllApproved();
        List<PurchaseOrderMessageVM> GetAllReceived();
        PurchaseOrderMessageVM GetById(int id);
        void Insert(PurchaseOrderMessageVM data);
        void Update(PurchaseOrderMessageVM data);
        void Save();
    }
}
