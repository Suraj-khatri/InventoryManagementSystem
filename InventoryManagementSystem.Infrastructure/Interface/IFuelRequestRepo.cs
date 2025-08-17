using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IFuelRequestRepo:IDisposable
    {
       List<FuelRequestsVM> GetAll();
       FuelRequestsVM GetById(int id);
        List<FuelRequestsVM> GetItemById(int id);
        FuelRequestsVM ViewRequestedDetails(int id);
        FuelRequestsVM GetByMessageIdandProductId(int msgid, int pid);
        FuelRequestsVM GetByProductId(int pid);
        void Create(FuelRequestsMessageVM data);
        void Update(FuelRequestsVM data);
        void Save();
        void Create(FuelRequestsVM inreq);
    }
}
