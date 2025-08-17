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
    public class AssetRequisitionRepo : IAssetRequisitionRepo
    {
        private Entities _context;

        public AssetRequisitionRepo()
        {
            _context = new Entities();
        }
        public AssetRequisitionRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.ASSET_REQUISITION.Remove(Mapper.Convert(GetById(id)));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<AssetRequisitionVM> GetAll()
        {
            return Mapper.Convert(_context.ASSET_REQUISITION.ToList());
        }

        public AssetRequisitionVM GetById(int id)
        {
            return Mapper.Convert(_context.ASSET_REQUISITION.Find(id));
        }

        public List<AssetRequisitionVM> GetItemById(int id)
        {
            return Mapper.Convert(_context.ASSET_REQUISITION.Where(x => x.requistion_message_id == id).ToList());
        }

        public void Insert(AssetRequisitionVM data)
        {
            _context.ASSET_REQUISITION.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(AssetRequisitionVM data)
        {
            ASSET_REQUISITION record = Mapper.Convert(data);
            ASSET_REQUISITION existing = _context.ASSET_REQUISITION.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.ASSET_REQUISITION.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
