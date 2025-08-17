using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInRequisitionRepo: IDisposable
    {
        List<InRequisitionVM> GetAll();
        InRequisitionVM GetById(int id);
        InRequisitionVM GetByMessageIdandProductId(int mesid,int pid);
        List<InRequisitionVM> GetItemById(int id);
        void Insert(InRequisitionVM data);
        void Update(InRequisitionVM data);
        void Save();
    }
}
