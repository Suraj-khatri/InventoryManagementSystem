using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class PurchaseOrderMessageRepo : IPurchaseOrderMessageRepo
    {
        private Entities _context;

        public PurchaseOrderMessageRepo()
        {
            _context = new Entities();
        }
        public PurchaseOrderMessageRepo(Entities context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<PurchaseOrderMessageVM> GetAll(int userid, int branchid)
        {
            if (userid == 1000 || userid==1211)
            {
                return Mapper.Convert(_context.Purchase_Order_Message.OrderByDescending(x => x.id).ToList());
            }
            return Mapper.Convert(_context.Purchase_Order_Message.Where(x => x.branch_id == branchid && x.created_by == userid).OrderByDescending(x => x.id).ToList());
        }
        public List<PurchaseOrderMessageVM> GetAllRequested(int userid, int branchid)
        {
            if (userid == 1000 || userid == 1211)
            {
                return Mapper.Convert(_context.Purchase_Order_Message.Where(x => x.status.Trim() == "Requested").OrderByDescending(x => x.id).ToList());
            }
            return Mapper.Convert(_context.Purchase_Order_Message.Where(x => x.status.Trim() == "Requested" && x.branch_id == branchid && x.forwarded_to == userid).OrderByDescending(x => x.id).ToList());
        }

        public List<PurchaseOrderMessageVM> GetAllDirectPurchaseRequested(int userid, int branchid)
        {
            if (userid == 1000 || userid == 1211)
            {
                return Mapper.Convert(_context.Purchase_Order_Message.Where(x => x.status.Trim() == "DirectPurchaseRequested").OrderByDescending(x => x.id).ToList());
            }
            return Mapper.Convert(_context.Purchase_Order_Message.Where(x => x.status.Trim() == "DirectPurchaseRequested" && x.branch_id == branchid && x.forwarded_to == userid).OrderByDescending(x => x.id).ToList());
        }
        public List<PurchaseOrderMessageVM> GetAllApproved()
        {
            return Mapper.Convert(_context.Purchase_Order_Message.Where(x => x.status.Trim() == "Approved").OrderByDescending(x => x.id).ToList());
        }

        public List<PurchaseOrderMessageVM> GetAllReceived()
        {
            return Mapper.Convert(_context.Purchase_Order_Message.Where(x => x.status.Trim() == "Received").OrderByDescending(x => x.id).ToList());
        }
        public PurchaseOrderMessageVM GetById(int id)
        {
            return Mapper.Convert(_context.Purchase_Order_Message.Find(id));
        }

        public void Insert(PurchaseOrderMessageVM data)
        {
            _context.Purchase_Order_Message.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(PurchaseOrderMessageVM data)
        {
            Purchase_Order_Message record = Mapper.Convert(data);
            Purchase_Order_Message existing = _context.Purchase_Order_Message.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Purchase_Order_Message.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
