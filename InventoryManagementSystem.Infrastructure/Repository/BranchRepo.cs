using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class BranchRepo : IBranchRepo
    {
        private Entities _context;

        public BranchRepo()
        {
            _context = new Entities();
        }
        public BranchRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.Branches.Find(id).Is_Active = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<BranchVM> GetAll()
        {
            return Mapper.Convert(_context.Branches.ToList());
        }

        public BranchVM GetById(int id)
        {
            return Mapper.Convert(_context.Branches.Find(id));
        }

        public void Insert(BranchVM data)
        {
            _context.Branches.Add(Mapper.Convert(data));
        }

        public bool IsBranchNameExists(BranchVM data)
        {
            if (_context.Branches.Where(x => x.BRANCH_NAME == data.BRANCH_NAME && x.BRANCH_ID != data.BRANCH_ID && x.Is_Active==true).Count() > 0)
                return true;
            return false;
        }

        public bool IsBranchShortNameExists(BranchVM data)
        {
            if (_context.Branches.Where(x => x.BRANCH_SHORT_NAME == data.BRANCH_SHORT_NAME && x.BRANCH_ID != data.BRANCH_ID && x.Is_Active == true).Count() > 0)
                return true;
            return false;
        }
        public bool IsBranchCodeExists(BranchVM data)
        {
            if (_context.Branches.Where(x => x.Batch_Code == data.Batch_Code && x.BRANCH_ID != data.BRANCH_ID && x.Is_Active == true).Count() > 0)
                return true;
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(BranchVM data)
        {
            Branches record = Mapper.Convert(data);
            Branches existing = _context.Branches.Find(data.BRANCH_ID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Branches.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
