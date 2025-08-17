using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Services;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class TempPurchaseOtherRepo : ITempPurchaseOtherRepo
    {
        private Entities _context;

        public TempPurchaseOtherRepo()
        {
            _context = new Entities();
        }
        public TempPurchaseOtherRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var data = _context.Temp_Purchase_Other.Where(x => x.temp_purchase_id == id).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    _context.Temp_Purchase_Other.Remove(item);
                }
            }
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<TempPurchaseOtherVM> GetAllTempPurchaseOtherFromTempPurId(int temppurid)
        {
            return Mapper.Convert(_context.Temp_Purchase_Other.Where(x=>x.temp_purchase_id==temppurid).ToList());
        }

        public TempPurchaseOtherVM GetById(int id)
        {
            return Mapper.Convert(_context.Temp_Purchase_Other.Find(id));
        }
        public void Insert(TempPurchaseOtherVM data)
        {
            _context.Temp_Purchase_Other.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
