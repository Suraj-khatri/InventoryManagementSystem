using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInRequisitionDetailRepo:IDisposable
    {
        List<InRequisitionDetailVM> GetAll();
        InRequisitionDetailVM GetById(int id);
        InRequisitionDetailVM GetByMessageIdandProductId(int mesid, int pid);
        void Insert(InRequisitionDetailVM data);
        void Update(InRequisitionDetailVM data);
        void Save();
    }
}
