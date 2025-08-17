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
    public class AssetRequisitionMessageRepo : IAssetRequisitionMessageRepo
    {
        private Entities _context;

        public AssetRequisitionMessageRepo()
        {
            _context = new Entities();
        }
        public AssetRequisitionMessageRepo(Entities context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<AssetRequisitionMessageVM> GetAll()
        {
            return Mapper.Convert(_context.ASSET_REQUISITION_MESSAGE.OrderByDescending(x => x.id).ToList());
        }
        public List<AssetRequisitionMessageVM> GetAllRequested()
        {
            return Mapper.Convert(_context.ASSET_REQUISITION_MESSAGE.Where(x=>x.status=="Requested").OrderByDescending(x => x.id).ToList());
        }

        public AssetRequisitionMessageVM GetById(int id)
        {
            return Mapper.Convert(_context.ASSET_REQUISITION_MESSAGE.Find(id));
        }

        public void Insert(AssetRequisitionMessageVM data)
        {
            _context.ASSET_REQUISITION_MESSAGE.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(AssetRequisitionMessageVM data)
        {
            ASSET_REQUISITION_MESSAGE record = Mapper.Convert(data);
            ASSET_REQUISITION_MESSAGE existing = _context.ASSET_REQUISITION_MESSAGE.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.ASSET_REQUISITION_MESSAGE.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
