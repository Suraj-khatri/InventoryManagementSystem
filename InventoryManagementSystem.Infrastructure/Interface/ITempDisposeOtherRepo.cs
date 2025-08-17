using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface ITempDisposeOtherRepo:IDisposable
    {
        TempDisposeOtherVM GetById(int id);
        List<TempDisposeOtherVM> GetAllTempDisposeOtherFromTempDisposeId(int tempdisposeid);
        void Insert(TempDisposeOtherVM data);
        void Delete(int id);
        void Save();
    }
}
