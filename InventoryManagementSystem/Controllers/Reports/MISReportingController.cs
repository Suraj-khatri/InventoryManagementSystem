using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Domain.ReportingViewModel;
using InventoryManagementSystem.Infrastructure.Filter;
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
    [UserAuthorize]
    public class MISReportingController : Controller
    {
        // GET: MISReporting
        private ReportViewer _reportViewer;
        private ReportViewer _reportViewerRDL;
        private static CompanyVM _company;
        private IDropDownList _dropDownList;
        private IUnitOfWork _unitOfWork;
        private List<ReportParameter> _parameters;
        public MISReportingController()
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
            _dropDownList = new DropDownList();
            if (_company == null)
                _company = CompanyServices.Get();
            _parameters = new List<ReportParameter> {
            new ReportParameter("CompanyName", _company.COMP_NAME),
            new ReportParameter("Pan","PAN : " + _company.COMP_MAP_CODE),
            };
        }
        #region Initializing Parameters
        public ReportParameterVM CreateTicketParam(ReportParameterVM param = null)
        {
            if (param == null)
                param = new ReportParameterVM();
            var adminid = Convert.ToInt32(Session["AuthId"]);
            var empid = CodeService.GetEmployeeIdFromAdmin(adminid);
            var bidfromemp = CodeService.GetBranchIdFromEmployee(empid);
            var assignedrole = Session["AssignRole"].ToString();
            if (assignedrole.Trim() == "Admin_User" || assignedrole.Trim() == "Administrator" || assignedrole.Trim() == "StoreKeeper(HeadOffice)" || assignedrole.Trim() == "Fixed Asset Management" || assignedrole.Trim() == "Fuel Manager")
            {
                param.BranchList = _dropDownList.BranchList();
                param.EmployeeList = _dropDownList.EmployeeListFuel(0);
                param.EmployeeList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            }
            else if (assignedrole.Trim() == "Auditor")
            {
                param.BranchList = _dropDownList.BranchListForAuditor();
                param.EmployeeList = _dropDownList.EmployeeListFuel(0);
                param.EmployeeList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            }
            else
            {
                param.BranchList = _dropDownList.BranchListForDirectDispatchForBranch(bidfromemp);
                param.EmployeeList = _dropDownList.EmployeeListFuel(empid);
            }
            param.DepartmentList = _dropDownList.DepartmentList();
            param.DepartmentList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            param.ProductList = _dropDownList.ProductList();
            param.ProductList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            param.SerialProductList = _dropDownList.SerialProductList();
            param.VendorList = _dropDownList.VendorList();
            param.VendorList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            param.ProductGroupList = _dropDownList.ProductGroupList();
            param.FiscalYearList = _dropDownList.FiscalYearList();
            param.UserAccessList = _dropDownList.UserAccessList();
            param.UserNameList = _dropDownList.UserNameList();
            param.PaymentStatusList = _dropDownList.PaymentStatusList();
            param.AquisitionTypeList = _dropDownList.AquisitionTypeList();
            param.AssetGroupNameList = _dropDownList.AssetGroupList();
            param.VehicleCategoryList = _dropDownList.VehicleCategoryList();
            param.VehicleCategoryList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            param.FuelCategoryList = _dropDownList.FuelCategoryList();
            param.FuelCategoryList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            param.FuelStatusList = _dropDownList.FuelStatusList();
            param.FuelStatusList.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            param.FuelVendorList = _dropDownList.FuelVendorList();
            var excludedRoles = new List<string> { "Admin_User", "Administrator", "StoreKeeper(HeadOffice)", "Fuel Manager"};
            if (bidfromemp == 12 && !excludedRoles.Contains(assignedrole))
            {
                param.BranchList = new List<SelectListItem>();
                param.ProductList = new List<SelectListItem>();
                param.ProductGroupList = new List<SelectListItem>();
                param.BranchList.Insert(0, new SelectListItem { Text = "SELECT" });
                param.ProductList.Insert(0, new SelectListItem { Text = "SELECT" });
                param.ProductGroupList.Insert(0, new SelectListItem { Text = "SELECT" });
            }
                return param;
        }
        #endregion
        #region MIS Reporting
        #region InventoryStockInHandDateWise
        [UserAuthorize(menuId:1087)]
        public ActionResult InventoryStockInHandDateWise()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryStockInHandDateWise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_StockInHandDateWise";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("GroupId", param.ProdGroupId.ToString()),
                new ReportParameter("GroupName", param.ProductGroupList.FirstOrDefault(x => x.Value == param.ProdGroupId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion
        #region StockLedgerDatewise
        [UserAuthorize(menuId:1103)]
        public ActionResult StockLedgerDatewise()
        {
            var rec = CreateTicketParam();
            rec.BranchList = rec.BranchList.Where(item => item.Text != "All").ToList();
            return View(rec);
        }
        [HttpPost]
        public ActionResult StockLedgerDatewise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            param.BranchList = param.BranchList.Where(item => item.Text != "All").ToList();
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_DateWiseStockLedgerRpt";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("GroupId", param.ProdGroupId.ToString()),
                new ReportParameter("GroupName", param.ProductGroupList.FirstOrDefault(x => x.Value == param.ProdGroupId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion
        #region InventoryStockInHand
        [UserAuthorize(menuId:56)]
        public ActionResult InventoryStockInHand()
        {
            return View(CreateTicketParam());
        }

        [HttpPost]
        public ActionResult InventoryStockInHand(ReportParameterVM param)
        {
            try
            {
                var reportby = @Session["FullName"].ToString();
                param = CreateTicketParam(param);
                _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_StockInHand";
                GetBranchDetails(param.BranchId);
                List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("GroupId", param.ProdGroupId.ToString()),
                new ReportParameter("GroupName", param.ProductGroupList.FirstOrDefault(x => x.Value == param.ProdGroupId.ToString()).Text),
               
            };
                _parameters.AddRange(parameters);
                _reportViewerRDL.ServerReport.SetParameters(_parameters);
                ViewBag.ReportViewer = _reportViewerRDL;
                return View(param);

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, "An error occurred while processing the report: " + (ex.InnerException == null ? ex.Message : ex.InnerException.Message));

                // Return the view with an error message
                return View(param);
            }

        }
        #endregion
        #region InventoryStockInHandSerialProduct
        [UserAuthorize(menuId:1084)]
        public ActionResult InventoryStockInHandSerialProduct()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryStockInHandSerialProduct(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_StockInHandSerialProduct";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("ProductName", param.SerialProductList.FirstOrDefault(x => x.Value == param.ProductId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion
        #region InventorySummaryReportDateWise
        public ActionResult InventorySummaryReportDateWise()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventorySummaryReportDateWise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_SummaryReportDateWise";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("branchid", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("productid", param.ProductId.ToString()),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryRateWiseStockInHand
        public ActionResult InventoryRateWiseStockInHand()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryRateWiseStockInHand(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_RateWiseStockReport";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryVendorWise
        [UserAuthorize(menuId:58)]
        public ActionResult InventoryVendorWise()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryVendorWise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_VendorWise";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("VendorId", param.VendorId.ToString()),
                new ReportParameter("VendorName", param.VendorList.FirstOrDefault(x => x.Value == param.VendorId.ToString()).Text),
                new ReportParameter("GroupId", param.ProdGroupId.ToString()),
                new ReportParameter("GroupName", param.ProductGroupList.FirstOrDefault(x => x.Value == param.ProdGroupId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryGroupwiseStockInHand
        public ActionResult InventoryGroupwiseStockInHand()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryGroupwiseStockInHand(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_GroupWiseStockInHand";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("GroupId", param.ProdGroupId.ToString()),
                new ReportParameter("GroupName", param.ProductGroupList.FirstOrDefault(x => x.Value == param.ProdGroupId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventorySummaryGroupWise
        public ActionResult InventorySummaryGroupWise()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventorySummaryGroupWise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_SummaryReportGroupWise";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("branchid", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("productid", param.ProductId.ToString()),
                new ReportParameter("GroupId", param.ProdGroupId.ToString()),
                new ReportParameter("GroupName", param.ProductGroupList.FirstOrDefault(x => x.Value == param.ProdGroupId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryExpensesGroupWise
        public ActionResult InventoryExpensesGroupWise()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryExpensesGroupWise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ExpensesReportGroupWise";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("branchid", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("productid", param.ProductId.ToString()),
                new ReportParameter("GroupId", param.ProdGroupId.ToString()),
                new ReportParameter("GroupName", param.ProductGroupList.FirstOrDefault(x => x.Value == param.ProdGroupId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryExpensesProductGroupAndBranchWise
        public ActionResult InventoryExpensesProductWise()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryExpensesProductWise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ExpensesReportProductWise";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }

        [HttpPost]
        public ActionResult InventoryExpensesBranchWise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ExpensesReportBranchWise";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View("InventoryExpensesProductWise", param);
        }

        [HttpPost]
        public ActionResult InventoryExpensesBranchAndGroupWise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ExpensesReportBranchAndGroupWise";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View("InventoryExpensesProductWise", param);
        }
        #endregion

        #region InventoryExpensesSummaryAndDetail
        public ActionResult InventoryExpenses()
        {
            return View(CreateTicketParam());
        }

        [HttpPost]
        public ActionResult InventoryExpensesSummary(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ExpensesSummaryReport";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("DepartmentId", param.DepartmentId.ToString()),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View("InventoryExpenses", param);
        }

        [HttpPost]
        public ActionResult InventoryExpensesDetail(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ExpensesDetailReport";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("DepartmentId", param.DepartmentId.ToString()),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("FromBranch", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View("InventoryExpenses", param);
        }
        #endregion

        #region InventoryMonthEndReport
        public ActionResult InventoryMonthEndReport()
        {
            var data = CreateTicketParam();
            data.BranchList.Remove(data.BranchList.First());
            return View(data);
        }

        [HttpPost]
        public ActionResult InventoryMonthEndReport(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_MonthEndReport";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region BudgetReport
        public ActionResult BudgetReport()
        {
            var data = CreateTicketParam();
            data.BranchList.Remove(data.BranchList.First());
            return View(data);
        }

        [HttpPost]
        public ActionResult BudgetReport(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_BudgetedReport";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("FY", param.FiscalYearList.FirstOrDefault(x => x.Value == param.FiscalId.ToString()).Text),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region CheckReOrderLevel
        [UserAuthorize(menuId:66)]
        public ActionResult CheckReOrderLevel()
        {
            return View(CreateTicketParam());
        }

        [HttpPost]
        public ActionResult CheckReOrderLevel(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_CheckReOrderLevel";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("GroupId", param.ProdGroupId.ToString()),
                new ReportParameter("GroupName", param.ProductGroupList.FirstOrDefault(x => x.Value == param.ProdGroupId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryRequestAndDirectPurchase
        [UserAuthorize(menuId:67)]
        public ActionResult InventoryRequestAndDirectPurchase()
        {
            return View(CreateTicketParam());
        }

        [HttpPost]
        public ActionResult InventoryRequestPurchase(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_RequestPurchaseOnly";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("VendorId", param.VendorId.ToString()),
                new ReportParameter("VendorName", param.VendorList.FirstOrDefault(x => x.Value == param.VendorId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View("InventoryRequestAndDirectPurchase", param);
        }

        [HttpPost]
        public ActionResult InventoryDirectPurchase(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_DirectPurchase";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("BillNo", param.BillNo??""),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("VendorId", param.VendorId.ToString()),
                new ReportParameter("VendorName", param.VendorList.FirstOrDefault(x => x.Value == param.VendorId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View("InventoryRequestAndDirectPurchase", param);
        }
        #endregion

        #region UserAccessReport
        public ActionResult UserAccessReport()
        {
            return View(CreateTicketParam());
        }

        [HttpPost]
        public ActionResult UserAccessReport(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_UserAccessReport";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("userName", param.UserName.ToString()),
                new ReportParameter("userType", param.UserAccess.ToString()),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryPurchasePaymentWise
        [UserAuthorize(menuId:69)]
        public ActionResult InventoryPurchasePaymentWise()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryPurchasePaymentWise(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_PurchasePaymentWiseRpt";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("VendorId", param.VendorId.ToString()),
                new ReportParameter("VendorName", param.VendorList.FirstOrDefault(x => x.Value == param.VendorId.ToString()).Text),
                 new ReportParameter("status", param.PaymentStatus.ToString()),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryMDSRpt
        [UserAuthorize(menuId:71)]
        public ActionResult InventoryMDSRpt()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryMDSRpt(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_MDSRpt";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                //new ReportParameter("aquisitiontype", param.aquisitiontype.ToString()),
                new ReportParameter("fBranch", param.fBranch.ToString()),
                new ReportParameter("tBranch", param.tBranch.ToString()),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("FromBranch", param.BranchList.FirstOrDefault(x => x.Value == param.fBranch.ToString()).Text),
                new ReportParameter("ToBranch", param.BranchList.FirstOrDefault(x => x.Value == param.tBranch.ToString()).Text)
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }

        [HttpPost]
        public ActionResult InventoryMDSSummaryRpt(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_MDSSummaryRpt";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("fBranch", param.fBranch.ToString()),
                new ReportParameter("tBranch", param.tBranch.ToString()),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("FromBranch", param.BranchList.FirstOrDefault(x => x.Value == param.fBranch.ToString()).Text),
                new ReportParameter("ToBranch", param.BranchList.FirstOrDefault(x => x.Value == param.tBranch.ToString()).Text)
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View("InventoryMDSRpt", param);
        }

        [HttpPost]
        public ActionResult InventoryMDSMasterSummaryRpt(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_MDSMasterSummaryRpt";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("fBranch", param.fBranch.ToString()),
                new ReportParameter("tBranch", param.tBranch.ToString()),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("FromBranch", param.BranchList.FirstOrDefault(x => x.Value == param.fBranch.ToString()).Text),
                new ReportParameter("ToBranch", param.BranchList.FirstOrDefault(x => x.Value == param.tBranch.ToString()).Text)
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View("InventoryMDSRpt", param);
        }
        #endregion

        #region RequestandAcknowledgeRpt
        [UserAuthorize(menuId:1096)]
        public ActionResult RequestandAcknowledgeRpt()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult RequestandAcknowledgeRpt(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_RequestandAckRpt";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("branch_id", param.fBranch.ToString()),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("ReqBranch", param.BranchList.FirstOrDefault(x => x.Value == param.fBranch.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion
        #region BranchTransferRpt
        [UserAuthorize(menuId:1088)]
        public ActionResult InventoryBranchTranferRpt()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryBranchTranferRpt(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_DispatchBranchList";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("fBranch", param.fBranch.ToString()),
                new ReportParameter("tBranch", param.tBranch.ToString()),
                new ReportParameter("FromBranch", param.BranchList.FirstOrDefault(x => x.Value == param.fBranch.ToString()).Text),
                new ReportParameter("ToBranch", param.BranchList.FirstOrDefault(x => x.Value == param.tBranch.ToString()).Text)
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion
        #region InventoryNotAcknowledgeReport
        [UserAuthorize(menuId:1086)]
        public ActionResult InventoryNotAcknowledgeRpt()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryNotAcknowledgeRpt(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_NotAcknowledgeRpt";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                //new ReportParameter("aquisitiontype", param.aquisitiontype.ToString()),
                new ReportParameter("fBranch", param.fBranch.ToString()),
                new ReportParameter("tBranch", param.tBranch.ToString()),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("FromBranch", param.BranchList.FirstOrDefault(x => x.Value == param.fBranch.ToString()).Text),
                new ReportParameter("ToBranch", param.BranchList.FirstOrDefault(x => x.Value == param.tBranch.ToString()).Text)
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryAllDispatchedReport
        [UserAuthorize(menuId:1107)]
        public ActionResult InventoryDispatchedReport()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryDispatchedReport(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_DispatchedRptWithDepartments";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("fBranch", param.fBranch.ToString()),
                new ReportParameter("tBranch", param.tBranch.ToString()),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("FromBranch", param.BranchList.FirstOrDefault(x => x.Value == param.fBranch.ToString()).Text),
                new ReportParameter("ToBranch", param.BranchList.FirstOrDefault(x => x.Value == param.tBranch.ToString()).Text),
                new ReportParameter("DepartmentId", param.DepartmentId.ToString())
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region InventoryAllDisposedReport
        [UserAuthorize(menuId:1108)]
        public ActionResult InventoryDisposedReport()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryDisposedReport(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "InventoryDisposedReport";
           // GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                //new ReportParameter("fBranch", param.fBranch.ToString()),
               // new ReportParameter("tBranch", param.tBranch.ToString()),
               // new ReportParameter("ProductId", param.ProductId.ToString()),
               // new ReportParameter("FromBranch", param.BranchList.FirstOrDefault(x => x.Value == param.fBranch.ToString()).Text),
               // new ReportParameter("ToBranch", param.BranchList.FirstOrDefault(x => x.Value == param.tBranch.ToString()).Text),
                //new ReportParameter("DepartmentId", param.DepartmentId.ToString())
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion
        #region InventoryReturnedDispatchReports
        [UserAuthorize(menuId:1109)]
        public ActionResult InventoryReturnedDispatchReports()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult InventoryReturnedDispatchReports(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ReturnedDispatchRpt";
            // GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromDate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("toDate", String.Format(param.DateTo,"yyyy-MM-dd")),
                //new ReportParameter("fBranch", param.fBranch.ToString()),
               // new ReportParameter("tBranch", param.tBranch.ToString()),
               // new ReportParameter("ProductId", param.ProductId.ToString()),
               // new ReportParameter("FromBranch", param.BranchList.FirstOrDefault(x => x.Value == param.fBranch.ToString()).Text),
               // new ReportParameter("ToBranch", param.BranchList.FirstOrDefault(x => x.Value == param.tBranch.ToString()).Text),
                //new ReportParameter("DepartmentId", param.DepartmentId.ToString())
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion
        #region StockStatementRpt
        public ActionResult StockStatementRpt()
        {
            var data = CreateTicketParam();
            data.BranchList.Remove(data.BranchList.First());
            data.ProductList.Remove(data.ProductList.First());
            return View(data);
        }
        [HttpPost]
        public ActionResult StockStatementRpt(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_StockStatementRpt";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("ProductId", param.ProductId.ToString()),
                new ReportParameter("ProductName", param.ProductList.FirstOrDefault(x => x.Value == param.ProductId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region ExtractBranchData
        public ActionResult ExtractBranchData()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult ExtractBranchData(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ExtractDataBranch";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region ExtractDepartmentData
        public ActionResult ExtractDepartmentData()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult ExtractDepartmentData(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ExtractDataDepartment";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region ExtractAssetType
        public ActionResult ExtractAssetType()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult ExtractAssetType(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "FA_ExtractAssetType";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("groupId", param.BranchId.ToString()),
                new ReportParameter("AssetGroupName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region ExtractEmployee
        public ActionResult ExtractEmployee()
        {
            return View(CreateTicketParam());
        }
        [HttpPost]
        public ActionResult ExtractEmployee(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);
            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_ExtractDataEmployee";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }
        #endregion

        #region FuelRequest
        [UserAuthorize(menuId:1101)]
        public ActionResult FuelRequest()
        {
            var param = CreateTicketParam();
            return View(param);
        }
        [HttpPost]
        public ActionResult FuelRequest(ReportParameterVM param)
        {
            var reportby = @Session["FullName"].ToString();
            param = CreateTicketParam(param);

            _reportViewerRDL.ServerReport.ReportPath = ConfigurationManager.AppSettings["SSRS_Reports"] + "Inv_FuelRequestDetailReport";
            GetBranchDetails(param.BranchId);
            List<ReportParameter> parameters = new List<ReportParameter>
            {
                new ReportParameter("ReportBy",reportby.ToString()),
                new ReportParameter("fromdate", String.Format(param.DateFrom,"yyyy-MM-dd")),
                new ReportParameter("todate", String.Format(param.DateTo,"yyyy-MM-dd")),
                new ReportParameter("BranchId", param.BranchId.ToString()),
                new ReportParameter("BranchName", param.BranchList.FirstOrDefault(x => x.Value == param.BranchId.ToString()).Text),
                new ReportParameter("FuelCategory", param.FuelCategory.ToString()),
                new ReportParameter("VehicleCategory", param.VehicleCategory.ToString()),
                new ReportParameter("FuelVendor",param.FuelVendor==null? 0.ToString():param.FuelVendor.Trim().ToString()),
                new ReportParameter("CouponNo",param.CouponNo==null? 0.ToString():param.CouponNo.Trim().ToString()),
                new ReportParameter("FuelStatus", param.FuelStatus.ToString()),
                new ReportParameter("UserId", param.UserId==null? 0.ToString():param.UserId.ToString()),

            };
            _parameters.AddRange(parameters);
            _reportViewerRDL.ServerReport.SetParameters(_parameters);
            ViewBag.ReportViewer = _reportViewerRDL;
            return View(param);
        }


        #endregion
        #endregion
        public void GetBranchDetails(int branchId)
        {
            _company.Branches = CompanyServices.GetBranch(branchId);
            _parameters.AddRange(new List<ReportParameter>{
                    new ReportParameter("Address", _company.Branches.BRANCH_ADDRESS),
                    new ReportParameter("Phone", "Phone : " + _company.Branches.BRANCH_PHONE),
                    new ReportParameter("Fax", "Fax : " + _company.Branches.BRANCH_FAX)
                });
        }

        public JsonResult GetProductNameFromProductGroupName(int groupid, int branchid)
        {
            if (branchid > 0 && groupid > 0)
            {
                var data = _dropDownList.ProductWithProductGroupAndBranchAssignList(groupid, branchid).ToList();
                return Json(data);
            }
            if (branchid == 0 && groupid > 0)
            {
                var data = _dropDownList.ProductWithProductGroupList(groupid).ToList();
                return Json(data);
            }
            else
            {
                var data = _dropDownList.ProductList();
                data.Insert(0, new SelectListItem { Text = "All", Value = "0" });
                return Json(data);
            }
        }
        public JsonResult GetDepartmentNameFromBranchName(int branchid)
        {
            if (branchid > 0)
            {
                var data = _dropDownList.DepartmentFromBranchList(branchid).ToList();
                return Json(data);
            }
            else
            {
                var data = _dropDownList.DepartmentList();
                data.Insert(0, new SelectListItem { Text = "All", Value = "0" });
                return Json(data);
            }
        }
    }
}