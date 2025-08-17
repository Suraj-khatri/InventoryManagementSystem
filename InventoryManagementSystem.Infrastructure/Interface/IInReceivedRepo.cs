using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInReceivedRepo:IDisposable
    {
        List<InReceivedVM> GetAll();
        InReceivedVM GetById(int id);
        void Insert(InReceivedVM data);
        void Update(InReceivedVM data);
        void Save();
    }
}
