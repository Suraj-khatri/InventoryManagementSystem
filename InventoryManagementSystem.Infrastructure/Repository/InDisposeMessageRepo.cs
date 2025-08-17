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
    public class InDisposeMessageRepo : IInDisposeMessageRepo
    {
        private Entities _context;

        public InDisposeMessageRepo()
        {
            _context = new Entities();
        }
        public InDisposeMessageRepo(Entities context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InDisposeMessageVM> GetAll(int userid, int branchid)
        {
            if (userid == 1000 || userid==1211)
            {
                return Mapper.Convert(_context.In_Dispose_Message.OrderByDescending(x => x.Id).ToList());
            }
            return Mapper.Convert(_context.In_Dispose_Message.Where(x => x.DisposingBranchId == branchid && x.RequestBy == userid).OrderByDescending(x => x.Id).ToList());
        }

        public List<InDisposeMessageVM> GetAllApproved(int userid, int branchid)
        {
            if (userid == 1000 || userid==1211)
            {
                return Mapper.Convert(_context.In_Dispose_Message.Where(x => x.Status == "Approved").OrderByDescending(x => x.Id).ToList());
            }
            return Mapper.Convert(_context.In_Dispose_Message.Where(x => x.Status == "Approved" && x.DisposingBranchId == branchid && x.RequestBy == userid).OrderByDescending(x => x.Id).ToList());
        }

        public List<InDisposeMessageVM> GetAllByBranch(int branchid)
        {
            return Mapper.Convert(_context.In_Dispose_Message.Where(x => x.DisposingBranchId == branchid).OrderBy(x => x.Id).ToList());
        }
        public List<InDisposeMessageVM> GetAllDisposed()
        {
            return Mapper.Convert(_context.In_Dispose_Message.Where(x => x.Status == "Disposed").OrderByDescending(x => x.Id).ToList());
        }
        public List<InDisposeMessageVM> GetAllRequested(int userid, int branchid)
        {
            if (userid == 1000 || userid==1211)
            {
                return Mapper.Convert(_context.In_Dispose_Message.Where(x => x.Status == "Requested").OrderByDescending(x => x.Id).ToList());
            }
            return Mapper.Convert(_context.In_Dispose_Message.Where(x => x.Status == "Requested" && x.DisposingBranchId == branchid && x.ForwardedForApproval == userid).OrderByDescending(x => x.Id).ToList());
        }
        public InDisposeMessageVM GetById(int id)
        {
            return Mapper.Convert(_context.In_Dispose_Message.Find(id));
        }
        public void Insert(InDisposeMessageVM data)
        {
            _context.In_Dispose_Message.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Update(InDisposeMessageVM data)
        {
            In_Dispose_Message record = Mapper.Convert(data);
            In_Dispose_Message existing = _context.In_Dispose_Message.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.In_Dispose_Message.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
