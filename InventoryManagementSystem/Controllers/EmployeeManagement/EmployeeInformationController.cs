using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Domain.RoleSetupVM;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InventoryManagementSystem.Controllers.EmployeeManagement
{
    [UserAuthorize(menuId: 11)]
    public class EmployeeInformationController : Controller
    {
        // GET: EmployeeInformation
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;

        public EmployeeInformationController()
        {
            _dapperrepo = new DapperRepoServices();
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }

        [UserAuthorize(menuId: 11)]
        public ActionResult Index()
        {
            var list = new EmployeeVM();
            var role = Session["AssignRole"].ToString();
            // var userid = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name;
            list.EmployeeVMList = _dapperrepo.GetAllEmployee(0, "0");
            list.userRole = role;
            return View(list);
        }

        [UserAuthorize(menuId: 11)]
        public ActionResult Create()
        {
            var record = new EmployeeVM();
            record.SalutationList = _dropDownList.SalutationList();
            record.GenderList = _dropDownList.GenderList();
            record.BranchList = _dropDownList.BranchList();
            record.BranchList.Remove(record.BranchList.First());
            record.DepartmentList = _dropDownList.DepartmentList();
            record.MaritalStatusList = _dropDownList.MaritalStatusList();
            record.PositionList = _dropDownList.PositionList();
            record.FunctionalTitleList = _dropDownList.FunctionalTitleList();
            record.SalaryTitleList = _dropDownList.SalaryTitleList();
            record.EmployeeStatusList = _dropDownList.EmployeeStatusList();
            record.BloodGroupList = _dropDownList.BloodGroupList();
            record.EmployeeTypeList = _dropDownList.EmployeeTypeList();
            record.CountryList = _dropDownList.CountryList();
            record.ProvinceList = _dropDownList.ProvinceList();
            record.ProvinceList.Remove(record.ProvinceList.First());
            record.DistrictList = _dropDownList.DistrictList();
            record.RelationshipList = _dropDownList.RelationshipList();
            record.BIRTH_DATE = DateTime.Now.Date;
            record.JOINED_DATE = DateTime.Now.Date;
            record.APPOINTMENT_DATE = DateTime.Now.Date;
            record.PERMANENT_DATE = DateTime.Now.Date;
            record.C_START_DATE = DateTime.Now.Date;
            record.C_END_DATE = DateTime.Now.Date;
            record.GRATUITY_EFFECTIVE_DATE = DateTime.Now.Date;
            record.LASTPROMOTED = DateTime.Now.Date;
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeVM data)
        {
            data.SalutationList = _dropDownList.SalutationList();
            data.GenderList = _dropDownList.GenderList();
            data.BranchList = _dropDownList.BranchList();
            data.BranchList.Remove(data.BranchList.First());
            data.DepartmentList = _dropDownList.DepartmentList();
            data.MaritalStatusList = _dropDownList.MaritalStatusList();
            data.PositionList = _dropDownList.PositionList();
            data.FunctionalTitleList = _dropDownList.FunctionalTitleList();
            data.SalaryTitleList = _dropDownList.SalaryTitleList();
            data.EmployeeStatusList = _dropDownList.EmployeeStatusList();
            data.BloodGroupList = _dropDownList.BloodGroupList();
            data.EmployeeTypeList = _dropDownList.EmployeeTypeList();
            data.CountryList = _dropDownList.CountryList();
            data.ProvinceList = _dropDownList.ProvinceList();
            data.DistrictList = _dropDownList.DistrictList();
            data.RelationshipList = _dropDownList.RelationshipList();
            try
            {
                if (ModelState.IsValid)
                {
                    string newPwd = Membership.GeneratePassword(6, 1);

                    if (!_unitofWork.Employee.IsEmployeeCodeExist(data))
                    {
                        if (!_unitofWork.Employee.IsEmployeeEmailExist(data))
                        {
                            data.CREATED_DATE = DateTime.Now;
                            data.CREATED_BY = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            _unitofWork.Employee.Insert(data);
                            _unitofWork.Save();

                            //admin insert
                            var admin = new AdminVM();
                            admin.UserName = data.EMP_CODE;
                            admin.UserPassword = "A9993E364706816ABA3E25717850C26C9CD0D89D";// this password is abc// HashService.Hash(newPwd);
                            admin.Email = data.OFFICIAL_EMAIL;
                            admin.Cell_phone = data.PERSONAL_MOBILE;
                            admin.Name = CodeService.GetEmployeeId();
                            admin.status = true;
                            admin.IsTemporary = false;
                            admin.user_type = "A";
                            admin.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            admin.created_date = DateTime.Now;
                            _unitofWork.Admin.Insert(admin);
                            _unitofWork.Save();

                            //userrole insert
                            var userrole = new UserRoleVM();
                            userrole.user_id = CodeService.GetAdminId();
                            userrole.role_id = CodeService.GetStaticDataDetailRowId();
                            _unitofWork.UserRole.Insert(userrole);
                            _unitofWork.Save();

                            //await EmailService.SendMailAsync(data.PERSONAL_EMAIL, "Account Created",
                            // EmailService.EmailMessage.AccountCreated(data.FIRST_NAME + " " + data.MIDDLE_NAME + " " + data.LAST_NAME, data.FIRST_NAME, newPwd));
                            //TempData["Success"] = "<p>Email sent to " + data.PERSONAL_EMAIL + "</p>";
                            //TempData["Title"] = "<strong>Email Sent</strong> <br />";
                            //TempData["Icon"] = "fa fa-mail-forward";
                            TempData["Success"] = "<p>New Employee Registered Successfully.</p>";
                            TempData["Title"] = "<strong>Employee Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("OFFICIAL_EMAIL", "This email already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("EMP_CODE", "Employee Code already exists !.");
                        return View("Create", data);
                    }
                }
                else
                {
                    return View(data);
                }
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                TempData["Fail"] = "Failed to Add Data. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
                return View(data);
            }
        }
        [UserAuthorize(menuId: 11)]
        public ActionResult Edit(int Id)
        {
            var data = _unitofWork.Employee.GetById(Id);
            data.SalutationList = _dropDownList.SalutationList();
            data.GenderList = _dropDownList.GenderList();
            data.BranchList = _dropDownList.BranchList();
            data.BranchList.Remove(data.BranchList.First());
            data.DepartmentList = _dropDownList.DepartmentList();
            data.MaritalStatusList = _dropDownList.MaritalStatusList();
            data.PositionList = _dropDownList.PositionList();
            data.FunctionalTitleList = _dropDownList.FunctionalTitleList();
            data.SalaryTitleList = _dropDownList.SalaryTitleList();
            data.EmployeeStatusList = _dropDownList.EmployeeStatusList();
            data.BloodGroupList = _dropDownList.BloodGroupList();
            data.EmployeeTypeList = _dropDownList.EmployeeTypeList();
            data.CountryList = _dropDownList.CountryList();
            data.ProvinceList = _dropDownList.ProvinceList();
            data.DistrictList = _dropDownList.DistrictList();
            data.RelationshipList = _dropDownList.RelationshipList();
            return View("Create", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeVM data)
        {
            data.SalutationList = _dropDownList.SalutationList();
            data.GenderList = _dropDownList.GenderList();
            data.BranchList = _dropDownList.BranchList();
            data.BranchList.Remove(data.BranchList.First());
            data.DepartmentList = _dropDownList.DepartmentList();
            data.MaritalStatusList = _dropDownList.MaritalStatusList();
            data.PositionList = _dropDownList.PositionList();
            data.FunctionalTitleList = _dropDownList.FunctionalTitleList();
            data.SalaryTitleList = _dropDownList.SalaryTitleList();
            data.EmployeeStatusList = _dropDownList.EmployeeStatusList();
            data.BloodGroupList = _dropDownList.BloodGroupList();
            data.EmployeeTypeList = _dropDownList.EmployeeTypeList();
            data.CountryList = _dropDownList.CountryList();
            data.ProvinceList = _dropDownList.ProvinceList();
            data.DistrictList = _dropDownList.DistrictList();
            data.RelationshipList = _dropDownList.RelationshipList();

            var modifiedby = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();

            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.Employee.IsEmployeeCodeExist(data))
                    {
                        data.MODIFIED_DATE = DateTime.Now;
                        data.MODIFIED_BY = modifiedby;
                        _unitofWork.Employee.Update(data);
                        _unitofWork.Save();

                        var admin = _unitofWork.Admin.GetByEmployeeSetupId(data.EMPLOYEE_ID);
                        admin.modified_by = modifiedby;
                        admin.modified_date = DateTime.Now;
                        _unitofWork.Admin.Update(admin);
                        _unitofWork.Save();

                        TempData["Success"] = "<p>Employee :  Succesfully Updated</p>";
                        TempData["Title"] = "<strong>Data Updated</strong> <br />";
                        TempData["Icon"] = "fa fa-check";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("EMP_CODE", "Employee Code already exists !.");
                        return View("Create", data);
                    }
                }
                else
                {
                    return View("Create", data);
                }
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                TempData["Fail"] = "Failed to Update Data. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
                return View("Create", data);
            }
            //return View(data);
        }
        public JsonResult GetPerDistrictforZoneName(string pzonename)
        {
            var data = _dropDownList.DistricWithProvinceList(pzonename).ToList();
            return Json(data);
        }
        public JsonResult GetTempDistrictforZoneName(string tzonename)
        {
            var data = _dropDownList.DistricWithProvinceList(tzonename).ToList();
            return Json(data);
        }
        public JsonResult GetDepartmentForBranchList(int branchid)
        {
            var data = _dropDownList.DepartmentFromBranchList(branchid).ToList();
            return Json(data);
        }
    }
}