using InventoryManagementSystem.Data;
using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Services
{
   public class CompanyServices
    {
        public static CompanyVM Get()
        {
            using (var _context = new Entities())
            {
                var data = _context.COMPANY.FirstOrDefault();
                var record = new CompanyVM
                {
                    COMP_ADDRESS = data.COMP_ADDRESS,
                    COMP_FAX_NO = data.COMP_FAX_NO,
                    COMP_NAME = data.COMP_NAME,
                    COMP_PHONE_NO = data.COMP_PHONE_NO,
                };
                return record;
            }
        }
        public static BranchVM GetBranch(int id)
        {
            using (var _context = new Entities())
            {
                Branches data;
                if (id > 0)
                    data = _context.Branches.Find(id);
                else
                    data = _context.Branches.Find(1);
                return new BranchVM
                {
                    BRANCH_NAME = data.BRANCH_NAME,
                    BRANCH_ADDRESS = data.BRANCH_ADDRESS,
                    BRANCH_FAX = data.BRANCH_FAX,
                    BRANCH_PHONE = data.BRANCH_PHONE
                };
            }
        }
    }
}
