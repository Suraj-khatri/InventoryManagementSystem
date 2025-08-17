using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IAssetNumberSequenceRepo : IDisposable
    {
        List<AssetNumberSequenceVM> GetAll();
        AssetNumberSequenceVM GetById(int id);
        void Insert(AssetNumberSequenceVM data);
        void Update(AssetNumberSequenceVM data);
        void Save();
    }
}
