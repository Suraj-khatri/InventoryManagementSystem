using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.EmployeeManagement
{
    [UserAuthorize]
    public class EmployeeDetailsController : Controller
    {
        // GET: EmployeeDetails
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public EmployeeDetailsController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 11)]
        public ActionResult Index()
        {
            var record = new EmployeeVM();
            record.BranchList = _dropDownList.BranchList();
            record.ProvinceList = _dropDownList.ProvinceList();
            record.EmployeeVMList = _dapperrepo.GetAllEmployee(0,"0");
            return View(record);
        }

        [HttpPost]
        public ActionResult Index(EmployeeVM data)
        {
            data.BranchList = _dropDownList.BranchList();
            data.ProvinceList = _dropDownList.ProvinceList();
            data.EmployeeVMList = _dapperrepo.GetAllEmployee(data.BRANCH_ID, data.TEMP_PROVINCE);
            return View(data);
        }
    }
}