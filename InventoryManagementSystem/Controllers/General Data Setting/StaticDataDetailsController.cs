using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.General_Data_Setting
{
    [UserAuthorize]
    public class StaticDataDetailsController : Controller
    {
        // GET: StaticDataDetails
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public StaticDataDetailsController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }
        public ActionResult Index(int id)
        {
            ViewBag.CategoryName = CodeService.GetStaticDataTypeName(id);
            ViewBag.CategoryId = CodeService.GetStaticDataTypeId(id);
            var list = _unitofWork.StaticDataDetail.GetByCategoryId(id);
            return View(list);
        }
        public ActionResult Create(int id)
        {
            var record = new StaticDataDetailVM();
            ViewBag.CategoryName = CodeService.GetStaticDataTypeName(id);
            record.TYPE_ID = id;
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaticDataDetailVM data)
        {
            try
            {
                ViewBag.CategoryName = CodeService.GetStaticDataTypeName(data.TYPE_ID);
                var catname = ViewBag.CategoryName;
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.StaticDataDetail.IsStaticDataTitleExist(data))
                    {
                        if (!_unitofWork.StaticDataDetail.IsStaticDataDescriptionExist(data))
                        {
                            if (catname == "Department Name".Trim())
                            {
                                data.isdepartment = true;
                            }
                            else if(catname == "Districts".Trim())
                            {
                                data.isdistrict = true;
                            }
                            data.IsActive = true;
                            data.CREATED_DATE = DateTime.Now.Date;
                            data.CREATED_BY= _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            _unitofWork.StaticDataDetail.Insert(data);
                            _unitofWork.Save();

                            TempData["Success"] = "Static Data Details Successfully added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index", new { id = data.TYPE_ID });
                        }
                        else
                        {
                            data.ROWID = 0;
                            ModelState.AddModelError("DETAIL_DESC", "Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        data.ROWID = 0;
                        ModelState.AddModelError("DETAIL_TITLE", "Title already exists !.");
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
            var data = _unitofWork.StaticDataDetail.GetById(Id);
            ViewBag.CategoryName = CodeService.GetStaticDataTypeName(data.TYPE_ID);
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
                            data.IsActive = true;
                            _unitofWork.StaticDataDetail.Update(data);
                            _unitofWork.Save();
                            TempData["Success"] = "<p>Static Data Details :  Succesfully Updated</p>";
                            TempData["Title"] = "<strong>Data Updated</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index", new { id = data.TYPE_ID });
                        }
                        else
                        {
                            ModelState.AddModelError("DETAIL_DESC", "Description already exists !.");
                            ViewBag.CategoryName = CodeService.GetStaticDataTypeName(data.TYPE_ID);
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("DETAIL_TITLE", "Title already exists !.");
                        ViewBag.CategoryName = CodeService.GetStaticDataTypeName(data.TYPE_ID);
                        return View("Create", data);
                    }
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
            return View("Create", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var typeid= CodeService.GetStaticDataDetailTypeeId(id);
            try
            {
                _unitofWork.StaticDataDetail.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Static Data Details : " + _unitofWork.StaticDataDetail.GetById(id).DETAIL_TITLE + "</p>";
                TempData["Title"] = "<strong>Static Data Details Removed</strong> <br />";
                TempData["Icon"] = "fa fa-check";
                return RedirectToAction("Index", new { id = typeid });
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                TempData["Fail"] = "Failed to remove. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
            }
            return RedirectToAction("Index", new { id = typeid });
        }
    }
}