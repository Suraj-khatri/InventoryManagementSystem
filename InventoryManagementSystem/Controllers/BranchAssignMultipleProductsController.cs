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

    public class BranchAssignMultipleProductsController : Controller
    {
        // GET: BranchAssignMultipleProducts
        private IUnitOfWork _unitofWork;
        private IDropDownList _dropDownList;
        private DapperRepoServices _dapperrepo;

        public BranchAssignMultipleProductsController()
        {
            _unitofWork = new UnitOfWork();
            _dropDownList = new DropDownList();
            _dapperrepo = new DapperRepoServices();
        }
        [UserAuthorize(menuId: 1080)]
        public ActionResult Index()
        {
            var data = new INBranchVM();
            data.BranchList = _dropDownList.BranchList();
           // data.BranchList.Remove(data.BranchList.First());
            data.ProductGroupList = _dropDownList.ProductGroupList();
            data.ProductGroupList.Remove(data.ProductGroupList.First());
           // data.BRANCH_ID =Convert.ToInt32(Session["BranchId"]);
            data.INItemVMList = _unitofWork.InItem.GetByParentId(data.ProdGrpId);
            return View(data);
        }

        [HttpPost]
        public ActionResult Index(INBranchVM data)
        {
            data.BranchList = _dropDownList.BranchList();
            //data.BranchList.Remove(data.BranchList.First());
            data.ProductGroupList = _dropDownList.ProductGroupList();
            data.ProductGroupList.Remove(data.ProductGroupList.First());
            if (data.BRANCH_ID > 0)
            {
                data.BRANCH_NAME = CodeService.GetBranchName(data.BRANCH_ID);
            }
            else
            {
                data.BRANCH_NAME = "All";
            }
            data.INItemVMList = _unitofWork.InItem.GetByParentId(data.ProdGrpId);
            return View(data);
        }

        [HttpPost]
        public ActionResult AssignProduct(INBranchVM inbranch)
        {
            inbranch.BranchList = _dropDownList.BranchList();
            var itmcount = inbranch.INItemVMList.Where(x => x.IsRowCheck == true).Count();
            if (itmcount > 0)
            {
                //if (inbranch.BRANCH_ID==0)
                //{
                    foreach (var item in inbranch.INItemVMList.Where(x => x.IsRowCheck == true && x.productid > 0))
                    {
                        try
                        {
                        _dapperrepo.AssignBranchForProduct(inbranch.BRANCH_ID,item.productid,(int)inbranch.ProductGroupId,(int)item.REORDER_LEVEL,(int) item.REORDER_QTY,(int)item.MAX_HOLDING_QTY);
                            //foreach (var itm in inbranch.BranchList)
                            //{
                            //    if (itm.Value != "0")
                            //    {
                            //        if (!_unitofWork.InBranchAssign.IsBranchAssigned(item.productid, Int32.Parse(itm.Value)))
                            //        {
                            //            inbranch.ProductGroupId = CodeService.GetParentByProductId(item.productid);
                            //            inbranch.PRODUCT_ID = item.productid;
                            //            inbranch.BRANCH_ID = Int32.Parse(itm.Value);
                            //            inbranch.IS_ACTIVE = true;
                            //            inbranch.REORDER_LEVEL = item.REORDER_LEVEL;
                            //            inbranch.REORDER_QTY = item.REORDER_LEVEL;
                            //            inbranch.MAX_HOLDING_QTY = item.REORDER_LEVEL;
                            //            _unitofWork.InBranchAssign.Insert(inbranch);
                            //            _unitofWork.Save();
                            //        }
                            //        else
                            //        {
                            //            var data = _unitofWork.InBranchAssign.GetItemById(item.productid, Int32.Parse(itm.Value));
                            //            if (data != null)
                            //            {
                            //                data.REORDER_LEVEL = item.REORDER_LEVEL;
                            //                data.REORDER_QTY = item.REORDER_QTY;
                            //                data.MAX_HOLDING_QTY = item.MAX_HOLDING_QTY;
                            //                _unitofWork.InBranchAssign.Update(data);
                            //                _unitofWork.Save();
                            //            }
                            //        }
                            //    }
                            //}
                        }
                        catch (Exception Ex)
                        {
                            return Json(new { success = false, err = Ex + "Error Occured !!.", JsonRequestBehavior.AllowGet });
                        }
                    }
                //}
                //else
                //{
                //    foreach (var item in inbranch.INItemVMList.Where(x => x.IsRowCheck == true && x.productid > 0))
                //    {
                //        try
                //        {
                //            if (!_unitofWork.InBranchAssign.IsBranchAssigned(item.productid, inbranch.BRANCH_ID))
                //            {
                //               inbranch.ProductGroupId = CodeService.GetParentByProductId(item.productid);
                //                inbranch.PRODUCT_ID = item.productid;
                //                inbranch.BRANCH_ID = inbranch.BRANCH_ID;
                //                inbranch.IS_ACTIVE = true;
                //                inbranch.REORDER_LEVEL = item.REORDER_LEVEL;
                //                inbranch.REORDER_QTY = item.REORDER_LEVEL;
                //                inbranch.MAX_HOLDING_QTY = item.REORDER_LEVEL;
                //                _unitofWork.InBranchAssign.Insert(inbranch);
                //                _unitofWork.Save();
                //            }
                //            else
                //            {
                //                var data = _unitofWork.InBranchAssign.GetItemById(item.productid, inbranch.BRANCH_ID);
                //                if (data!=null)
                //                {
                //                    data.REORDER_LEVEL = item.REORDER_LEVEL;
                //                    data.REORDER_QTY = item.REORDER_QTY;
                //                    data.MAX_HOLDING_QTY = item.MAX_HOLDING_QTY;
                //                    _unitofWork.InBranchAssign.Update(data);
                //                    _unitofWork.Save();
                //                }
                                
                //            }
                //        }
                //        catch (Exception Ex)
                //        {
                //            return Json(new { success = false, err = Ex + "Error Occured !!.", JsonRequestBehavior.AllowGet });
                //        }
                //    }
                //}
             
                return Json(new { success = true, mes = "Products Assigned Successfully", JsonRequestBehavior.AllowGet });
            }
            else
            {
                return Json(new { success = false, err = "Please Select At Least One Product To Assign.", JsonRequestBehavior.AllowGet });
            }
        }
    }
}