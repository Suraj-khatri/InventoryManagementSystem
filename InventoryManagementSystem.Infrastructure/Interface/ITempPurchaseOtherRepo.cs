using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface ITempPurchaseOtherRepo : IDisposable
    {
        TempPurchaseOtherVM GetById(int id);
        List<TempPurchaseOtherVM> GetAllTempPurchaseOtherFromTempPurId(int temppurid);
        void Insert(TempPurchaseOtherVM data);
        void Delete(int id);
        void Save();
    }
}
