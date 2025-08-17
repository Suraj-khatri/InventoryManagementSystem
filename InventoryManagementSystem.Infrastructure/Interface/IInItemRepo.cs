using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IInItemRepo : IDisposable
    {
        List<In_ItemVM> GetAll();
        In_ItemVM GetById(int id);
        void Insert(In_ItemVM data);
        void Update(In_ItemVM data);
        void Delete(int id);
        List<In_ItemVM> GetByParentId(int id);
        List<In_ItemVM> GetAllByParentId();
        List<In_ItemVM> GetInActiveProductByParentId(int id);
        bool IsGroupNameExists(In_ItemVM data);
        bool IsDescExists(In_ItemVM data);
        void Save();
    }
}
