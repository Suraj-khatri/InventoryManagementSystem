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
    public class FuelRequestRepo : IFuelRequestRepo
    {
        private Entities _context;

       public FuelRequestRepo()
        {
            _context = new Entities();
        }

        public FuelRequestRepo(Entities context)
        {
            _context = context;
        }

     

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<FuelRequestsVM> GetAll()
        {
            return Mapper.Convert(_context.Fuel_Requests.OrderByDescending(x => x.Id).ToList());
        }
           public void Create(FuelRequestsVM data)
        {
            _context.Fuel_Requests.Add(Mapper.Convert(data));
        }

        public FuelRequestsVM GetById(int id)
        {
            return Mapper.Convert(_context.Fuel_Requests.Find(id));
        }

        public List<FuelRequestsVM> GetItemById(int id)
        {
            return Mapper.Convert(_context.Fuel_Requests.Where(x => x.Fuel_Requests_Message_Id == id).ToList());
        }
        public void Create(FuelRequestsMessageVM data)
        {
            _context.Fuel_Requests_Message.Add(Mapper.Convert(data));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(FuelRequestsVM data)
        {
            Fuel_Requests record = Mapper.Convert(data);
            Fuel_Requests existing = _context.Fuel_Requests.Find(data.Id);
            if (existing != null)
                _context.Entry(existing).State = EntityState.Detached;
            _context.Fuel_Requests.Attach(record);
            _context.Entry(record).State = EntityState.Modified;
        }

        public FuelRequestsVM ViewRequestedDetails(int id)
        {

            return Mapper.Convert(_context.Fuel_Requests
            .Where(fr => fr.Fuel_Requests_Message_Id == id)
            .FirstOrDefault());
        
        }

        public FuelRequestsVM GetByMessageIdandProductId(int mesid, int pid)
        {
            return Mapper.Convert(_context.Fuel_Requests.Where(x => x.Fuel_Requests_Message_Id == mesid && x.Id == pid).FirstOrDefault());
        }
        public FuelRequestsVM GetByProductId(int pid)
        {
            return Mapper.Convert(_context.Fuel_Requests.Where(x =>  x.Id == pid).FirstOrDefault());
        }
    }
}
