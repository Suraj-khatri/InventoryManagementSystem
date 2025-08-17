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
    public class InRequisitionMessageRepo : IInRequisitionMessageRepo
    {
        private Entities _context;

        public InRequisitionMessageRepo()
        {
            _context = new Entities();
        }
        public InRequisitionMessageRepo(Entities context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InRequisitionMessageVM> GetAll(int userid, int branchid)
        {
            if (userid == 1000 || userid == 1211)
            {
                return Mapper.Convert(_context.IN_Requisition_Message.OrderByDescending(x => x.id).ToList());
            }
            return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.branch_id == branchid && x.Requ_by == userid).OrderByDescending(x => x.id).ToList());
        }
        public List<InRequisitionMessageVM> GetAllRequested(int userid, int branchid)
        {
            if (userid == 1000 || userid == 1211)
            {
                return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.status == "Requested").OrderByDescending(x => x.id).ToList());
            }
            return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.status == "Requested" && x.branch_id == branchid).OrderByDescending(x => x.id).ToList());
        }
        public List<InRequisitionMessageVM> GetAllRecommend(int userid, int branchid)
        {
            if (userid == 1000)
            {
                return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.status == "Recommended").OrderByDescending(x => x.id).ToList());
            }
            else if(userid==1211)
            {
                return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.status == "Recommended" && x.branch_id == branchid).OrderByDescending(x => x.id).ToList());
            }
            return Mapper.Convert(_context.IN_Requisition_Message.Where(x=>x.status== "Recommended" && x.branch_id == branchid && x.recommed_by==userid).OrderByDescending(x => x.id).ToList());
        }
        public List<InRequisitionMessageVM> GetAllApproved(int userid, int branchid)
        {
            if (userid == 1000 || userid== 1211)
            {
                return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.status == "Approved").Take(100).OrderByDescending(x => x.id).ToList());
            }
            return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.status == "Approved" && x.branch_id == branchid && x.Approver_id == userid).OrderByDescending(x => x.id).ToList());
        }
        public List<InRequisitionMessageVM> GetAllDispatched(int userid, int branchid)
        {
            if (userid == 1000 || userid == 1211)
            {
                return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.status == "Full Dispatched").OrderByDescending(x => x.id).ToList());
            }
            return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.status == "Full Dispatched" && x.branch_id == branchid && x.Requ_by == userid).OrderByDescending(x => x.id).ToList());
        }
        public InRequisitionMessageVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_Requisition_Message.Find(id));
        }
        public void Insert(InRequisitionMessageVM data)
        {
            _context.IN_Requisition_Message.Add(Mapper.Convert(data));
        }

        public void Save()  
        {
            _context.SaveChanges();
        }

        public void Update(InRequisitionMessageVM data)
        {
            IN_Requisition_Message record = Mapper.Convert(data);
            IN_Requisition_Message existing = _context.IN_Requisition_Message.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_Requisition_Message.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }

        public List<InRequisitionMessageVM> GetAllByBranch(int branchid)
        {
            return Mapper.Convert(_context.IN_Requisition_Message.Where(x => x.branch_id == branchid).OrderBy(x=>x.id).ToList());
        }
    }
}
