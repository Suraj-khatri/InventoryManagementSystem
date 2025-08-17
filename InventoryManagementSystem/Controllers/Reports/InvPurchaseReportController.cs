using InventoryManagementSystem.Domain.ReportingViewModel;
using InventoryManagementSystem.Infrastructure.Interface;
using InventoryManagementSystem.Infrastructure.Repository;
using InventoryManagementSystem.Infrastructure.Services;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers.Reports
{
    public class InvPurchaseReportController : Controller
    {
        private ReportViewer _reportViewer;
        private ReportViewer _reportViewerRDL;
        private IUnitOfWork _unitOfWork;
        private List<ReportParameter> _parameters;
        public InvPurchaseReportController()
        {
            _reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true
            };
            _reportViewerRDL = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };
            _reportViewerRDL.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["SSRS_URL"]);// new Uri("http://10.0.38.199/ReportServer");
            _reportViewerRDL.ServerReport.ReportServerCredentials = new SsrsReportCredential(ConfigurationManager.AppSettings["SSRS_UserName"], ConfigurationManager.AppSettings["SSRS_Password"], ConfigurationManager.AppSettings["SSRS_Domain"]);
            _reportViewerRDL.ServerReport.Refresh();
            _reportViewerRDL.ProcessingMode = ProcessingMode.Remote;
            _reportViewerRDL.Visible = true;
            _reportViewerRDL.ShowReportBody = true;
            _unitOfWork = new UnitOfWork();
        }

        #region Initializing Parameters
        public ReportParameterVM CreateTicketParam(ReportParameterVM param = null)
        {
            if (param == null)
                param = new ReportParameterVM();
            return param;
        }
        #endregion   
        public ActionResult PurchasePendingBillsInvoice(int Id)
        {
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "PurchasePendingBillsInvoice";
            ReportParameter param = new ReportParameter("BillId", Id.ToString());

            _reportViewerRDL.ServerReport.SetParameters(param);
            ViewBag.ReportViewer = _reportViewerRDL;
            var record = new ReportParameterVM();
            record.BillId = Id;
            return View(record);
        }
        public ActionResult ReceivedApprovedOrderGeneratePO(int Id)
        {
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "ReceivedApprovedOrder_GeneratePO";
            ReportParameter param = new ReportParameter("OrderId", Id.ToString());

            _reportViewerRDL.ServerReport.SetParameters(param);
            ViewBag.ReportViewer = _reportViewerRDL;
            var record = new ReportParameterVM();
            record.OrderId = Id;
            return View(record);
        }
        public ActionResult ReceivedOrderHistoryPrintReceived(int Id)
        {
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "RecivedOrderHistory_ViewReceived";
            ReportParameter param = new ReportParameter("OrderId", Id.ToString());

            _reportViewerRDL.ServerReport.SetParameters(param);
            ViewBag.ReportViewer = _reportViewerRDL;
            var record = new ReportParameterVM();
            record.OrderId = Id;
            return View(record);
        }

        #region MIS Reporting
        public ActionResult InventoryStockInHand()
        {
            var param = CreateTicketParam();
            return View(param);
        }
        #endregion
    }
}