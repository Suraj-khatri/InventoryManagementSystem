using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.General_Data_Setting
{
  
    public class GeneralDataSettingController : Controller
    {
        // GET: GeneralDataSetting
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;

        public GeneralDataSettingController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
        }

        [UserAuthorize(menuId: 47)]
        public ActionResult Index()
        {
            var list = _unitofWork.StaticDataType.GetAll();
            return View(list);
        }

        [UserAuthorize(menuId: 47)]
        public ActionResult Create()
        {
            var record = new StaticDataTypeVM();
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaticDataTypeVM data)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_unitofWork.StaticDataType.IsStaticDataTypeTitleExist(data))
                    {
                        if (!_unitofWork.StaticDataType.IsStaticDataTypeDescriptionExist(data))
                        {
                            data.IsActive = true;
                            _unitofWork.StaticDataType.Insert(data);
                            _unitofWork.Save();
                            TempData["Success"] = "Category Group Successfully added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("TYPE_DESC", "Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("TYPE_TITLE", "Category Name already exists !.");
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
            var data = _unitofWork.StaticDataType.GetById(Id);
            return View("Create", data);
        }


        [UserAuthorize(menuId: 47)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StaticDataTypeVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.StaticDataType.IsStaticDataTypeTitleExist(data))
                    {
                        if (!_unitofWork.StaticDataType.IsStaticDataTypeDescriptionExist(data))
                        {
                            data.IsActive = true;
                            _unitofWork.StaticDataType.Update(data);
                            _unitofWork.Save();
                            TempData["Success"] = "<p>Category Group :  Succesfully Updated</p>";
                            TempData["Title"] = "<strong>Data Updated</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("TYPE_DESC", "Description already exists !.");
                            return View("Create", data);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("TYPE_TITLE", "Category Name already exists !.");
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

        [UserAuthorize(menuId: 47)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _unitofWork.StaticDataType.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Category Group : " + _unitofWork.StaticDataType.GetById(id).TYPE_TITLE + "</p>";
                TempData["Title"] = "<strong>Category Group Removed</strong> <br />";
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
    }
}