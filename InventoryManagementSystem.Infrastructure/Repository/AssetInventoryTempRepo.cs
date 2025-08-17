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
    public class AssetInventoryTempRepo : IAssetInventoryTempRepo
    {
        private Entities _context;

        public AssetInventoryTempRepo()
        {
            _context = new Entities();
        }
        public AssetInventoryTempRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.ASSET_INVENTORY_TEMP.Find(id).IsActive = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<AssetInventoryTempVM> GetAll()
        {
            return Mapper.Convert(_context.ASSET_INVENTORY_TEMP.ToList());
        }

        public AssetInventoryTempVM GetById(int id)
        {
            return Mapper.Convert(_context.ASSET_INVENTORY_TEMP.Find(id));
        }

        public void Insert(AssetInventoryTempVM data)
        {
            _context.ASSET_INVENTORY_TEMP.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(AssetInventoryTempVM data)
        {
            ASSET_INVENTORY_TEMP record = Mapper.Convert(data);
            ASSET_INVENTORY_TEMP existing = _context.ASSET_INVENTORY_TEMP.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.ASSET_INVENTORY_TEMP.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
