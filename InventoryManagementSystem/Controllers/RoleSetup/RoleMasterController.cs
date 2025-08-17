using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.RoleSetup
{
    [UserAuthorize(menuId: 4)]
    public class RoleMasterController : Controller
    {
        // GET: RoleMaster
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public RoleMasterController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Index()
        {
            var list = _unitofWork.StaticDataDetail.GetAll();
            return View(list);
        }
        public ActionResult Create()
        {
            ViewBag.Category = "Roles Detail";
            var record = new StaticDataDetailVM();
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaticDataDetailVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.StaticDataDetail.IsStaticDataTitleExist(data))
                    {
                        if (!_unitofWork.StaticDataDetail.IsStaticDataDescriptionExist(data))
                        {
                            data.TYPE_ID = 25;
                            data.CREATED_DATE = DateTime.Now;
                            data.CREATED_BY = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString(); 
                            data.isdepartment = false;
                            data.isdistrict = false;
                            data.iszone = false;
                            _unitofWork.StaticDataDetail.Insert(data);
                            _unitofWork.Save();
                            TempData["Success"] = "Static Data Successfully Inserted";
                            TempData["Title"] = "<strong>Data Inserted</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("DETAIL_DESC", "Detail Description already exists !!.");
                            ViewBag.Category = "Roles Detail";
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("DETAIL_TITLE", "Detail Title already exists !!.");
                        ViewBag.Category = "Roles Detail";
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
            ViewBag.Category = "Roles Detail";
            var data = _unitofWork.StaticDataDetail.GetById(Id);
            return View("Create", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StaticDataDetailVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.StaticDataDetail.IsStaticDataTitleExist(data))
                    {
                        if (!_unitofWork.StaticDataDetail.IsStaticDataDescriptionExist(data))
                        {
                            data.MODIFIED_DATE = DateTime.Now;
                            data.MODIFIED_BY = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.TYPE_ID = 25;
                            data.isdepartment = false;
                            data.isdistrict = false;
                            data.iszone = false;
                            _unitofWork.StaticDataDetail.Update(data);
                            _unitofWork.Save();
                            TempData["Success"] = "<p>Static Data :  Succesfully Updated</p>";
                            TempData["Title"] = "<strong>Data Updated</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("DETAIL_DESC", "Detail Description already exists !!.");
                            ViewBag.Category = "Roles Detail";
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("DETAIL_TITLE", "Detail Title already exists !!.");
                        ViewBag.Category = "Roles Detail";
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
    }
}