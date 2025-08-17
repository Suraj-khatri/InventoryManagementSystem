using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface ISerialProductTransferBranchRepo:IDisposable
    {
        SerialProductTransferBranchVM GetById(int id);
        void Insert(SerialProductTransferBranchVM data);
        void Delete(int id);
        void Save();
    }
}
