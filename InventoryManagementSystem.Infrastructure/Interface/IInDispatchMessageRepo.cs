using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInDispatchMessageRepo:IDisposable
    {
        List<InDispatchedMessageVM> GetAll();
        InDispatchedMessageVM GetById(int id);
        InDispatchedMessageVM GetByInReqMesId(int inreqmesid);
        void Insert(InDispatchedMessageVM data);
        InDispatchedMessageVM InsertData(InDispatchedMessageVM data);
        void Update(InDispatchedMessageVM data);
        void Save();
    }
}
