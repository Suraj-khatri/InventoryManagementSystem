using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IInProductRepo : IDisposable
    {
        List<In_ProductVM> GetAll();
        In_ProductVM GetById(int id);
        void Insert(In_ProductVM data);
        void Update(In_ProductVM data);
        In_ProductVM GetByItemId(int itemId);
        bool IsProductNameExists(In_ProductVM data);
        bool IsProductDescExists(In_ProductVM data);
        bool IsEditProductNameExists(In_ProductVM data);
        bool IsEditProductDescExists(In_ProductVM data);
        void Delete(int id);
        void Save();
    }
}
