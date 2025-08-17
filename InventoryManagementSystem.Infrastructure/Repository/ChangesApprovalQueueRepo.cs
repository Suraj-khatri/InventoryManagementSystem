using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class ChangesApprovalQueueRepo : IChangesApprovalQueueRepo
    {
        private Entities _context;

        public ChangesApprovalQueueRepo()
        {
            _context = new Entities();
        }
        public ChangesApprovalQueueRepo(Entities context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            _context.changesApprovalQueue.Find(id).IsActive = false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<changesApprovalQueueVM> GetAll()
        {
            return Mapper.Convert(_context.changesApprovalQueue.ToList());
        }

        public changesApprovalQueueVM GetById(int id)
        {
            return Mapper.Convert(_context.changesApprovalQueue.Find(id));
        }

        public void Insert(changesApprovalQueueVM data)
        {
            _context.changesApprovalQueue.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(changesApprovalQueueVM data)
        {
            changesApprovalQueue record = Mapper.Convert(data);
            changesApprovalQueue existing = _context.changesApprovalQueue.Find(data.rowId);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.changesApprovalQueue.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
