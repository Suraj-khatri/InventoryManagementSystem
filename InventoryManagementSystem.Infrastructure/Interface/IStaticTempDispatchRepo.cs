using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IStaticTempDispatchRepo:IDisposable
    {
        List<StaticTempDispatchVM> GetAllNonPrintingItem();
        List<StaticTempDispatchVM> GetAllPrintingItem();
        StaticTempDispatchVM GetById(int id);
        void Insert(StaticTempDispatchVM data);
        void Update(StaticTempDispatchVM data);
        void Delete(int id);
        void Save();
    }
}
