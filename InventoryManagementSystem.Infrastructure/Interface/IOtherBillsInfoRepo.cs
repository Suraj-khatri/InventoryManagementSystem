using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IOtherBillsInfoRepo : IDisposable
    {
        List<OtherBillsInfoVM> GetAll();
        OtherBillsInfoVM GetById(int id);
        void Insert(OtherBillsInfoVM data);
        void Update(OtherBillsInfoVM data);
        void Delete(int id);
        void Save();
        bool BillNoExists(string billNo);
    }
}
