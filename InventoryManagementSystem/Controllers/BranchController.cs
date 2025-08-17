using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{

    public class BranchController : Controller
    {
        // GET: Branch
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;
        public BranchController()
        {
            _dapperrepo = new DapperRepoServices();
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        [UserAuthorize(menuId: 8)]
        public ActionResult Index()
        {
            var list = _dapperrepo.GetAllBranch();
            return View(list);
        }
        [UserAuthorize(menuId: 8)]
        public ActionResult Create()
        {
            var record = new BranchVM();
            record.Is_Active = true;
            record.CountryList = _dropDownList.CountryList();
            record.ProvinceList = _dropDownList.ProvinceList();
            record.ProvinceList.Remove(record.ProvinceList.First());
            record.MunicipalityList = _dropDownList.MunicipalityList();
            record.MunicipalityList.Remove(record.MunicipalityList.First());
            record.DistrictList = _dropDownList.DistrictList();
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BranchVM data)
        {
            data.CountryList = _dropDownList.CountryList();
            data.ProvinceList = _dropDownList.ProvinceList();
            data.MunicipalityList = _dropDownList.MunicipalityList();
            data.MunicipalityList.Remove(data.MunicipalityList.First());
            data.DistrictList = _dropDownList.DistrictList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InBranch.IsBranchNameExists(data))
                    {
                        //if (!_unitofWork.InBranch.IsBranchShortNameExists(data))
                        //{
                        //if (!_unitofWork.InBranch.IsBranchCodeExists(data))
                        //{
                        data.CREATED_DATE = DateTime.Now;
                        data.CREATED_BY = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                        data.COMPANY_ID = 1;
                        _unitofWork.InBranch.Insert(data);
                        _unitofWork.Save();
                        TempData["Success"] = "Branch Successfully added";
                        TempData["Title"] = "<strong>Data Added</strong> <br />";
                        TempData["Icon"] = "fa fa-check";
                        return RedirectToAction("Index");
                        //    }
                        //    else
                        //    {
                        //        ModelState.AddModelError("Batch_Code", "Branch Code already exists !.");
                        //        return View("Create", data);
                        //    }
                        //}
                        //else
                        //{
                        //    ModelState.AddModelError("BRANCH_SHORT_NAME", "Branch Short Name already exists !.");
                        //    return View("Create", data);
                        //}
                    }
                    else
                    {
                        ModelState.AddModelError("BRANCH_NAME", "Branch Name already exists !.");
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

        [UserAuthorize(menuId: 8)]
        public ActionResult Edit(int Id)
        {
            var data = _unitofWork.InBranch.GetById(Id);
            data.CountryList = _dropDownList.CountryList();
            data.ProvinceList = _dropDownList.ProvinceList();
            data.MunicipalityList = _dropDownList.MunicipalityList();
            data.MunicipalityList.Remove(data.MunicipalityList.First());
            data.DistrictList = _dropDownList.DistrictList();
            return View("Create", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BranchVM data)
        {
            data.CountryList = _dropDownList.CountryList();
            data.ProvinceList = _dropDownList.ProvinceList();
            data.MunicipalityList = _dropDownList.MunicipalityList();
            data.MunicipalityList.Remove(data.MunicipalityList.First());
            data.DistrictList = _dropDownList.DistrictList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InBranch.IsBranchNameExists(data))
                    {
                        //if (!_unitofWork.InBranch.IsBranchShortNameExists(data))
                        //{
                        //    if (!_unitofWork.InBranch.IsBranchCodeExists(data))
                        //    {
                                data.MODIFIED_DATE = DateTime.Now;
                                data.MODIFIED_BY = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                                data.COMPANY_ID = 1;
                                _unitofWork.InBranch.Update(data);
                                _unitofWork.Save();
                                TempData["Success"] = "<p>Branch :  Succesfully Updated</p>";
                                TempData["Title"] = "<strong>Data Updated</strong> <br />";
                                TempData["Icon"] = "fa fa-check";
                                return RedirectToAction("Index");
                        //    }
                        //    else
                        //    {
                        //        ModelState.AddModelError("Batch_Code", "Branch Code already exists !.");
                        //        return View("Create", data);
                        //    }
                        //}
                        //else
                        //{
                        //    ModelState.AddModelError("BRANCH_SHORT_NAME", "Branch Short Name already exists !.");
                        //    return View("Create", data);
                        //}
                    }
                    else
                    {
                        ModelState.AddModelError("BRANCH_NAME", "Branch Name already exists !.");
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
        public ActionResult Delete(int id)
        {
            try
            {
                _unitofWork.InBranch.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Branch : " + _unitofWork.InBranch.GetById(id).BRANCH_NAME + "</p>";
                TempData["Title"] = "<strong>Branch Removed</strong> <br />";
                TempData["Icon"] = "fa fa-check";
                return RedirectToAction("Index");
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                TempData["Fail"] = "Failed to remove. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
            }
            return RedirectToAction("Index");
        }
        public JsonResult GetDistrictforProvinceName(string provincename)
        {
            var data = _dropDownList.DistricWithProvinceList(provincename).ToList();
            return Json(data);
        }
    }
}