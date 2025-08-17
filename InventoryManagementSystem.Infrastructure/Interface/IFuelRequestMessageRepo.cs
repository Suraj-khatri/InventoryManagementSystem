using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IFuelRequestMessageRepo:IDisposable
    {
        List<FuelRequestsMessageVM> GetAll();
        List<FuelRequestsMessageVM> GetAll(int userid, int branchid);
      
        List<FuelRequestsMessageVM> GetAllByBranch(int branchid);
        List<FuelRequestsMessageVM> GetAllRequested(int userid, int branchid);
        List<FuelRequestsMessageVM> GetAllRecommend(int userid, int branchid);
        List<FuelRequestsMessageVM> GetAllApproved(int userid, int branchid);
        List<FuelRequestsMessageVM> GetAllRejected(int userid, int branchid);
        FuelRequestsMessageVM GetById(int id);
        void Create(FuelRequestsMessageVM data);
        void Update(FuelRequestsMessageVM data);
        void Save();
    }
}
