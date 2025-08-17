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
    public class BillInfoRepo : IBillInfoRepo
    {
        private Entities _context;

        public BillInfoRepo()
        {
            _context = new Entities();
        }
        public BillInfoRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public BillInfoVM GetById(int id)
        {
            return Mapper.Convert(_context.Bill_info.Find(id));
        }
        public List<BillInfoVM> GetAllPaidBill()
        {
            return Mapper.Convert(_context.Bill_info.Where(x => x.paid_date != null && x.party_code != null).OrderByDescending(x => x.bill_id).ToList());
        }

        public List<BillInfoVM> GetAllUnpaidBill()
        {
            return Mapper.Convert(_context.Bill_info.Where(x => x.paid_date == null && x.party_code != null).OrderByDescending(x => x.bill_id).ToList());
        }
        public List<BillInfoVM> GetAllDirectPurchaseOrder(int branchid, int userid)
        {
            try
            {
                if (userid == 1000)
                {
                    return Mapper.Convert(_context.Bill_info.Where(x => x.Status.Trim() == "Direct P.O Request").ToList());
                }
                return Mapper.Convert(_context.Bill_info.Where(x => x.Status.Trim() == "Direct P.O Request" && x.branch_id == branchid && x.forwarded_to == userid).ToList());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }


        }

        public List<BillInfoVM> GetAllPurchaseVoucherOrder(int branchid, int userid)
        {
            try
            {
                if (userid == 1000)
                {
                    return Mapper.Convert(_context.Bill_info.Where(x => x.Status.Trim() == "PurchaseVoucher Request").ToList());
                }
                return Mapper.Convert(_context.Bill_info.Where(x => x.Status.Trim() == "PurchaseVoucher Request" && x.branch_id == branchid && x.forwarded_to == userid).ToList());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }


        }

        public void Insert(BillInfoVM data)
        {
            _context.Bill_info.Add(Mapper.Convert(data));
        }

        //public int GetBranchId(BillInfoVM id)
        //{


        //}
        //public BillInfoVM ReturnInsertData(BillInfoVM data)
        //{
        //    var record = _context.Bill_info.Add(Mapper.Convert(data));
        //    Save();
        //    return Mapper.Convert(record);
        //}
        //public void Save()
        //{
        //    _context.SaveChanges();
        //}


        public void Update(BillInfoVM data)
        {
            Bill_info record = Mapper.Convert(data);
            Bill_info existing = _context.Bill_info.Find(data.bill_id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Bill_info.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }

        public bool BillNoExists(string billNo, string vendorname)
        {
            return _context.Bill_info.Any(b => b.billno == billNo && b.VendorName==vendorname && b.Status!= "PurchaseVoucher Rejected");
        }

        public void Insert(Bill_info data)
        {
            _context.Bill_info.Add(data);
        }
    }
}
