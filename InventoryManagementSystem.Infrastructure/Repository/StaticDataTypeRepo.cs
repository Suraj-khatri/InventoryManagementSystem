using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class StaticDataTypeRepo : IStaticDataTypeRepo
    {
        private Entities _context;

        public StaticDataTypeRepo()
        {
            _context = new Entities();
        }
        public StaticDataTypeRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.StaticDataType.Find(id).IsActive = false;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<StaticDataTypeVM> GetAll()
        {
            return Mapper.Convert(_context.StaticDataType.ToList());
        }

        public StaticDataTypeVM GetById(int id)
        {
            return Mapper.Convert(_context.StaticDataType.Find(id));
        }

        public void Insert(StaticDataTypeVM data)
        {
            _context.StaticDataType.Add(Mapper.Convert(data));
        }

        public bool IsStaticDataTypeDescriptionExist(StaticDataTypeVM data)
        {
            if (_context.StaticDataType.Where(x => x.TYPE_DESC == data.TYPE_DESC && x.ROWID != data.ROWID).Count() > 0)
                return true;
            return false;
        }

        public bool IsStaticDataTypeTitleExist(StaticDataTypeVM data)
        {
            if (_context.StaticDataType.Where(x => x.TYPE_TITLE == data.TYPE_TITLE && x.ROWID != data.ROWID).Count() > 0)
                return true;
            return false;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(StaticDataTypeVM data)
        {
            StaticDataType record = Mapper.Convert(data);
            StaticDataType existing = _context.StaticDataType.Find(data.ROWID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.StaticDataType.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
