using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IVendorRepo : IDisposable
    {
        List<CustomerVM> GetAll();
        CustomerVM GetById(int id);
        void Insert(CustomerVM data);
        void Update(CustomerVM data);
        bool IsVendorNameExists(CustomerVM data);
        void Delete(int id);
        void Save();
    }
}
