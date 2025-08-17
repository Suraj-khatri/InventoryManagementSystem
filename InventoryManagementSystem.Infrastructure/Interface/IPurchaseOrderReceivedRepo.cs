using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IPurchaseOrderReceivedRepo : IDisposable
    {
        List<PurchaseOrderReceivedVM> GetAll();
        PurchaseOrderReceivedVM GetById(int id);
        void Insert(PurchaseOrderReceivedVM data);
        void Update(PurchaseOrderReceivedVM data);
        void Delete(int id);
        void Save();
    }
}
