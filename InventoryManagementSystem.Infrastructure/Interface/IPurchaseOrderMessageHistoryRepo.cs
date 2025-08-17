using InventoryManagementSystem.Domain;
using System;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IPurchaseOrderMessageHistoryRepo : IDisposable
    {
        void Insert(PurchaseOrderMessageHistoryVM data);
        void Update(PurchaseOrderMessageHistoryVM data);
        void Save();
    }
}
