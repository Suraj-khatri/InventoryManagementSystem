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
    public class AssetNumberSequenceRepo : IAssetNumberSequenceRepo
    {
        private Entities _context;

        public AssetNumberSequenceRepo()
        {
            _context = new Entities();
        }
        public AssetNumberSequenceRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<AssetNumberSequenceVM> GetAll()
        {
            return Mapper.Convert(_context.ASSET_NumberSequence.ToList());
        }

        public AssetNumberSequenceVM GetById(int id)
        {
            return Mapper.Convert(_context.ASSET_NumberSequence.Find(id));
        }

        public void Insert(AssetNumberSequenceVM data)
        {
            _context.ASSET_NumberSequence.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(AssetNumberSequenceVM data)
        {
            ASSET_NumberSequence record = Mapper.Convert(data);
            ASSET_NumberSequence existing = _context.ASSET_NumberSequence.Find(data.ID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.ASSET_NumberSequence.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
