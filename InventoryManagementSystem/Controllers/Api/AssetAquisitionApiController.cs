using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace InventoryManagementSystem.Controllers.Api
{
    public class AssetAquisitionApiController : ApiController
    {
        private IUnitOfWork _unitofWork;

        public AssetAquisitionApiController()
        {
            _unitofWork = new UnitOfWork();
        }
        [HttpPost]
        public HttpResponseMessage Create(AssetRequisitionMessageVM inreqmes)//place requisition
        {
            var Session = HttpContext.Current.Session;
            var branchid = Convert.ToInt32(Session["BranchId"]);
            try
            {
                if (ModelState.IsValid)
                {
                    if (inreqmes.priority == "Normal")
                    {
                        inreqmes.priority = "N";
                    }
                    else if (inreqmes.priority == "High")
                    {
                        inreqmes.priority = "H";
                    }
                    else if (inreqmes.priority == "Low")
                    {
                        inreqmes.priority = "L";
                    }
                    if (inreqmes.inreqList.Count >= 1)
                    {
                        //assetrequisitionmessage insert
                        inreqmes.status = "Requested";
                        inreqmes.branch_id = branchid;
                        inreqmes.dept_id = Convert.ToInt32(Session["DepartmentId"]);
                        _unitofWork.AssetRequisitionMessage.Insert(inreqmes);
                        _unitofWork.Save();

                        //assetrequisition insert
                        var inreq = new AssetRequisitionVM();
                        foreach (var itm in inreqmes.inreqList.Where(x => x.asset_id > 0))
                        {
                            var reqmesid = CodeService.GetAssetRequisitionMessageId();
                            inreq.asset_id = itm.asset_id;
                            inreq.qty = itm.qty;
                            inreq.price = itm.price;
                            inreq.requistion_message_id = reqmesid;
                            inreq.approved_qty = itm.qty;
                            _unitofWork.AssetRequisition.Insert(inreq);
                            _unitofWork.Save();
                        }
                        var newUrl = this.Url.Link("Default", new
                        {
                            Controller = "AssetRequisition",
                            Action = "Index"
                        });
                        return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Failed...");
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed...");
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                throw;
            }
        }
    }
}
