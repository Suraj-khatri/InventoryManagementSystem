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
    public class SuperVisorAssignmentRepo : ISuperVisorAssignmentRepo
    {
        private Entities _context;

        public SuperVisorAssignmentRepo()
        {
            _context = new Entities();
        }
        public SuperVisorAssignmentRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.SuperVisroAssignment.Remove(Mapper.Convert(GetById(id)));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<SuperVisorAssignmentVM> GetAll()
        {
            return Mapper.Convert(_context.SuperVisroAssignment.ToList());
        }

        public List<SuperVisorAssignmentVM> GetByEmpIdandBranchId(int id,int bid)
        {
            var result = (from sa in _context.SuperVisroAssignment
                          join emp in _context.Employee on sa.SUPERVISOR equals emp.EMPLOYEE_ID
                          where sa.EMP == id && emp.BRANCH_ID==bid && sa.record_status == "y" && emp.EMP_STATUS == "458"
                          select new { sa, emp }).ToList();

            return Mapper.Convert(result.Select(x => x.sa).ToList());
            //return Mapper.Convert(_context.SuperVisroAssignment.Where(x => x.EMP == id && x.record_status=="y").ToList());
        }

        public SuperVisorAssignmentVM GetById(int id)
        {
            return Mapper.Convert(_context.SuperVisroAssignment.Find(id));
        }

        public void Insert(SuperVisorAssignmentVM data)
        {
            _context.SuperVisroAssignment.Add(Mapper.Convert(data));
        }

        public bool IsEmployeeAssigned(SuperVisorAssignmentVM data)
        {
            if (_context.SuperVisroAssignment.Where(x => x.EMP == data.EMP && x.SUPERVISOR == data.SUPERVISOR && x.record_status=="y").Count() > 0)
                return true;
            return false;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(SuperVisorAssignmentVM data)
        {
            SuperVisroAssignment record = Mapper.Convert(data);
            SuperVisroAssignment existing = _context.SuperVisroAssignment.Find(data.SV_ASSIGN_ID);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.SuperVisroAssignment.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }
    }
}
