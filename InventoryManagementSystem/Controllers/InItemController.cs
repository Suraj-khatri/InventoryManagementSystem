using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using System;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{

    public class InItemController : Controller
    {
        // GET: InItem
        private IUnitOfWork _unitofWork;

        public InItemController()
        {
            _unitofWork = new UnitOfWork();
        }
        [UserAuthorize(menuId: 14)]
        public ActionResult Index()
        {
            var list = _unitofWork.InItem.GetAll();
            return View(list);
        }
        [UserAuthorize(menuId: 14)]
        public ActionResult Create()
        {
            var record = new In_ItemVM();
            record.Is_Active = true;
            return View(record);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(In_ItemVM data)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InItem.IsGroupNameExists(data))
                    {
                        if (!_unitofWork.InItem.IsDescExists(data))
                        {
                            data.created_date = DateTime.Now;
                            data.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.is_product = false;
                            data.parent_id = 1;
                            _unitofWork.InItem.Insert(data);
                            _unitofWork.Save();
                            TempData["Success"] = "Product Group Successfully added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("item_desc", "Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("item_name", "Group Name already exists !.");
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
        [UserAuthorize(menuId: 14)]
        public ActionResult Edit(int Id)
        {
            var data = _unitofWork.InItem.GetById(Id);
            return View("Create", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(In_ItemVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InItem.IsGroupNameExists(data))
                    {
                        if (!_unitofWork.InItem.IsDescExists(data))
                        {
                            data.modified_date = DateTime.Now;
                            data.is_product = false;
                            data.modified_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.parent_id = 1;
                            _unitofWork.InItem.Update(data);
                            _unitofWork.Save();
                            TempData["Success"] = "<p>Product Group :  Succesfully Updated</p>";
                            TempData["Title"] = "<strong>Data Updated</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("item_desc", "Description already exists !.");
                            return View("Create", data);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("item_name", "Group Name already exists !.");
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
        [UserAuthorize(menuId: 14)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _unitofWork.InItem.Delete(id);
                _unitofWork.Save();
                TempData["Success"] = "<p>Successfully removed Product Group : " + _unitofWork.InItem.GetById(id).item_name + "</p>";
                TempData["Title"] = "<strong>Product Group Removed</strong> <br />";
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

        //In Active Product List
        [UserAuthorize(menuId: 1094)]
        public ActionResult InActiveIndex()
        {
            var list = _unitofWork.InItem.GetAll();
            return View(list);
        }
    }
}