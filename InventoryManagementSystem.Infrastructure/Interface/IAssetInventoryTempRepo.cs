using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IAssetInventoryTempRepo:IDisposable
    {
        List<AssetInventoryTempVM> GetAll();
        AssetInventoryTempVM GetById(int id);
        void Insert(AssetInventoryTempVM data);
        void Update(AssetInventoryTempVM data);
        void Delete(int id);
        void Save();
    }
}
