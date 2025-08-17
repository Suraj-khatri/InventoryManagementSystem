using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain.ViewModel
{
    public class DashboardVM
    {
        public DashboardVM()
        {
            StockLevelVM = new INBranchVM();
            recentApprovedRequisitionListVM = new InRequisitionMessageVM();
            recentDispatchRequisitionListVM = new InRequisitionMessageVM();
            recentAcknowledgeRequisitionListVM = new InRequisitionMessageVM();
            recentPurchasesListVM = new InPurchaseVM();
        }
        public INBranchVM StockLevelVM { get; set; }
        public InRequisitionMessageVM recentApprovedRequisitionListVM { get; set; }
        public InRequisitionMessageVM recentDispatchRequisitionListVM { get; set; }
        public InRequisitionMessageVM recentAcknowledgeRequisitionListVM { get; set; }
        public InPurchaseVM recentPurchasesListVM { get; set; }

        public List<INBranchVM> stockLevelList { get; set; }
        public List<InRequisitionMessageVM> recentApprovedRequisitionList { get; set; }
        public List<InRequisitionMessageVM> recentDispatchRequisitionList { get; set; }
        public List<InRequisitionMessageVM> recentAcknowledgeRequisitionList { get; set; }
        public List<InPurchaseVM> recentPurchasesList { get; set; }

    }
}
