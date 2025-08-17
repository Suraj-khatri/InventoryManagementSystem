using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface ITempPurchaseRepo : IDisposable
    {
        List<TempPurchaseVM> GetAll();
        TempPurchaseVM GetById(int id);
        void Insert(TempPurchaseVM data);
        void Update(TempPurchaseVM data);
        void Delete(int id);
        void Save();
    }
}
