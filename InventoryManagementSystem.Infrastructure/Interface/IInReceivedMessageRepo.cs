using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInReceivedMessageRepo:IDisposable
    {
        List<InReceivedMessageVM> GetAll();
        InReceivedMessageVM GetById(int id);
        void Insert(InReceivedMessageVM data);
        void Update(InReceivedMessageVM data);
        void Save();
    }
}
