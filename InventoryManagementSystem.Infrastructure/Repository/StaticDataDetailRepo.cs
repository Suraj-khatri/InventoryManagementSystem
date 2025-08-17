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
    public class StaticDataDetailRepo : IStaticDataDetailRepo
    {
        private Entities _context;

        public StaticDataDetailRepo()
        {
            _context = new Entities();
        }
        public StaticDataDetailRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.StaticDataDetail.Find(id).IsActive = false;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<StaticDataDetailVM> GetAll()
        {
            return Mapper.Convert(_context.StaticDataDetail.Where(x=>x.TYPE_ID==25).OrderBy(x=>x.DETAIL_TITLE).ToList());
        }

        public StaticDataDetailVM GetById(int id)
        {
            return Mapper.Convert(_context.StaticDataDetail.Find(id));
        }

        public void Insert(StaticDataDetailVM data)
        {
            _context.StaticDataDetail.Add(Mapper.Convert(data));
        }
        public bool IsStaticDataDescriptionExist(StaticDataDetailVM data)
        {
            if (_context.StaticDataDetail.Where(x => x.DETAIL_DESC == data.DETAIL_DESC && x.ROWID != data.ROWID).Count() > 0)
                return true;
            return false;
        }
        public bool IsStaticDataTitleExist(StaticDataDetailVM data)
        {
            if (_context.StaticDataDetail.Where(x => x.DETAIL_TITLE == data.DETAIL_TITLE && x.ROWID != data.ROWID).Count() > 0)
                return true;
            return false;
        }
        public List<StaticDataDetailVM> GetByCategoryId(int id)
        {
            return Mapper.Convert(_context.StaticDataDetail.Where(x => x.TYPE_ID == id).ToList());
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(StaticDataDetailVM data)
        {
            StaticDataDetail record = Mapper.Convert(data);
            StaticDataDetail existing = _context.StaticDataDetail.Find(data.ROWID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.StaticDataDetail.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
