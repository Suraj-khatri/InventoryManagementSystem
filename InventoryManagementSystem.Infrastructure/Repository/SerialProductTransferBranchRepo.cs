using InventoryManagementSystem.Data;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
   public class SerialProductTransferBranchRepo: ISerialProductTransferBranchRepo
    {
        private Entities _context;

        public SerialProductTransferBranchRepo()
        {
            _context = new Entities();
        }
        public SerialProductTransferBranchRepo(Entities context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var data = _context.SerialProduct_TransferBranch.Where(x => x.Id == id).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    _context.SerialProduct_TransferBranch.Remove(item);
                }
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public SerialProductTransferBranchVM GetById(int id)
        {
            return Mapper.Convert(_context.SerialProduct_TransferBranch.Find(id));
        }
        public void Insert(SerialProductTransferBranchVM data)
        {
            _context.SerialProduct_TransferBranch.Add(Mapper.Convert(data));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
