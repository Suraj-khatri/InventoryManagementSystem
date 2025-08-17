using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class VendorAssignRepo : IVendorAssignRepo
    {
        private Entities _context;

        public VendorAssignRepo()
        {
            _context = new Entities();
        }
        public VendorAssignRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.Vendor_Bid_Price.Find(id).is_active = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<VendorBidPriceVM> GetAllByProductId(int prodid)
        {
            return Mapper.Convert(_context.Vendor_Bid_Price.Where(x => x.product_id == prodid).ToList());
        }
        public List<VendorBidPriceVM> GetAllByVendorId(int vid)
        {
            return Mapper.Convert(_context.Vendor_Bid_Price.Where(x => x.vendor_id == vid && x.is_active == true).ToList());
        }
        public VendorBidPriceVM GetById(int id)
        {
            return Mapper.Convert(_context.Vendor_Bid_Price.Find(id));
        }

        public void Insert(VendorBidPriceVM data)
        {
            _context.Vendor_Bid_Price.Add(Mapper.Convert(data));
        }
        public bool IsVendorAssigned(int id, int vendorid)
        {
            if (_context.Vendor_Bid_Price.Where(x => x.product_id == id && x.vendor_id == vendorid).Count() > 0)
                return true;
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(VendorBidPriceVM data)
        {
            Vendor_Bid_Price record = Mapper.Convert(data);
            Vendor_Bid_Price existing = _context.Vendor_Bid_Price.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Vendor_Bid_Price.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
