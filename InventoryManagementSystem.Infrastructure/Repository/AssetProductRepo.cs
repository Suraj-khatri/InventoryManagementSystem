using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class AssetProductRepo : IAssetProductRepo
    {
        private Entities _context;

        public AssetProductRepo()
        {
            _context = new Entities();
        }
        public AssetProductRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.ASSET_PRODUCT.Find(id).Is_Active = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<AssetProductVM> GetAll()
        {
            return Mapper.Convert(_context.ASSET_PRODUCT.ToList());
        }

        public AssetProductVM GetById(int id)
        {
            return Mapper.Convert(_context.ASSET_PRODUCT.FirstOrDefault(x => x.item_id == id));
        }

        public AssetProductVM GetByItemId(int itemId)
        {
            return Mapper.Convert(_context.ASSET_PRODUCT.FirstOrDefault(x => x.item_id == itemId));
        }

        public void Insert(AssetProductVM data)
        {
            _context.ASSET_PRODUCT.Add(Mapper.Convert(data));
        }

        public bool IsAssetDescExists(AssetProductVM data)
        {
            if (_context.ASSET_PRODUCT.Where(x => x.product_desc == data.product_desc && x.id != data.id).Count() > 0)
                return true;
            return false;
        }

        public bool IsAssetNameExists(AssetProductVM data)
        {
            if (_context.ASSET_PRODUCT.Where(x => x.porduct_code == data.porduct_code && x.id != data.id).Count() > 0)
                return true;
            return false;
        }

        public bool IsEditAssetDescExists(AssetProductVM data)
        {
            if (_context.ASSET_PRODUCT.Where(x => x.product_desc == data.product_desc && x.item_id != data.id).Count() > 0)
                return true;
            return false;
        }

        public bool IsEditAssetNameExists(AssetProductVM data)
        {
            if (_context.ASSET_PRODUCT.Where(x => x.porduct_code == data.porduct_code && x.item_id != data.id).Count() > 0)
                return true;
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(AssetProductVM data)
        {
            ASSET_PRODUCT record = Mapper.Convert(data);
            ASSET_PRODUCT existing = _context.ASSET_PRODUCT.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.ASSET_PRODUCT.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
