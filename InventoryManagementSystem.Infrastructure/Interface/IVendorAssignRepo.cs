
using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IVendorAssignRepo : IDisposable
    {
        List<VendorBidPriceVM> GetAllByProductId(int prodid);
        List<VendorBidPriceVM> GetAllByVendorId(int vid);
        VendorBidPriceVM GetById(int id);
        void Insert(VendorBidPriceVM data);
        bool IsVendorAssigned(int id, int vendorid);
        void Update(VendorBidPriceVM data);
        void Delete(int id);
        void Save();
    }
}
