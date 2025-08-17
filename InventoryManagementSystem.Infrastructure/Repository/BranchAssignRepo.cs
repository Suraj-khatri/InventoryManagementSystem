using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class BranchAssignRepo : IBranchAssignRepo
    {
        private Entities _context;

        public BranchAssignRepo()
        {
            _context = new Entities();
        }
        public BranchAssignRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.IN_BRANCH.Find(id).IS_ACTIVE = false;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public List<INBranchVM> GetAll()
        {
            return Mapper.Convert(_context.IN_BRANCH.ToList());
        }
        public List<INBranchVM> GetAllByProductId(int prodid)
        {
            return Mapper.Convert(_context.IN_BRANCH.Where(x => x.PRODUCT_ID == prodid /*&& x.IS_ACTIVE==true*/).ToList());
        }
        public INBranchVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_BRANCH.Find(id));
        }
        public INBranchVM GetItemById(int pid, int bid)
        {
            return Mapper.Convert(_context.IN_BRANCH.Where(x=>x.PRODUCT_ID==pid && x.BRANCH_ID== bid && x.IS_ACTIVE==true).FirstOrDefault());
        }

        public List<INBranchVM> IndexFilter(INBranchVM result)
        {
            try
            {
                var list = Mapper.Convert(_context.IN_BRANCH.Where(x => (x.BRANCH_ID == result.BRANCH_ID || result.BRANCH_ID == 0)
                                                                   &&(x.ProductGroupId == result.ProductGroupId || result.ProductGroupId == 0)).ToList());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(INBranchVM data)
        {
            _context.IN_BRANCH.Add(Mapper.Convert(data));
        }
        public bool IsBranchAssigned(int productid, int branchid)
        {
            if (_context.IN_BRANCH.Where(x => x.PRODUCT_ID == productid && x.BRANCH_ID == branchid /*&& x.IS_ACTIVE==true*/).Count() > 0)
                return true;
            return false;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(INBranchVM data)
        {
            IN_BRANCH record = Mapper.Convert(data);
            IN_BRANCH existing = _context.IN_BRANCH.Find(data.ID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_BRANCH.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
