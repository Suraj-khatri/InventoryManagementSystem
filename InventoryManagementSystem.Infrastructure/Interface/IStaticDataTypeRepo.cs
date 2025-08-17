using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Interface
{
   public interface IStaticDataTypeRepo:IDisposable
    {
        List<StaticDataTypeVM> GetAll();
        StaticDataTypeVM GetById(int id);
        void Insert(StaticDataTypeVM data);
        void Update(StaticDataTypeVM data);
        void Delete(int id);
        bool IsStaticDataTypeTitleExist(StaticDataTypeVM data);
        bool IsStaticDataTypeDescriptionExist(StaticDataTypeVM data);
        void Save();
    }
}
