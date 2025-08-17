using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface ISerialProductStockHistoryRepo:IDisposable
    {
        SerialProductStockHistoryVM GetById(int id);
        void Insert(SerialProductStockHistoryVM data);
        void Delete(int id);
    }
}
