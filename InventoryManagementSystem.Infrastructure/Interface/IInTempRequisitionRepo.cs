using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInTempRequisitionRepo:IDisposable
    {
        List<InTempRequitionVM> GetAll();
        InTempRequitionVM GetById(int id);
        void Insert(InTempRequitionVM data);
        void Delete(int id);
    }
}
