using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public  interface IAssetRequisitionRepo:IDisposable
    {
        List<AssetRequisitionVM> GetAll();
        AssetRequisitionVM GetById(int id);
        List<AssetRequisitionVM> GetItemById(int id);
        void Insert(AssetRequisitionVM data);
        void Update(AssetRequisitionVM data);
        void Delete(int id);
        void Save();
    }
}
