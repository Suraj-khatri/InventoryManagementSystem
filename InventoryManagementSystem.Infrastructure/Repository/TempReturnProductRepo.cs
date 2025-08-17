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
    public class TempReturnProductRepo : ITempReturnProductRepo
    {
        private Entities _context;

        public TempReturnProductRepo()
        {
            _context = new Entities();
        }
        public TempReturnProductRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var data = _context.Temp_Return_Product.Where(x => x.productid == id).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    _context.Temp_Return_Product.Remove(item);
                }
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public TempReturnProductVM GetById(int id)
        {
            return Mapper.Convert(_context.Temp_Return_Product.Find(id));
        }
        public TempReturnProductVM GetByProductId(int pid)
        {
            return Mapper.Convert(_context.Temp_Return_Product.Where(x=>x.productid==pid).FirstOrDefault());
        }
        public void Insert(TempReturnProductVM data)
        {
            _context.Temp_Return_Product.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
