using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IAssetProductRepo:IDisposable
    {
        List<AssetProductVM> GetAll();
        AssetProductVM GetById(int id);
        void Insert(AssetProductVM data);
        void Update(AssetProductVM data);
        AssetProductVM GetByItemId(int itemId);
        bool IsAssetNameExists(AssetProductVM data);
        bool IsAssetDescExists(AssetProductVM data);
        bool IsEditAssetNameExists(AssetProductVM data);
        bool IsEditAssetDescExists(AssetProductVM data);
        void Delete(int id);
        void Save();
    }
}
