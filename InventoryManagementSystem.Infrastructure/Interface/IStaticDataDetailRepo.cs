using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IStaticDataDetailRepo:IDisposable
    {
        List<StaticDataDetailVM> GetAll();
        StaticDataDetailVM GetById(int id);
        void Insert(StaticDataDetailVM data);
        void Update(StaticDataDetailVM data);
        void Delete(int id);
        List<StaticDataDetailVM> GetByCategoryId(int id);
        bool IsStaticDataTitleExist(StaticDataDetailVM data);
        bool IsStaticDataDescriptionExist(StaticDataDetailVM data);
        void Save();
    }
}
