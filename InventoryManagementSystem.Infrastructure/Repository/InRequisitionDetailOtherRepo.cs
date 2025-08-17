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
   public class InRequisitionDetailOtherRepo: IInRequisitionDetailOtherRepo
    {
        private Entities _context;

        public InRequisitionDetailOtherRepo()
        {
            _context = new Entities();
        }
        public InRequisitionDetailOtherRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var data=_context.IN_Requisition_Detail_Other.Where(x=>x.detail_id==id).FirstOrDefault();
            if (data != null)
            {
            _context.IN_Requisition_Detail_Other.Remove(_context.IN_Requisition_Detail_Other.Where(x => x.detail_id == id).FirstOrDefault());
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<InRequisitionDetailOtherVM> GetAll()
        {
            return Mapper.Convert(_context.IN_Requisition_Detail_Other.OrderByDescending(x => x.id).ToList());
        }

        public List<InRequisitionDetailOtherVM> GetAllInRequisitionDetailOtherFromDetailIdandPurId(int detailid, int pid)
        {
            return Mapper.Convert(_context.IN_Requisition_Detail_Other.Where(x => x.detail_id == detailid && x.productid==pid).ToList());
        }

        public InRequisitionDetailOtherVM GetById(int id)
        {
            return Mapper.Convert(_context.IN_Requisition_Detail_Other.Find(id));
        }

        public InRequisitionDetailOtherVM GetInRequisitionDetailOtherByReqMesIdAndPId(int reqmesid, int pid)
        {
            return Mapper.Convert(_context.IN_Requisition_Detail_Other.Where(x => x.detail_id == reqmesid && x.productid == pid).FirstOrDefault());
        }

        public void Insert(InRequisitionDetailOtherVM data)
        {
            _context.IN_Requisition_Detail_Other.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(InRequisitionDetailOtherVM data)
        {
            IN_Requisition_Detail_Other record = Mapper.Convert(data);
            IN_Requisition_Detail_Other existing = _context.IN_Requisition_Detail_Other.Find(data.id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.IN_Requisition_Detail_Other.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
