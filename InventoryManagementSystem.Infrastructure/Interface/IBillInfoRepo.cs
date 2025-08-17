using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Infrastructure.Interface
{
    public interface IBillInfoRepo : IDisposable
    {
        BillInfoVM GetById(int id);
        List<BillInfoVM> GetAllUnpaidBill();
        List<BillInfoVM> GetAllPaidBill();
        List<BillInfoVM> GetAllDirectPurchaseOrder(int userid, int branchid);
        List<BillInfoVM> GetAllPurchaseVoucherOrder(int userid, int branchid);
        void Insert(BillInfoVM data);
       // BillInfoVM ReturnInsertData(BillInfoVM data);
        void Update(BillInfoVM data);
        //void Save();
        bool BillNoExists(string billNo, string vendorname);
        void Insert(Bill_info billInfo);
    }
}
