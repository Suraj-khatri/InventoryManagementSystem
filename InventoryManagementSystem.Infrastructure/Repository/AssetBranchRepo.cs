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
    public class AssetBranchRepo : IAssetBranchRepo
    {
        private Entities _context;

        public AssetBranchRepo()
        {
            _context = new Entities();
        }
        public AssetBranchRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.ASSET_BRANCH.Find(id).IS_ACTIVE = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<AssetBranchVM> GetAll()
        {
            return Mapper.Convert(_context.ASSET_BRANCH.ToList());
        }

        public List<AssetBranchVM> GetAllByProductId(int prodid)
        {
            return Mapper.Convert(_context.ASSET_BRANCH.Where(x => x.PRODUCT_ID == prodid).ToList());
        }

        public List<AssetBranchVM> GetAllProducByBranch(int branchid)
        {
            return Mapper.Convert(_context.ASSET_BRANCH.Where(x => x.BRANCH_ID == branchid).ToList());
        }

        public AssetBranchVM GetById(int id)
        {
            return Mapper.Convert(_context.ASSET_BRANCH.Find(id));
        }

        public AssetBranchVM GetItemById(int pid, int bid)
        {
            return Mapper.Convert(_context.ASSET_BRANCH.Where(x => x.PRODUCT_ID == pid && x.BRANCH_ID == bid).FirstOrDefault());
        }

        public void Insert(AssetBranchVM data)
        {
            _context.ASSET_BRANCH.Add(Mapper.Convert(data));
        }

        public bool IsBranchAssigned(int productid, int branchid)
        {
            if (_context.ASSET_BRANCH.Where(x => x.PRODUCT_ID == productid && x.BRANCH_ID == branchid).Count() > 0)
                return true;
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(AssetBranchVM data)
        {
            ASSET_BRANCH record = Mapper.Convert(data);
            ASSET_BRANCH existing = _context.ASSET_BRANCH.Find(data.ID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.ASSET_BRANCH.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
