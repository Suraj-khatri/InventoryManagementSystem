using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IAssetRequisitionMessageRepo:IDisposable
    {
        List<AssetRequisitionMessageVM> GetAll();
        List<AssetRequisitionMessageVM> GetAllRequested();
        AssetRequisitionMessageVM GetById(int id);
        void Insert(AssetRequisitionMessageVM data);
        void Update(AssetRequisitionMessageVM data);
        void Save();
    }
}
