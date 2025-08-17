using InventoryManagementSystem.Domain;
using System;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IBillSettingRepo : IDisposable
    {
        void Update(BillSettingVM data);
        void Save();
    }
}
