using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface ITempDisposeRepo:IDisposable
    {
        List<TempDisposeVM> GetAll();
        TempDisposeVM GetById(int id);
        void Insert(TempDisposeVM data);
        void Update(TempDisposeVM data);
        void Delete(int id);
        void Save();
    }
}
