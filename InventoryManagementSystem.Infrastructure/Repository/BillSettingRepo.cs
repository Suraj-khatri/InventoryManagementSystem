using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Data.Entity;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class BillSettingRepo : IBillSettingRepo
    {
        private Entities _context;

        public BillSettingRepo()
        {
            _context = new Entities();
        }
        public BillSettingRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(BillSettingVM data)
        {
            BillSetting record = Mapper.Convert(data);
            BillSetting existing = _context.BillSetting.Find(data.rowid);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.BillSetting.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
