using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IInRequisitionDetailOtherRepo: IDisposable
    {
        List<InRequisitionDetailOtherVM> GetAll();
        List<InRequisitionDetailOtherVM> GetAllInRequisitionDetailOtherFromDetailIdandPurId(int detailid,int pid);
        InRequisitionDetailOtherVM GetInRequisitionDetailOtherByReqMesIdAndPId(int reqmesid, int pid);
        InRequisitionDetailOtherVM GetById(int id);
        void Insert(InRequisitionDetailOtherVM data);
        void Update(InRequisitionDetailOtherVM data);
        void Delete(int id);
        void Save();
    }
}
