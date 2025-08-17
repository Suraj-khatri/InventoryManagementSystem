using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IPurchaseOrderRepo : IDisposable
    {
        List<PurchaseOrderVM> GetAll();
        PurchaseOrderVM GetById(int id);
        List<PurchaseOrderVM> GetItemById(int id);
        List<PurchaseOrderVM> GetItemByBillId(int id);
        void Insert(PurchaseOrderVM data);
        void Update(PurchaseOrderVM data);
        void Delete(int id);
        void Save();
    }
}
