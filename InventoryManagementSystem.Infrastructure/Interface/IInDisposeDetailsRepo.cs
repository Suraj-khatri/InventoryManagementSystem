using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInDisposeDetailsRepo:IDisposable
    {
        List<InDisposeDetailsVM> GetAll();
        InDisposeDetailsVM GetById(int id);
        InDisposeDetailsVM GetByMessageIdandProductId(int mesid, int pid);
        List<InDisposeDetailsVM> GetItemById(int id);
        void Insert(InDisposeDetailsVM data);
        void Update(InDisposeDetailsVM data);
        void Save();
    }
}
