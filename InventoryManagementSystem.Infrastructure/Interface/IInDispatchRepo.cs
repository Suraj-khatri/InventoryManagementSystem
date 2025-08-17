using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInDispatchRepo:IDisposable
    {
        List<InDispatchedVM> GetAll();
        InDispatchedVM GetById(int id);
        List<InDispatchedVM> GetAllByDispMesId(int dispmesid);
        void Insert(InDispatchedVM data);
        void Update(InDispatchedVM data);
        void Save();
    }
}
