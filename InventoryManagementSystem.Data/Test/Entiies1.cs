using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace InventoryManagementSystem.Data.Test
{
    public partial class Entiies1 : DbContext
    {
        public Entiies1()
            : base("name=Entiies1")
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<ASSET_BRANCH> ASSET_BRANCH { get; set; }
        public virtual DbSet<ASSET_INVENTORY_TEMP> ASSET_INVENTORY_TEMP { get; set; }
        public virtual DbSet<ASSET_ITEM> ASSET_ITEM { get; set; }
        public virtual DbSet<ASSET_NumberSequence> ASSET_NumberSequence { get; set; }
        public virtual DbSet<ASSET_PRODUCT> ASSET_PRODUCT { get; set; }
        public virtual DbSet<ASSET_REQUISITION> ASSET_REQUISITION { get; set; }
        public virtual DbSet<ASSET_REQUISITION_MESSAGE> ASSET_REQUISITION_MESSAGE { get; set; }
        public virtual DbSet<Bill_info> Bill_info { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<changesApprovalQueue> changesApprovalQueue { get; set; }
        public virtual DbSet<COMPANY> COMPANY { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<departments> departments { get; set; }
        public virtual DbSet<emp_log> emp_log { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Employee_Contract> Employee_Contract { get; set; }
        public virtual DbSet<Fuel_Requests> Fuel_Requests { get; set; }
        public virtual DbSet<Fuel_Requests_Message> Fuel_Requests_Message { get; set; }
        public virtual DbSet<IN_BRANCH> IN_BRANCH { get; set; }
        public virtual DbSet<In_Branch_Department> In_Branch_Department { get; set; }
        public virtual DbSet<IN_BUDGET> IN_BUDGET { get; set; }
        public virtual DbSet<IN_DISPATCH> IN_DISPATCH { get; set; }
        public virtual DbSet<IN_DISPATCH_MESSAGE> IN_DISPATCH_MESSAGE { get; set; }
        public virtual DbSet<IN_DISPOSAL> IN_DISPOSAL { get; set; }
        public virtual DbSet<In_Dispose_Details> In_Dispose_Details { get; set; }
        public virtual DbSet<In_Dispose_Message> In_Dispose_Message { get; set; }
        public virtual DbSet<IN_ITEM> IN_ITEM { get; set; }
        public virtual DbSet<IN_Notifications> IN_Notifications { get; set; }
        public virtual DbSet<IN_PRODUCT> IN_PRODUCT { get; set; }
        public virtual DbSet<IN_PURCHASE> IN_PURCHASE { get; set; }
        public virtual DbSet<In_Purchase_Department> In_Purchase_Department { get; set; }
        public virtual DbSet<In_PurchaseReturn> In_PurchaseReturn { get; set; }
        public virtual DbSet<In_PurchaseReturnDetails> In_PurchaseReturnDetails { get; set; }
        public virtual DbSet<IN_RECEIVED> IN_RECEIVED { get; set; }
        public virtual DbSet<IN_RECEIVED_MESSAGE> IN_RECEIVED_MESSAGE { get; set; }
        public virtual DbSet<IN_Requisition> IN_Requisition { get; set; }
        public virtual DbSet<IN_Requisition_Detail> IN_Requisition_Detail { get; set; }
        public virtual DbSet<IN_Requisition_Detail_Other> IN_Requisition_Detail_Other { get; set; }
        public virtual DbSet<IN_Requisition_Message> IN_Requisition_Message { get; set; }
        public virtual DbSet<IN_SALES> IN_SALES { get; set; }
        public virtual DbSet<In_Static_Temp_Dispatch> In_Static_Temp_Dispatch { get; set; }
        public virtual DbSet<In_Static_Temp_Dispatch_Other> In_Static_Temp_Dispatch_Other { get; set; }
        public virtual DbSet<IN_Temp_Requisition> IN_Temp_Requisition { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationType> NotificationType { get; set; }
        public virtual DbSet<NotificationUser> NotificationUser { get; set; }
        public virtual DbSet<OtherBillsInfo> OtherBillsInfo { get; set; }
        public virtual DbSet<Purchase_Order> Purchase_Order { get; set; }
        public virtual DbSet<Purchase_Order_Message> Purchase_Order_Message { get; set; }
        public virtual DbSet<Purchase_Order_Message_History> Purchase_Order_Message_History { get; set; }
        public virtual DbSet<Purchase_Order_Received> Purchase_Order_Received { get; set; }
        public virtual DbSet<roles_detail> roles_detail { get; set; }
        public virtual DbSet<SerialProduct_TransferBranch> SerialProduct_TransferBranch { get; set; }
        public virtual DbSet<SerialProductStock> SerialProductStock { get; set; }
        public virtual DbSet<SerialProductStockHistory> SerialProductStockHistory { get; set; }
        public virtual DbSet<StaticDataDetail> StaticDataDetail { get; set; }
        public virtual DbSet<StaticDataType> StaticDataType { get; set; }
        public virtual DbSet<SuperVisroAssignment> SuperVisroAssignment { get; set; }
        public virtual DbSet<Temp_Dispose> Temp_Dispose { get; set; }
        public virtual DbSet<Temp_Dispose_Other> Temp_Dispose_Other { get; set; }
        public virtual DbSet<Temp_Purchase> Temp_Purchase { get; set; }
        public virtual DbSet<Temp_Purchase_Other> Temp_Purchase_Other { get; set; }
        public virtual DbSet<Temp_Return_Product> Temp_Return_Product { get; set; }
        public virtual DbSet<user_function> user_function { get; set; }
        public virtual DbSet<user_role> user_role { get; set; }
        public virtual DbSet<Vendor_Bid_Price> Vendor_Bid_Price { get; set; }
        public virtual DbSet<BillSetting> BillSetting { get; set; }
        public virtual DbSet<Fiscal_Month> Fiscal_Month { get; set; }
        public virtual DbSet<FiscalYear> FiscalYear { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admins>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.UserPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.Post)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.Cell_phone)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.agent_id)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.LOGINTIMEFROM)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.LOGINTIMETO)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.REPORTACCESS)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.session)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.user_type)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.new_user)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.approved_by)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.branch_level_access)
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .Property(e => e.forceChangePwd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Admins>()
                .HasMany(e => e.user_role)
                .WithRequired(e => e.Admins)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ASSET_BRANCH>()
                .Property(e => e.ASSET_AC)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_BRANCH>()
                .Property(e => e.DEPRECIATION_EXP_AC)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_BRANCH>()
                .Property(e => e.ACCUMULATED_DEP_AC)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_BRANCH>()
                .Property(e => e.WRITE_OFF_EXP_AC)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_BRANCH>()
                .Property(e => e.SALES_PROFIT_LOSS_AC)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_BRANCH>()
                .Property(e => e.MAINTAINANCE_EXP_AC)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_BRANCH>()
                .Property(e => e.ASSET_NEXT_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_BRANCH>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_BRANCH>()
                .Property(e => e.MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.asset_number)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.purchase_value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.acc_depriciation)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.narration)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.asset_status)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.asset_serial)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.in_out)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.asset_type)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.forwarded_by)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.rejection_reason)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.old_asset_no)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.old_asset_code)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .Property(e => e.brand)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_INVENTORY_TEMP>()
                .HasMany(e => e.changesApprovalQueue)
                .WithRequired(e => e.ASSET_INVENTORY_TEMP)
                .HasForeignKey(e => e.dataid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ASSET_ITEM>()
                .Property(e => e.item_name)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_ITEM>()
                .Property(e => e.item_desc)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_ITEM>()
                .Property(e => e.Product_Code)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_ITEM>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_ITEM>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_ITEM>()
                .HasMany(e => e.ASSET_PRODUCT)
                .WithRequired(e => e.ASSET_ITEM)
                .HasForeignKey(e => e.item_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ASSET_NumberSequence>()
                .Property(e => e.SeqDateFormat)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_NumberSequence>()
                .Property(e => e.NumSequence)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_NumberSequence>()
                .Property(e => e.SequenceSep)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_NumberSequence>()
                .Property(e => e.CompSeparator)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_NumberSequence>()
                .Property(e => e.BranchSeparator)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_NumberSequence>()
                .Property(e => e.AssetSeparator)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_PRODUCT>()
                .Property(e => e.porduct_code)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_PRODUCT>()
                .Property(e => e.product_desc)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_PRODUCT>()
                .Property(e => e.ASSET_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_PRODUCT>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_PRODUCT>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_REQUISITION>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ASSET_REQUISITION_MESSAGE>()
                .Property(e => e.approval_message)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_REQUISITION_MESSAGE>()
                .Property(e => e.priority)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_REQUISITION_MESSAGE>()
                .Property(e => e.narration)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_REQUISITION_MESSAGE>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<ASSET_REQUISITION_MESSAGE>()
                .HasMany(e => e.ASSET_REQUISITION)
                .WithRequired(e => e.ASSET_REQUISITION_MESSAGE)
                .HasForeignKey(e => e.requistion_message_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.party_code)
                .IsUnicode(false);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.billno)
                .IsUnicode(false);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.taxable_amt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.nontax_amt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.vat_amt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.bill_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.bill_discount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.paid_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.bill_type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.bill_notes)
                .IsUnicode(false);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.entered_by)
                .IsUnicode(false);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.paid_by)
                .IsUnicode(false);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.VendorName)
                .IsUnicode(false);

            modelBuilder.Entity<Bill_info>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_SHORT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_CITY)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_FAX)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_POST_BOX)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.EPS)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_MOBILE)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_COUNTRY)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_Municipality)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_DISTRICT)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.CONTACT_PERSON)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.IBT_Account)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.Batch_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.BRANCH_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.stockAc)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.expensesAc)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .Property(e => e.transitAc)
                .IsUnicode(false);

            modelBuilder.Entity<Branches>()
                .HasMany(e => e.COMPANY)
                .WithOptional(e => e.Branches)
                .HasForeignKey(e => e.BranchId);

            modelBuilder.Entity<Branches>()
                .HasMany(e => e.IN_BRANCH)
                .WithRequired(e => e.Branches)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.tableName)
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.identifierField)
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.modType)
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.createdBy)
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.approveFlag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.changeStatus)
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.tableDescription)
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.reasonForRejection)
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.editLink)
                .IsUnicode(false);

            modelBuilder.Entity<changesApprovalQueue>()
                .Property(e => e.forwarded_to)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_SHORT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_ADDRESS2)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.POST_BOX)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.EPS)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_PHONE_NO)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_FAX_NO)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_CONTACT_PERSON)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_MAP_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.COMP_URL)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<COMPANY>()
                .Property(e => e.MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerCode)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerTelNo)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerTelNoSec)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerPANNo)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomeFax)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactPersonFirst)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactPersonMobile1)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactPersonEmail1)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactPersonSec)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactPersonMobile2)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactPersonEmail2)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactPersonThird)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactPersonMobile3)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ContactPersonEmail3)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerWebsite)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.BusinessDetails)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.FacilityDetails)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CUSTOMERMOBILENO)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Vendor_Bid_Price)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.vendor_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.DEPARTMENT_SHORT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.DEPARTMENT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.PHONE_EXTENSION)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.FAX)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.MOBILE_DEPARTMENT_HEAD)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.EMAIL_DEPARTMENT_HEAD)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<departments>()
                .Property(e => e.MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<emp_log>()
                .Property(e => e.flag)
                .IsUnicode(false);

            modelBuilder.Entity<emp_log>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<emp_log>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EMP_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.SALUTATION)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FIRST_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.MIDDLE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LAST_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.OFFICE_PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.HOME_PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.OFFICE_MOBILE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PERSONAL_MOBILE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.OFFICE_FAX)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PERSONAL_FAX)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.OFFICIAL_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PERSONAL_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.GENDER)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.BLOOD_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.NATIONALITY)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.DRIVARY_LICENCE_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PASSPORT_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.MERITAL_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PAN_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.MAP_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TEMP_STREET_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TEMP_HOUSE_NO)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TEMP_MUNICIPALITY_VDC)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TEMP_DISTRICT)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TEMP_PROVINCE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TEMP_COUNTRY)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PER_STREET_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PER_HOUSE_NO)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PER_MUNICIPALITY_VDC)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PER_DISTRICT)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PER_PROVINCE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PER_COUNTRY)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.AVAAILED_VEHICLE_FACILITY)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.AVAILED_HOUSE_RENT_FACILITY)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.IS_PENSION_HOLDER)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.IS_DISABLED)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EMP_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EMP_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.MARITAL_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EM_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EM_ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EM_RELATIONSHIP)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EM_CONTACTNO1)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EM_CONTACTNO2)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EM_CONTACTNO3)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EM_EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.SYSTEM_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Individual_Profile_update)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CARD_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EDUCATION_DETAILS)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.OUTSOURCE_COMPANY)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.ATT_CARD_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Rl_group)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.birthdayFlag)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.temp_STATE)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.per_state)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.branch_state)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Admins)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.Name)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.emp_log)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.emp_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Employee_Contract)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.NotificationUser)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee_Contract>()
                .Property(e => e.Created_By)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Contract>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Contract>()
                .Property(e => e.flag)
                .IsUnicode(false);

            modelBuilder.Entity<Fuel_Requests>()
                .Property(e => e.Unit)
                .IsUnicode(false);

            modelBuilder.Entity<Fuel_Requests>()
                .Property(e => e.Requested_Quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Fuel_Requests>()
                .Property(e => e.Recommended_Quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Fuel_Requests>()
                .Property(e => e.Approved_Quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Fuel_Requests>()
                .Property(e => e.Received_Quantity)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Fuel_Requests_Message>()
                .Property(e => e.Requested_Message)
                .IsUnicode(false);

            modelBuilder.Entity<Fuel_Requests_Message>()
                .Property(e => e.Recommended_Message)
                .IsUnicode(false);

            modelBuilder.Entity<Fuel_Requests_Message>()
                .Property(e => e.Approved_Message)
                .IsUnicode(false);

            modelBuilder.Entity<Fuel_Requests_Message>()
                .Property(e => e.Rejected_Message)
                .IsUnicode(false);

            modelBuilder.Entity<Fuel_Requests_Message>()
                .Property(e => e.Priority)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Fuel_Requests_Message>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Fuel_Requests_Message>()
                .HasMany(e => e.Fuel_Requests)
                .WithRequired(e => e.Fuel_Requests_Message)
                .HasForeignKey(e => e.Fuel_Requests_Message_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_BRANCH>()
                .Property(e => e.SALES_AC)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BRANCH>()
                .Property(e => e.PURCHASE_AC)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BRANCH>()
                .Property(e => e.INVENTORY_AC)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BRANCH>()
                .Property(e => e.COMM_AC)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BRANCH>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BRANCH>()
                .Property(e => e.MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BRANCH>()
                .HasMany(e => e.In_Branch_Department)
                .WithRequired(e => e.IN_BRANCH)
                .HasForeignKey(e => e.InBranchId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_BUDGET>()
                .Property(e => e.FY)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BUDGET>()
                .Property(e => e.RATE)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_BUDGET>()
                .Property(e => e.REMARKS)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BUDGET>()
                .Property(e => e.IS_ACTIVE)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BUDGET>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<IN_BUDGET>()
                .Property(e => e.MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPATCH>()
                .Property(e => e.rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_DISPATCH_MESSAGE>()
                .Property(e => e.dispatch_message)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPATCH_MESSAGE>()
                .Property(e => e.dispatched_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPATCH_MESSAGE>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPATCH_MESSAGE>()
                .Property(e => e.stkFlag)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPATCH_MESSAGE>()
                .HasMany(e => e.IN_RECEIVED_MESSAGE)
                .WithOptional(e => e.IN_DISPATCH_MESSAGE)
                .HasForeignKey(e => e.dis_msg_id);

            modelBuilder.Entity<IN_DISPOSAL>()
                .Property(e => e.disposal_id)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPOSAL>()
                .Property(e => e.prod_code)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPOSAL>()
                .Property(e => e.p_rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_DISPOSAL>()
                .Property(e => e.p_sn_from)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPOSAL>()
                .Property(e => e.p_sn_to)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPOSAL>()
                .Property(e => e.p_batch)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPOSAL>()
                .Property(e => e.entered_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_DISPOSAL>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<In_Dispose_Message>()
                .HasMany(e => e.In_Dispose_Details)
                .WithRequired(e => e.In_Dispose_Message)
                .HasForeignKey(e => e.DisposeMessageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_ITEM>()
                .Property(e => e.item_name)
                .IsUnicode(false);

            modelBuilder.Entity<IN_ITEM>()
                .Property(e => e.item_desc)
                .IsUnicode(false);

            modelBuilder.Entity<IN_ITEM>()
                .Property(e => e.Product_Code)
                .IsUnicode(false);

            modelBuilder.Entity<IN_ITEM>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_ITEM>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_ITEM>()
                .HasMany(e => e.IN_PRODUCT)
                .WithRequired(e => e.IN_ITEM)
                .HasForeignKey(e => e.item_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_Notifications>()
                .Property(e => e.Icon)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.porduct_code)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.product_desc)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.package_unit)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.single_unit)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.unit_discount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.bulk_discount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.purchase_base_price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.sales_base_price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.make)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.model)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.ext_fld1)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.ext_fld2)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .HasMany(e => e.IN_BRANCH)
                .WithRequired(e => e.IN_PRODUCT)
                .HasForeignKey(e => e.PRODUCT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_PRODUCT>()
                .HasMany(e => e.Vendor_Bid_Price)
                .WithRequired(e => e.IN_PRODUCT)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_PURCHASE>()
                .Property(e => e.p_rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_PURCHASE>()
                .Property(e => e.p_sn_from)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PURCHASE>()
                .Property(e => e.p_sn_to)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PURCHASE>()
                .Property(e => e.p_batch)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PURCHASE>()
                .Property(e => e.entered_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PURCHASE>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_PURCHASE>()
                .HasMany(e => e.In_Purchase_Department)
                .WithRequired(e => e.IN_PURCHASE)
                .HasForeignKey(e => e.InPurchaseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<In_PurchaseReturn>()
                .Property(e => e.Discount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<In_PurchaseReturn>()
                .Property(e => e.Vat)
                .HasPrecision(10, 2);

            modelBuilder.Entity<In_PurchaseReturn>()
                .Property(e => e.GrandTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<In_PurchaseReturn>()
                .HasMany(e => e.In_PurchaseReturnDetails)
                .WithRequired(e => e.In_PurchaseReturn)
                .HasForeignKey(e => e.PR_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<In_PurchaseReturnDetails>()
                .Property(e => e.Rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<In_PurchaseReturnDetails>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<In_PurchaseReturnDetails>()
                .Property(e => e.Vat)
                .HasPrecision(10, 2);

            modelBuilder.Entity<IN_RECEIVED>()
                .Property(e => e.unit)
                .IsUnicode(false);

            modelBuilder.Entity<IN_RECEIVED_MESSAGE>()
                .Property(e => e.received_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_RECEIVED_MESSAGE>()
                .Property(e => e.received_msg)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition>()
                .Property(e => e.unit)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Detail>()
                .Property(e => e.unit)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Detail>()
                .Property(e => e.session_id)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Detail_Other>()
                .Property(e => e.sn_from)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Detail_Other>()
                .Property(e => e.sn_to)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Detail_Other>()
                .Property(e => e.batch)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Detail_Other>()
                .Property(e => e.is_approved)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Detail_Other>()
                .Property(e => e.session_id)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.Requ_Message)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.recommed_message)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.Approver_message)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.Delivery_Message)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.Acknowledged_Message)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.priority)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.rejected_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.rejected_message)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.IS_SCHEDULE)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .Property(e => e.UNSCHEDULE_REASON)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .HasMany(e => e.IN_DISPATCH_MESSAGE)
                .WithRequired(e => e.IN_Requisition_Message)
                .HasForeignKey(e => e.req_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .HasMany(e => e.IN_RECEIVED_MESSAGE)
                .WithOptional(e => e.IN_Requisition_Message)
                .HasForeignKey(e => e.req_msg_id);

            modelBuilder.Entity<IN_Requisition_Message>()
                .HasMany(e => e.IN_Requisition)
                .WithRequired(e => e.IN_Requisition_Message)
                .HasForeignKey(e => e.Requistion_message_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .HasMany(e => e.IN_Requisition_Detail)
                .WithRequired(e => e.IN_Requisition_Message)
                .HasForeignKey(e => e.Requistion_message_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_Requisition_Message>()
                .HasMany(e => e.IN_SALES)
                .WithRequired(e => e.IN_Requisition_Message)
                .HasForeignKey(e => e.msg_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IN_SALES>()
                .Property(e => e.s_prod_id)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SALES>()
                .Property(e => e.s_rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_SALES>()
                .Property(e => e.s_discount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_SALES>()
                .Property(e => e.s_amt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IN_SALES>()
                .Property(e => e.sales_type)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SALES>()
                .Property(e => e.s_sn_from)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SALES>()
                .Property(e => e.s_sn_to)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SALES>()
                .Property(e => e.s_batch)
                .IsUnicode(false);

            modelBuilder.Entity<IN_SALES>()
                .Property(e => e.entered_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Temp_Requisition>()
                .Property(e => e.Product_Code)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Temp_Requisition>()
                .Property(e => e.unit)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Temp_Requisition>()
                .Property(e => e.session_id)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Temp_Requisition>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<IN_Temp_Requisition>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .HasMany(e => e.NotificationUser)
                .WithRequired(e => e.Notification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.billno)
                .IsUnicode(false);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.vat_amt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.bill_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.bill_discount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.bill_notes)
                .IsUnicode(false);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.entered_by)
                .IsUnicode(false);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.modified_by)
                .IsUnicode(false);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.paid_by)
                .IsUnicode(false);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.VendorName)
                .IsUnicode(false);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.Invoice)
                .IsUnicode(false);

            modelBuilder.Entity<OtherBillsInfo>()
                .Property(e => e.Received_By)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order>()
                .Property(e => e.rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Purchase_Order>()
                .Property(e => e.amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Purchase_Order_Message>()
                .Property(e => e.order_no)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order_Message>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order_Message>()
                .Property(e => e.vat_amt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Purchase_Order_Message>()
                .Property(e => e.received_desc)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order_Message>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order_Message>()
                .Property(e => e.prod_specfic)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order_Message>()
                .Property(e => e.appropiate_cond)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order_Message>()
                .HasMany(e => e.Purchase_Order)
                .WithRequired(e => e.Purchase_Order_Message)
                .HasForeignKey(e => e.order_message_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Purchase_Order_Message>()
                .HasMany(e => e.Purchase_Order_Message_History)
                .WithRequired(e => e.Purchase_Order_Message)
                .HasForeignKey(e => e.Ord_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Purchase_Order_Message>()
                .HasMany(e => e.Purchase_Order_Received)
                .WithRequired(e => e.Purchase_Order_Message)
                .HasForeignKey(e => e.Order_Message_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Purchase_Order_Message_History>()
                .Property(e => e.from_user)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order_Message_History>()
                .Property(e => e.to_user)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order_Message_History>()
                .Property(e => e.narration)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase_Order_Received>()
                .Property(e => e.Rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Purchase_Order_Received>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SerialProductStock>()
                .Property(e => e.cardNum)
                .IsUnicode(false);

            modelBuilder.Entity<SerialProductStock>()
                .Property(e => e.issuedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SerialProductStock>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<SerialProductStockHistory>()
                .Property(e => e.cardNum)
                .IsUnicode(false);

            modelBuilder.Entity<SerialProductStockHistory>()
                .Property(e => e.issuedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SerialProductStockHistory>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataDetail>()
                .Property(e => e.DETAIL_TITLE)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataDetail>()
                .Property(e => e.DETAIL_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataDetail>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataDetail>()
                .Property(e => e.MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataDetail>()
                .Property(e => e.IS_DELETE)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataDetail>()
                .Property(e => e.add_PF)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataDetail>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataDetail>()
                .Property(e => e.applyOT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataDetail>()
                .Property(e => e.CEAvalue)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataType>()
                .Property(e => e.TYPE_TITLE)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataType>()
                .Property(e => e.TYPE_DESC)
                .IsUnicode(false);

            modelBuilder.Entity<StaticDataType>()
                .HasMany(e => e.StaticDataDetail)
                .WithRequired(e => e.StaticDataType)
                .HasForeignKey(e => e.TYPE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SuperVisroAssignment>()
                .Property(e => e.SUPERVISOR_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<SuperVisroAssignment>()
                .Property(e => e.OPERATION_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<SuperVisroAssignment>()
                .Property(e => e.record_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.session_id)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.serial_start)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.serial_end)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.batch)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.rate)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.account_no)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.ac_type)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.ac_amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.created_by)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.created_date)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase>()
                .Property(e => e.flag)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase_Other>()
                .Property(e => e.sn_from)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase_Other>()
                .Property(e => e.sn_to)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase_Other>()
                .Property(e => e.batch)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase_Other>()
                .Property(e => e.is_approved)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Purchase_Other>()
                .Property(e => e.session_id)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Return_Product>()
                .Property(e => e.sn_from)
                .IsUnicode(false);

            modelBuilder.Entity<Temp_Return_Product>()
                .Property(e => e.sn_to)
                .IsUnicode(false);

            modelBuilder.Entity<user_function>()
                .Property(e => e.function_name)
                .IsUnicode(false);

            modelBuilder.Entity<user_function>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<user_function>()
                .Property(e => e.link_file)
                .IsUnicode(false);

            modelBuilder.Entity<user_function>()
                .Property(e => e.main_menu)
                .IsUnicode(false);

            modelBuilder.Entity<user_function>()
                .Property(e => e.menu_group)
                .IsUnicode(false);

            modelBuilder.Entity<user_function>()
                .Property(e => e.Icon)
                .IsUnicode(false);

            modelBuilder.Entity<user_function>()
                .Property(e => e.ControllerName)
                .IsUnicode(false);

            modelBuilder.Entity<user_function>()
                .HasMany(e => e.user_function1)
                .WithOptional(e => e.user_function2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Vendor_Bid_Price>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.bill_sales)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.bill_purchase)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.bill_cash_sales)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.voucher_number)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.journal_voucher)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.sales_voucher)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.payment_voucher)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.receipt_voucher)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.purchase_voucher)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.contra_voucher)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.TRAN_VOUCHER)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.TRADING_VOUCHER)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.EXCHANGE_VOUCHER)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.TRANSIT_VOUCHER)
                .IsUnicode(false);

            modelBuilder.Entity<BillSetting>()
                .Property(e => e.depreciation_voucher)
                .IsUnicode(false);

            modelBuilder.Entity<Fiscal_Month>()
                .Property(e => e.nplYear)
                .IsUnicode(false);

            modelBuilder.Entity<Fiscal_Month>()
                .Property(e => e.DefaultYr)
                .IsUnicode(false);

            modelBuilder.Entity<FiscalYear>()
                .Property(e => e.FISCAL_YEAR_ENGLISH)
                .IsUnicode(false);

            modelBuilder.Entity<FiscalYear>()
                .Property(e => e.FISCAL_YEAR_NEPALI)
                .IsUnicode(false);

            modelBuilder.Entity<FiscalYear>()
                .Property(e => e.NP_YEAR_START_DATE)
                .IsUnicode(false);

            modelBuilder.Entity<FiscalYear>()
                .Property(e => e.NP_YEAR_END_DATE)
                .IsUnicode(false);
        }
    }
}
