using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class InProductRepo : IInProductRepo
    {
        private Entities _context;

        public InProductRepo()
        {
            _context = new Entities();
        }
        public InProductRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.IN_PRODUCT.Find(id).is_active = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<In_ProductVM> GetAll()
        {
            return Mapper.Convert(_context.IN_PRODUCT.Where(x=>x.is_active==true).ToList());
        }

        public In_ProductVM GetById(int id)//by itemid
        {
            return Mapper.Convert(_context.IN_PRODUCT.FirstOrDefault(x => x.item_id == id));
        }

        public In_ProductVM GetByItemId(int pId)//by productid
        {
            return Mapper.Convert(_context.IN_PRODUCT.FirstOrDefault(x => x.id == pId));
        }

        public void Insert(In_ProductVM data)
        {
            _context.IN_PRODUCT.Add(Mapper.Convert(data));
        }

        public bool IsEditProductDescExists(In_ProductVM data)
        {
            if (_context.IN_PRODUCT.Where(x => x.product_desc == data.product_desc && x.item_id != data.id).Count() > 0)
                return true;
            return false;
        }

        public bool IsEditProductNameExists(In_ProductVM data)
        {
            if (_context.IN_PRODUCT.Where(x => x.porduct_code == data.porduct_code && x.item_id != data.id).Count() > 0)
                return true;
            return false;
        }

        public bool IsProductDescExists(In_ProductVM data)
        {
            if (_context.IN_PRODUCT.Where(x => x.product_desc == data.product_desc && x.id != data.id && x.is_active == true).Count() > 0)
                return true;
            return false;
        }

        public bool IsProductNameExists(In_ProductVM data)
        {
            if (_context.IN_PRODUCT.Where(x => x.porduct_code == data.porduct_code && x.id != data.id && x.is_active==true).Count() > 0)
                return true;
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(In_ProductVM data)
        {
            IN_PRODUCT record = Mapper.Convert(data);
            IN_PRODUCT existing = _context.IN_PRODUCT.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_PRODUCT.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
