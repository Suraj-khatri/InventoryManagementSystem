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
    public class FuelRequestMessageRepo : IFuelRequestMessageRepo
    {
        private Entities _context;

        public FuelRequestMessageRepo()
        {
            _context = new Entities();
        }
        public FuelRequestMessageRepo(Entities context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public List<FuelRequestsMessageVM> GetAll(int userid, int branchid)
        {
            return Mapper.Convert(_context.Fuel_Requests_Message.Where(x => x.Branch_id == branchid && x.Requested_By == userid).OrderByDescending(x => x.Id).ToList());
        }

    


        public List<FuelRequestsMessageVM> GetAllByBranch(int branchid)
        {
            return Mapper.Convert(_context.Fuel_Requests_Message.Where(x => x.Branch_id == branchid).OrderBy(x => x.Id).ToList());
        }

        public List<FuelRequestsMessageVM> GetAllRequested(int userid, int branchid)
        {

            return Mapper.Convert(_context.Fuel_Requests_Message.Where(x => x.Status == "Requested" && x.Recommended_By == userid && x.Branch_id == branchid).OrderByDescending(x => x.Id).ToList());
        }

        public List<FuelRequestsMessageVM> GetAllRecommend(int userid, int branchid)
        {
           
            if (userid == 1000)
            {
                return Mapper.Convert(_context.Fuel_Requests_Message.Where(x => x.Status == "Recommended").OrderByDescending(x => x.Id).ToList());
            }
            else if (userid == 1211)
            {
                return Mapper.Convert(_context.Fuel_Requests_Message.Where(x => x.Status == "Recommended" && x.Branch_id == branchid).OrderByDescending(x => x.Id).ToList());
            }
            return Mapper.Convert(_context.Fuel_Requests_Message.Where(x => x.Status == "Recommended" && x.Branch_id == branchid && x.Approved_By == userid).OrderByDescending(x => x.Id).ToList());
        }

        public List<FuelRequestsMessageVM> GetAllApproved(int userid, int branchid)
        {
            return Mapper.Convert(_context.Fuel_Requests_Message.Where(x => x.Status == "Approved" && x.Branch_id == branchid).OrderByDescending(x => x.Id).ToList());
        }

        public List<FuelRequestsMessageVM> GetAllRejected(int userid, int branchid)
        {
            return Mapper.Convert(_context.Fuel_Requests_Message.Where(x => x.Status == "Rejected" && x.Branch_id == branchid).OrderByDescending(x => x.Id).ToList());
        }

        

     
      
        public void Create(FuelRequestsMessageVM data)
        {
            var fuelRequestMessage = Mapper.Convert(data);  
           // fuelRequestMessage.FilePath = filePath;  
            _context.Fuel_Requests_Message.Add(fuelRequestMessage);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(FuelRequestsMessageVM data)
        {
            Fuel_Requests_Message record = Mapper.Convert(data);
            Fuel_Requests_Message existing = _context.Fuel_Requests_Message.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Fuel_Requests_Message.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }

        public List<FuelRequestsMessageVM> GetAll()
        {
            return Mapper.Convert(_context.Fuel_Requests_Message.OrderByDescending(x => x.Id).ToList());
        }

        public FuelRequestsMessageVM ViewRequestedDetails(int id)
        {

            return Mapper.Convert(_context.Fuel_Requests_Message
            .Where(fr => fr.Id == id)
            .FirstOrDefault());

        }

        public FuelRequestsMessageVM GetById(int id)
        {
            return Mapper.Convert(_context.Fuel_Requests_Message.Find(id));
        }

    }
}
