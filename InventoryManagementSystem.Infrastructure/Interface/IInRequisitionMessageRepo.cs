using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInRequisitionMessageRepo:IDisposable
    {
        List<InRequisitionMessageVM> GetAll(int userid, int branchid);
        List<InRequisitionMessageVM> GetAllByBranch(int branchid);
        List<InRequisitionMessageVM> GetAllRequested(int userid, int branchid);
        List<InRequisitionMessageVM> GetAllRecommend(int userid, int branchid);
        List<InRequisitionMessageVM> GetAllApproved(int userid, int branchid);
        List<InRequisitionMessageVM> GetAllDispatched(int userid, int branchid);
        InRequisitionMessageVM GetById(int id);
        void Insert(InRequisitionMessageVM data);
        void Update(InRequisitionMessageVM data);
        void Save();
    }
}
