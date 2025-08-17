using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Infrastructure.DapperRepo;
using InventoryManagementSystem.Infrastructure.Filter;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [UserAuthorize]
    public class InProductController : Controller
    {
        // GET: InProduct
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;

        public InProductController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        public ActionResult Index(int id)
        {
            bool isactive = CodeService.GetInItemStatus(id);
            if (isactive == true)
            {
                ViewBag.ItemName = CodeService.GetInItemName(id);
                ViewBag.ItemId = CodeService.GetInItemId(id);
                var list = _unitofWork.InItem.GetByParentId(id);
                return View(list);
            }
            else
            {
                TempData["Fail"] = "Failed To View Product. Product Group Is not Active.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index", "InItem");
            }

        }

        public ActionResult Create(int id)
        {
            var record = new In_ProductVM();
            //record.item_id = id;
            bool isactive = CodeService.GetInItemStatus(id);
            record.batch_condition = false;
            if (isactive == true)
            {
                ViewBag.ItemName = CodeService.GetInItemName(id);
                record.is_active = true;
                return View(record);
            }
            else
            {
                TempData["Fail"] = "Failed To Add Product. Product Group Is not Active.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
                return RedirectToAction("Index", "InItem");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(In_ProductVM data)
        {
            try
            {
                ViewBag.ItemName = CodeService.GetInItemName(data.id);
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InProduct.IsProductNameExists(data))
                    {
                        if (!_unitofWork.InProduct.IsProductDescExists(data))
                        {
                            //InItem added
                            In_ItemVM intem = new In_ItemVM();
                            intem.item_name = data.porduct_code;
                            intem.item_desc = data.product_desc;
                            intem.created_date = DateTime.Now;
                            intem.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            intem.is_product = true;
                            intem.Is_Active = data.is_active;
                            intem.parent_id = data.id;
                            _unitofWork.InItem.Insert(intem);
                            _unitofWork.Save();

                            //Inproduct added
                            data.id = 0;
                            data.created_date = DateTime.Now;
                            data.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.modified_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.item_id = CodeService.GetInItemId();
                            data.modified_date = DateTime.Now;
                            _unitofWork.InProduct.Insert(data);
                            _unitofWork.Save();

                            TempData["Success"] = "Product Successfully added";
                            TempData["Title"] = "<strong>Data Added</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index", new { id = intem.parent_id });
                        }
                        else
                        {
                            data.id = 0;
                            ModelState.AddModelError("product_desc", "Product Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        data.id = 0;
                        ModelState.AddModelError("porduct_code", "Product Name already exists !.");
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
            var data = _unitofWork.InProduct.GetById(Id);
            ViewBag.ItemName = CodeService.GetInItemNameByParentId(data.item_id);
            //data.item_id= CodeService.GetParentId(Id);
            return View("Create", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(In_ProductVM data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_unitofWork.InProduct.IsEditProductNameExists(data))
                    {
                        if (!_unitofWork.InProduct.IsEditProductDescExists(data))
                        {
                            //InItem updated
                            data.INITEMVM = _unitofWork.InItem.GetById(data.item_id);
                            data.INITEMVM.item_name = data.porduct_code;
                            data.INITEMVM.item_desc = data.product_desc;
                            data.INITEMVM.created_date = DateTime.Now;
                            data.INITEMVM.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.INITEMVM.is_product = true;
                            data.INITEMVM.Is_Active = data.is_active;
                            data.INITEMVM.parent_id = CodeService.GetParentId(data.id);
                            _unitofWork.InItem.Update(data.INITEMVM);
                            //_unitofWork.Save();

                            //Inproduct updated
                            data.created_date = DateTime.Now;
                            data.created_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.modified_by = _unitofWork.Admin.GetUser(Session["UserId"].ToString()).Name.ToString();
                            data.modified_date = DateTime.Now;
                            data.id = _unitofWork.InProduct.GetById(data.item_id).id;
                            _unitofWork.InProduct.Update(data);

                            _unitofWork.Save();

                            if (data.is_active == true)
                            {
                                _dapperrepo.UpdateInBranchAssignByPIdActive(data.id);
                            }
                            if (data.is_active == false)
                            {
                                _dapperrepo.UpdateInBranchAssignByPIdInActive(data.id);
                            }

                            TempData["Success"] = "<p>Product :  Succesfully Updated</p>";
                            TempData["Title"] = "<strong>Data Updated</strong> <br />";
                            TempData["Icon"] = "fa fa-check";
                            return RedirectToAction("Index", new { id = data.INITEMVM.parent_id });
                        }
                        else
                        {
                            ModelState.AddModelError("product_desc", "Product Description already exists !.");
                            return View("Create", data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("porduct_code", "Product Name already exists !.");
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

        public ActionResult TransferProduct(int Id)
        {
            var data = _unitofWork.InProduct.GetById(Id);
            ViewBag.ItemName = CodeService.GetInItemNameByParentId(data.item_id);
            data.ProductGroupList = _dropDownList.ProductGroupListForTransferProduct((int)data.INITEMVM.parent_id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TransferProduct(In_ProductVM data)
        {
            int parentid = CodeService.GetParentId(data.id);
            data.INITEMVM = _unitofWork.InItem.GetById(data.item_id);
            data.INITEMVM.parent_id = data.ProductGroupId;
            _unitofWork.InItem.Update(data.INITEMVM);
            _unitofWork.Save();
            TempData["Success"] = "<p>Product :  Succesfully Transfered</p>";
            TempData["Title"] = "<strong>Data Transfer</strong> <br />";
            TempData["Icon"] = "fa fa-check";
            return RedirectToAction("Index", new { id = parentid });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int parentid = CodeService.GetParentId(id);
            try
            {
                int itemid = CodeService.GetInProductId(id);
                _unitofWork.InProduct.Delete(itemid);
                _unitofWork.InItem.Delete(id);
                _unitofWork.Save();
                _dapperrepo.UpdateInBranchAssignByPIdInActive(itemid);
                TempData["Success"] = "<p>Successfully removed Product : " + _unitofWork.InProduct.GetByItemId(id).porduct_code + "</p>";
                TempData["Title"] = "<strong>Poduct Removed</strong> <br />";
                TempData["Icon"] = "fa fa-check";
                return RedirectToAction("Index", new { id = parentid });
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                TempData["Fail"] = "Failed to remove. Please try again later.";
                TempData["Title"] = " <strong>Error</strong> <br />";
                TempData["Icon"] = "fa fa-exclamation-circle";
            }
            return RedirectToAction("Index", new { id = parentid });
        }

        public ActionResult InActiveIndex(int id)
        {
            ViewBag.ItemName = CodeService.GetInItemName(id);
            ViewBag.ItemId = CodeService.GetInItemId(id);
            var list = _unitofWork.InItem.GetInActiveProductByParentId(id);
            return View(list);
        }
    }
}