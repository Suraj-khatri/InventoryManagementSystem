using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class OtherBillsInfoRepo : IOtherBillsInfoRepo
    {
        private Entities _context;
        public OtherBillsInfoRepo()
        {
            _context = new Entities();
        }
        public OtherBillsInfoRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.OtherBillsInfo.Find(id).deletevoucher = true;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public List<OtherBillsInfoVM> GetAll()
        {
            return Mapper.Convert(_context.OtherBillsInfo.Where(x => x.deletevoucher == false).OrderByDescending(x => x.Id).ToList());
        }
        public OtherBillsInfoVM GetById(int id)
        {
            return Mapper.Convert(_context.OtherBillsInfo.Find(id));
        }
        public void Insert(OtherBillsInfoVM data)
        {
            _context.OtherBillsInfo.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(OtherBillsInfoVM data)
        {
            OtherBillsInfo record = Mapper.Convert(data);
            OtherBillsInfo existing = _context.OtherBillsInfo.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.OtherBillsInfo.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }

        public bool BillNoExists(string billNo)
        {
            return _context.OtherBillsInfo.Any(b => b.billno == billNo);
        }
    }
}
