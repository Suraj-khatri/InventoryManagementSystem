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
    public class AssetItemRepo : IAssetItemRepo
    {
        private Entities _context;

        public AssetItemRepo()
        {
            _context = new Entities();
        }
        public AssetItemRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.ASSET_ITEM.Find(id).Is_Active = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<AssetItemVM> GetAll()
        {
            return Mapper.Convert(_context.ASSET_ITEM.ToList());
        }

        public AssetItemVM GetById(int id)
        {
            return Mapper.Convert(_context.ASSET_ITEM.Find(id));
        }

        public List<AssetItemVM> GetByParentId(int id)
        {
            return Mapper.Convert(_context.ASSET_ITEM.Where(x => x.parent_id == id).ToList());
        }

        public void Insert(AssetItemVM data)
        {
            _context.ASSET_ITEM.Add(Mapper.Convert(data));
        }

        public bool IsDescExists(AssetItemVM data)
        {
            if (_context.ASSET_ITEM.Where(x => x.item_desc == data.item_desc && x.id != data.id).Count() > 0)
                return true;
            return false;
        }

        public bool IsGroupNameExists(AssetItemVM data)
        {
            if (_context.ASSET_ITEM.Where(x => x.item_name == data.item_name && x.id != data.id).Count() > 0)
                return true;
            return false;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(AssetItemVM data)
        {
            ASSET_ITEM record = Mapper.Convert(data);
            ASSET_ITEM existing = _context.ASSET_ITEM.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.ASSET_ITEM.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
