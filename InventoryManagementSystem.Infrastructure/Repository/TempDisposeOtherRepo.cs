using InventoryManagementSystem.Data;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class TempDisposeOtherRepo : ITempDisposeOtherRepo
    {
        private Entities _context;

        public TempDisposeOtherRepo()
        {
            _context = new Entities();
        }
        public TempDisposeOtherRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var data = _context.Temp_Dispose_Other.Where(x => x.TempDisposedId == id).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                _context.Temp_Dispose_Other.Remove(item);
                }
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<TempDisposeOtherVM> GetAllTempDisposeOtherFromTempDisposeId(int tempdisposeid)
        {
            return Mapper.Convert(_context.Temp_Dispose_Other.Where(x => x.TempDisposedId == tempdisposeid).ToList());
        }

        public TempDisposeOtherVM GetById(int id)
        {
            return Mapper.Convert(_context.Temp_Dispose_Other.Find(id));
        }

        public void Insert(TempDisposeOtherVM data)
        {
            _context.Temp_Dispose_Other.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
