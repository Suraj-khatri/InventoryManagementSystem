using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [UserAuthorize(menuId: 9)]
    public class DepartmentController : Controller
    {
        // GET: Department
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public DepartmentController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        public ActionResult Index()
        {
            var list = _dapperrepo.GetAllDepartment();
            return View(list);
        }
        public ActionResult Create()
        {
            var record = new DepartmentsVM();
            record.BranchList = _dropDownList.BranchList();
            record.BranchList.Remove(record.BranchList.First());
            record.DepartmentList = _dropDownList.DepartmentList();
            record.DepartmentShortNameList = _dropDownList.DepartmentShortNameList();
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentsVM data)
        {
            var deptname = CodeService.GetDepartmentNamewithStaticId(data.ROWID);
            data.BranchList = _dropDownList.BranchList();
            data.DepartmentList = _dropDownList.DepartmentList();
            data.DepartmentShortNameList = _dropDownList.DepartmentShortNameList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InDepartment.IsDepartmentAssigned(data))
                    {
                        data.DEPARTMENT_NAME = deptname;
                        data.CREATED_DATE = DateTime.Now;
                        data.CREATED_BY = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                        data.STATIC_ID = data.ROWID;
                        _unitofWork.InDepartment.Insert(data);
                        _unitofWork.Save();
                        TempData["Success"] = "Department Successfully Assigned";
                        TempData["Title"] = "<strong>Data Asigned</strong> <br />";
                        TempData["Icon"] = "fa fa-check";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("ROWID", "This Department is already assigned for this Branch.");
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
        public ActionResult Edit(int Id)
        {
            var data = _unitofWork.InDepartment.GetById(Id);
            data.BranchList = _dropDownList.BranchList();
            data.BranchList.Remove(data.BranchList.First());
            data.ROWID = data.STATIC_ID;
            data.DepartmentList = _dropDownList.DepartmentList();
            data.DepartmentShortNameList = _dropDownList.DepartmentShortNameList();
            return View("Create", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentsVM data)
        {
            data.BranchList = _dropDownList.BranchList();
            data.DepartmentList = _dropDownList.DepartmentList();
            data.DepartmentShortNameList = _dropDownList.DepartmentShortNameList();
            var deptname = CodeService.GetDepartmentNamewithStaticId(data.ROWID);
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InDepartment.IsDepartmentAssigned(data))
                    {
                        data.MODIFIED_DATE = DateTime.Now;
                        data.MODIFIED_BY = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                        data.DEPARTMENT_NAME = deptname;
                        data.STATIC_ID = data.ROWID;
                        _unitofWork.InDepartment.Update(data);
                        _unitofWork.Save();
                        TempData["Success"] = "<p>Department :  Succesfully Updated</p>";
                        TempData["Title"] = "<strong>Data Updated</strong> <br />";
                        TempData["Icon"] = "fa fa-check";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("ROWID", "This Department is already assigned for this Branch.");
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
                TempData["Fail"] = "Failed to Update Data. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                ModelState.AddModelError("", "Cannot process the request at the moment. Please try again later.");
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDepartment([Bind(Prefix = "StaticDataDetail")]StaticDataDetailVM data)
        {
            var dept = new DepartmentsVM();
            dept.BranchList = _dropDownList.BranchList();
            dept.DepartmentList = _dropDownList.DepartmentList();
            dept.DepartmentShortNameList = _dropDownList.DepartmentShortNameList();
            dept.StaticDataDetail = data;
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.StaticDataDetail.IsStaticDataTitleExist(data))
                    {
                        if (!_unitofWork.StaticDataDetail.IsStaticDataDescriptionExist(data))
                        {
                            data.TYPE_ID = 7;
                            data.CREATED_DATE = DateTime.Now;
                            data.CREATED_BY = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.isdepartment = true;
                            data.isdistrict = false;
                            data.iszone = false;
                            data.IsActive = true;
                            _unitofWork.StaticDataDetail.Insert(data);
                            _unitofWork.Save();
                            TempData["Success"] = "Department Successfully Added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            ModelState.AddModelError("StaticDataDetail.DETAIL_DESC", "Department Short Name already exists.");
                            dept.StaticDataDetail = data;
                            return View("Create", dept);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("StaticDataDetail.DETAIL_TITLE", "Department Name already exists.");
                        dept.StaticDataDetail = data;
                        return View("Create",dept);
                    }
                }
                else
                {
                    return RedirectToAction("Create");
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

        public JsonResult GetDeptShortName(int deptname)
        {
            var data = CodeService.GetDepartmentShortName(deptname);
            return Json(data);
        }
    }
}