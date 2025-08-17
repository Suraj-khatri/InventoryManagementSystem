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
    public class InPurchaseRepo : IInPurchaseRepo
    {
        private Entities _context;

        public InPurchaseRepo()
        {
            _context = new Entities();
        }
        public InPurchaseRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InPurchaseVM> GetAll()
        {
            return Mapper.Convert(_context.IN_PURCHASE.ToList());
        }

        public List<InPurchaseVM> GetAllByBillId(int billid)
        {
            return Mapper.Convert(_context.IN_PURCHASE.Where(x => x.bill_id == billid).ToList());
        }

        public List<InPurchaseVM> GetAllByBranchIdAndProdId(int branchid, int prodid)
        {
            return Mapper.Convert(_context.IN_PURCHASE.Where(x => x.prod_code == prodid && x.branch_id == branchid && x.p_stk_remain > 0).OrderBy(x=>x.entered_date).ToList());
        }

        public InPurchaseVM GetByBillIdandPid(int pid,int bid)
        {
            return Mapper.Convert(_context.IN_PURCHASE.Where(x => x.bill_id == bid && x.prod_code==pid).FirstOrDefault());
        }

        public InPurchaseVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_PURCHASE.FirstOrDefault(x => x.pur_id == id));
        }

        public void Insert(InPurchaseVM data)
        {
            _context.IN_PURCHASE.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(InPurchaseVM data)
        {
            IN_PURCHASE record = Mapper.Convert(data);
            IN_PURCHASE existing = _context.IN_PURCHASE.Find(data.pur_id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_PURCHASE.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
