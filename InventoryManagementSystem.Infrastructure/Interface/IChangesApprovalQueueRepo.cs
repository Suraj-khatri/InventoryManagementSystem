using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IChangesApprovalQueueRepo: IDisposable
    {
        List<changesApprovalQueueVM> GetAll();
        changesApprovalQueueVM GetById(int id);
        void Insert(changesApprovalQueueVM data);
        void Update(changesApprovalQueueVM data);
        void Delete(int id);
        void Save();
    }
}
