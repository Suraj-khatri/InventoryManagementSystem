using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IAssetBranchRepo:IDisposable
    {
        List<AssetBranchVM> GetAll();
        List<AssetBranchVM> GetAllByProductId(int prodid);
        List<AssetBranchVM> GetAllProducByBranch(int branchid);
        AssetBranchVM GetById(int id);
        AssetBranchVM GetItemById(int pid, int bid);
        void Insert(AssetBranchVM data);
        void Update(AssetBranchVM data);
        bool IsBranchAssigned(int productid, int branchid);
        void Delete(int id);
        void Save();
    }
}
