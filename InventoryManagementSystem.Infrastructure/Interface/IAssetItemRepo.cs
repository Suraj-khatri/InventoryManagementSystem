using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public  interface IAssetItemRepo:IDisposable
    {
        List<AssetItemVM> GetAll();
        AssetItemVM GetById(int id);
        void Insert(AssetItemVM data);
        void Update(AssetItemVM data);
        void Delete(int id);
        List<AssetItemVM> GetByParentId(int id);
        bool IsGroupNameExists(AssetItemVM data);
        bool IsDescExists(AssetItemVM data);
        void Save();
    }
}
