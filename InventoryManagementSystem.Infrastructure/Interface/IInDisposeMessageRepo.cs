using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInDisposeMessageRepo:IDisposable
    {
        List<InDisposeMessageVM> GetAll(int userid, int branchid);
        List<InDisposeMessageVM> GetAllByBranch(int branchid);
        List<InDisposeMessageVM> GetAllRequested(int userid, int branchid);
        List<InDisposeMessageVM> GetAllApproved(int userid, int branchid);
        List<InDisposeMessageVM> GetAllDisposed();
        InDisposeMessageVM GetById(int id);
        void Insert(InDisposeMessageVM data);
        void Update(InDisposeMessageVM data);
        void Save();
    }
}
