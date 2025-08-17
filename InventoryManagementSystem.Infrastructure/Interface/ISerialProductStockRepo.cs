using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface ISerialProductStockRepo : IDisposable
    {
        SerialProductStockVM GetById(int id);
        SerialProductStockVM GetByPIdAndSequenceNo(int pid,int seqno);
        void Insert(SerialProductStockVM data);
        void Delete(int id);
        void Update(SerialProductStockVM data);
    }
}
