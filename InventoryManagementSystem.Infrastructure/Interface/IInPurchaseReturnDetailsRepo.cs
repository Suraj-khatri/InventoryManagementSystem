using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInPurchaseReturnDetailsRepo:IDisposable
    {
        void Insert(InPurchaseReturnDetailsVM data);
        void Save();
    }
}
