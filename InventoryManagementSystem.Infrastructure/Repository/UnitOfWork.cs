using InventoryManagementSystem.Data;
using InventoryManagementSystem.Infrastructure.Interface;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        Entities _context = null;

        public UnitOfWork()
        {
            _context = new Entities();
        }

        public UnitOfWork(Entities dbcontext)
        {
            _context = dbcontext;
        }

        private ICompanyRepo _icompany;
        public ICompanyRepo ICompany
        {
            get { return _icompany ?? (_icompany = new CompanyRepo(_context)); }
        }

        private IInItemRepo _initem;
        public IInItemRepo InItem
        {
            get { return _initem ?? (_initem = new InItemRepo(_context)); }
        }

        private IInProductRepo _inproduct;
        public IInProductRepo InProduct
        {
            get { return _inproduct ?? (_inproduct = new InProductRepo(_context)); }
        }

        private IVendorRepo _invendor;
        public IVendorRepo InVendor
        {
            get { return _invendor ?? (_invendor = new VendorRepo(_context)); }
        }

        private IBranchRepo _inbranch;
        public IBranchRepo InBranch
        {
            get { return _inbranch ?? (_inbranch = new BranchRepo(_context)); }
        }

        private IVendorAssignRepo _invendorassign;
        public IVendorAssignRepo InVendorAssign
        {
            get { return _invendorassign ?? (_invendorassign = new VendorAssignRepo(_context)); }
        }

        private IBranchAssignRepo _inbranchassign;
        public IBranchAssignRepo InBranchAssign
        {
            get { return _inbranchassign ?? (_inbranchassign = new BranchAssignRepo(_context)); }
        }

        private IPurchaseOrderMessageRepo _purchaseordermessage;
        public IPurchaseOrderMessageRepo PurchaseOrderMessage
        {
            get { return _purchaseordermessage ?? (_purchaseordermessage = new PurchaseOrderMessageRepo(_context)); }
        }

        private IPurchaseOrderMessageHistoryRepo _purchaseordermessagehistory;
        public IPurchaseOrderMessageHistoryRepo PurchaseOrderMessageHistory
        {
            get { return _purchaseordermessagehistory ?? (_purchaseordermessagehistory = new PurchaseOrderMessageHistoryRepo(_context)); }
        }

        private IPurchaseOrderRepo _purchaseorder;
        public IPurchaseOrderRepo PurchaseOrder
        {
            get { return _purchaseorder ?? (_purchaseorder = new PurchaseOrderRepo(_context)); }
        }

        private IInPurchaseReturnRepo _inpurchasereturn;
        public IInPurchaseReturnRepo PurchaseReturn
        {
            get { return _inpurchasereturn ?? (_inpurchasereturn = new InPurchaseReturnRepo(_context)); }
        }

        private IInPurchaseReturnDetailsRepo _inpurchasereturndetails;
        public IInPurchaseReturnDetailsRepo PurchaseReturnDetails
        {
            get { return _inpurchasereturndetails ?? (_inpurchasereturndetails = new InPurchaseReturnDetailsRepo(_context)); }
        }

        private IInPurchaseRepo _inpurchase;
        public IInPurchaseRepo InPurchase
        {
            get { return _inpurchase ?? (_inpurchase = new InPurchaseRepo(_context)); }
        }

        private ITempPurchaseRepo _temppurchase;
        public ITempPurchaseRepo TempPurchase
        {
            get { return _temppurchase ?? (_temppurchase = new TempPurchaseRepo(_context)); }
        }

        private ITempPurchaseOtherRepo _temppurchaseother;
        public ITempPurchaseOtherRepo TempPurchaseOther
        {
            get { return _temppurchaseother ?? (_temppurchaseother = new TempPurchaseOtherRepo(_context)); }
        }

        private IStaticTempDispatchRepo _statictempdispatch;
        public IStaticTempDispatchRepo StaticTempDispatch
        {
            get { return _statictempdispatch ?? (_statictempdispatch = new StaticTempDispatchRepo(_context)); }
        }
        private ITempDisposeRepo _tempdispose;
        public ITempDisposeRepo TempDispose
        {
            get { return _tempdispose ?? (_tempdispose = new TempDisposeRepo(_context)); }
        }

        private ITempDisposeOtherRepo _tempdisposeother;
        public ITempDisposeOtherRepo TempDisposeOther
        {
            get { return _tempdisposeother ?? (_tempdisposeother = new TempDisposeOtherRepo(_context)); }
        }

        private IBillInfoRepo _billinfo;
        public IBillInfoRepo BillInfo
        {
            get { return _billinfo ?? (_billinfo = new BillInfoRepo(_context)); }
        }

        private IBillSettingRepo _billsetting;
        public IBillSettingRepo BillSetting
        {
            get { return _billsetting ?? (_billsetting = new BillSettingRepo(_context)); }
        }

        private IOtherBillsInfoRepo _otherbillsinfo;
        public IOtherBillsInfoRepo OtherBillsInfo
        {
            get { return _otherbillsinfo ?? (_otherbillsinfo = new OtherBillsInfoRepo(_context)); }
        }

        private IInRequisitionMessageRepo _inrequisitionmessage;
        public IInRequisitionMessageRepo InRequisitionMessage
        {
            get { return _inrequisitionmessage ?? (_inrequisitionmessage = new InRequisitionMessageRepo(_context)); }
        }

        private IInRequisitionRepo _inrequisition;
        public IInRequisitionRepo InRequisition
        {
            get { return _inrequisition ?? (_inrequisition = new InRequisitionRepo(_context)); }
        }

        private IInRequisitionDetailRepo _inrequisitiondetail;
        public IInRequisitionDetailRepo InRequisitionDetail
        {
            get { return _inrequisitiondetail ?? (_inrequisitiondetail = new InRequisitionDetailRepo(_context)); }
        }

        private IInRequisitionDetailOtherRepo _inrequisitiondetailother;
        public IInRequisitionDetailOtherRepo InRequisitionDetailOther
        {
            get { return _inrequisitiondetailother ?? (_inrequisitiondetailother = new InRequisitionDetailOtherRepo(_context)); }
        }

        private IInReceivedRepo _inreceived;
        public IInReceivedRepo InReceived
        {
            get { return _inreceived ?? (_inreceived = new InReceivedRepo(_context)); }
        }

        private IInReceivedMessageRepo _inreceivedmessage;
        public IInReceivedMessageRepo InReceivedMessage
        {
            get { return _inreceivedmessage ?? (_inreceivedmessage = new InReceivedMessageRepo(_context)); }
        }

        private IInDispatchRepo _indispatch;
        public IInDispatchRepo InDispatch
        {
            get { return _indispatch ?? (_indispatch = new InDispatchRepo(_context)); }
        }

        private IInDispatchMessageRepo _indispatchmessage;
        public IInDispatchMessageRepo InDispatchMessage
        {
            get { return _indispatchmessage ?? (_indispatchmessage = new InDispatchMessageRepo(_context)); }
        }

        private IDepartmentRepo _indepart;
        public IDepartmentRepo InDepartment
        {
            get { return _indepart ?? (_indepart = new DepartmentRepo(_context)); }
        }

        private IEmployeeRepo _employee;
        public IEmployeeRepo Employee
        {
            get { return _employee ?? (_employee = new EmployeeRepo(_context)); }
        }

        private IAdminRepo _admin;
        public IAdminRepo Admin
        {
            get { return _admin ?? (_admin = new AdminRepo(_context)); }
        }

     
        private IEmployeeLogRepo _emplog;
        public IEmployeeLogRepo EmployeeLog
        {
            get { return _emplog ?? (_emplog = new EmployeeLogRepo(_context)); }
        }

        private IEmployeeContractRepo _empcontract;
        public IEmployeeContractRepo EmployeeContract
        {
            get { return _empcontract ?? (_empcontract = new EmployeeContractRepo(_context)); }
        }
        private ISuperVisorAssignmentRepo _supervisorassignment;
        public ISuperVisorAssignmentRepo SuperVisorAssignment
        {
            get { return _supervisorassignment ?? (_supervisorassignment = new SuperVisorAssignmentRepo(_context)); }
        }
        
        private IInTempRequisitionRepo _intempreq;
        public IInTempRequisitionRepo InTempRequisition
        {
            get { return _intempreq ?? (_intempreq = new InTempRequisitionRepo(_context)); }
        }

        private ISerialProductStockRepo _serialproductstock;
        public ISerialProductStockRepo SerialProductStock
        {
            get { return _serialproductstock ?? (_serialproductstock = new SerialProductStockRepo(_context)); }
        }

        private ISerialProductStockHistoryRepo _serialproductstockhistory;
        public ISerialProductStockHistoryRepo SerialProductStockHistory
        {
            get { return _serialproductstockhistory ?? (_serialproductstockhistory = new SerialProductStockHistoryRepo(_context)); }
        }
        private ITempReturnProductRepo _tempreturnproduct;
        public ITempReturnProductRepo TempReturnProduct
        {
            get { return _tempreturnproduct ?? (_tempreturnproduct = new TempReturnProductRepo(_context)); }
        }
        private ISerialProductTransferBranchRepo _serialproducttransferbranch;
        public ISerialProductTransferBranchRepo SerialProductTransferBranch
        {
            get { return _serialproducttransferbranch ?? (_serialproducttransferbranch = new SerialProductTransferBranchRepo(_context)); }
        }
        private IAssetItemRepo _assetitem;
        public IAssetItemRepo AssetItem
        {
            get { return _assetitem ?? (_assetitem = new AssetItemRepo(_context)); }
        }

        private IAssetProductRepo _assetproduct;
        public IAssetProductRepo AssetProduct
        {
            get { return _assetproduct ?? (_assetproduct = new AssetProductRepo(_context)); }
        }

        private IAssetBranchRepo _assetbranch;
        public IAssetBranchRepo AssetBranch
        {
            get { return _assetbranch ?? (_assetbranch = new AssetBranchRepo(_context)); }
        }

        private IUserRoleRepo _userrole;
        public IUserRoleRepo UserRole
        {
            get { return _userrole ?? (_userrole = new UserRoleRepo(_context)); }
        }

        private IRoleDetailsRepo _roledetail;
        public IRoleDetailsRepo RoleDetails
        {
            get { return _roledetail ?? (_roledetail = new RoleDetailsRepo(_context)); }
        }

        private IStaticDataDetailRepo _staticdatadetail;
        public IStaticDataDetailRepo StaticDataDetail
        {
            get { return _staticdatadetail ?? (_staticdatadetail = new StaticDataDetailRepo(_context)); }
        }

        private IStaticDataTypeRepo _staticdatatype;
        public IStaticDataTypeRepo StaticDataType
        {
            get { return _staticdatatype ?? (_staticdatatype = new StaticDataTypeRepo(_context)); }
        }

        private IUserFunctionRepo _userfunction;
        public IUserFunctionRepo UserFunction
        {
            get { return _userfunction ?? (_userfunction = new UserFunctionRepo(_context)); }
        }
       
        private IAssetRequisitionMessageRepo _assetreqmes;
        public IAssetRequisitionMessageRepo AssetRequisitionMessage
        {
            get { return _assetreqmes ?? (_assetreqmes = new AssetRequisitionMessageRepo(_context)); }
        }

        private IAssetRequisitionRepo _assetreq;
        public IAssetRequisitionRepo AssetRequisition
        {
            get { return _assetreq ?? (_assetreq = new AssetRequisitionRepo(_context)); }
        }

        private IAssetInventoryTempRepo _assetinvtemp;
        public IAssetInventoryTempRepo AssetInventoryTemp
        {
            get { return _assetinvtemp ?? (_assetinvtemp = new AssetInventoryTempRepo(_context)); }
        }

        private IChangesApprovalQueueRepo _chgappque;
        public IChangesApprovalQueueRepo ChangesApprovalQueue
        {
            get { return _chgappque ?? (_chgappque = new ChangesApprovalQueueRepo(_context)); }
        }

        private IAssetNumberSequenceRepo _assetnumseq;
        public IAssetNumberSequenceRepo AssetNumberSequence
        {
            get { return _assetnumseq ?? (_assetnumseq = new AssetNumberSequenceRepo(_context)); }
        }

        private INotificationTypeRepo _notificationtype;
        public INotificationTypeRepo NotificationType
        {
            get { return _notificationtype ?? (_notificationtype = new NotificationTypeRepo(_context)); }
        }

        private INotificationRepo _notification;
        public INotificationRepo Notification
        {
            get { return _notification ?? (_notification = new NotificationRepo(_context)); }
        }

        private INotificationUserRepo _notificationuser;
        public INotificationUserRepo NotificationUser
        {
            get { return _notificationuser ?? (_notificationuser = new NotificationUserRepo(_context)); }
        }

        private IInNotificationsRepo _imsnotification;
        public IInNotificationsRepo InNotifications
        {
            get { return _imsnotification ?? (_imsnotification = new InNotificationsRepo(_context)); }
        }
        private IFuelRequestMessageRepo _furlreqmes;
        public IFuelRequestMessageRepo FuelRequestMessage
        {
            get { return _furlreqmes ?? (_furlreqmes = new FuelRequestMessageRepo(_context)); }
        }
        private IFuelRequestRepo _fuelRequest;
        public IFuelRequestRepo FuelRequest
        {
            get { return _fuelRequest ?? (_fuelRequest = new FuelRequestRepo(_context)); }
        }
        private IInDisposeMessageRepo _disposemes;
        public IInDisposeMessageRepo InDisposeMessage
        {
            get { return _disposemes ?? (_disposemes = new InDisposeMessageRepo(_context)); }
        }
        private IInDisposeDetailsRepo _disposedetails;
        public IInDisposeDetailsRepo InDisposeDetails
        {
            get { return _disposedetails ?? (_disposedetails = new InDisposeDetailsRepo(_context)); }


        }
      
        public void Dispose()
        {
            _context.Dispose();
        }
        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {
                throw ex;
            }
        }
        public Task<int> SaveAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (System.Data.Entity.Core.OptimisticConcurrencyException ex)
            {

                throw ex;
            }
        }
    }
}
