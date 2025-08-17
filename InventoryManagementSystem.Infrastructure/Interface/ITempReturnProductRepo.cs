using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface ITempReturnProductRepo:IDisposable
    {
        TempReturnProductVM GetById(int id);
        TempReturnProductVM GetByProductId(int pid);
        void Insert(TempReturnProductVM data);
        void Delete(int id);
        void Save();
    }
}
