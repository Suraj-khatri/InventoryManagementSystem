using InventoryManagementSystem.Domain;
using System;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface ICompanyRepo : IDisposable
    {
        CompanyVM GetAll();
        CompanyVM GetById(int id);
        void Insert(CompanyVM data);
        void Update(CompanyVM data);
    }
}
