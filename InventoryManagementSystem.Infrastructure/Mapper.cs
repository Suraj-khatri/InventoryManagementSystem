using InventoryManagementSystem.Data.Models;
using InventoryManagementSystem.Domain;
using InventoryManagementSystem.Domain.FixedAssetsViewModel;
using InventoryManagementSystem.Domain.RoleSetupVM;
using InventoryManagementSystem.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Web;

namespace InventoryManagementSystem.Infrastructure
{
    public class Mapper
    {
        #region RoleSetup

        #region UserRole
        public static user_role Convert(UserRoleVM data)
        {
            if (data != null)
            {
                user_role record = new user_role
                {
                    role_id = data.role_id,
                    row_id = data.row_id,
                    user_id = data.user_id,
                    Remarks = data.Remarks,
                    Department_id= data.Department_id
                    
                };
                return record;
            }
            return null;
        }
        public static UserRoleVM Convert(user_role data)
        {
            if (data != null)
            {
                UserRoleVM record = new UserRoleVM
                {
                    role_id = data.role_id,
                    row_id = data.row_id,
                    user_id = data.user_id,
                    Remarks = data.Remarks,
                    AssignedRole = CodeService.GetStaticDataDetailDetailTitle(data.role_id),
                    Admins = Convert(data.Admins),
                    Department_id= data.Department_id?? 0,
                    DepartmentName = data.Department_id.HasValue ? CodeService.GetDepartmentName((int)data.Department_id.Value) : "No Department Assigned"
                };
                return record;
            }
            return null;
        }
        public static List<UserRoleVM> Convert(List<user_role> data)
        {
            List<UserRoleVM> records = new List<UserRoleVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region UserFunction
        public static user_function Convert(UserFunctionVM data)
        {
            if (data != null)
            {
                user_function record = new user_function
                {
                    sno = data.sno,
                    Description = data.Description,
                    dis_order = data.dis_order,
                    function_name = data.function_name,
                    link_file = data.link_file,
                    main_menu = data.main_menu,
                    menu_group = data.menu_group,
                    IsActive = data.IsActive,
                    ControllerName = data.ControllerName,
                    Icon = data.Icon,
                    ParentId = data.ParentId,
                };
                return record;
            }
            return null;
        }
        public static UserFunctionVM Convert(user_function data)
        {
            if (data != null)
            {
                UserFunctionVM record = new UserFunctionVM
                {
                    sno = data.sno,
                    Description = data.Description,
                    dis_order = data.dis_order,
                    function_name = data.function_name,
                    link_file = data.link_file,
                    main_menu = data.main_menu,
                    menu_group = data.menu_group,
                    IsActive = data.IsActive,
                    ControllerName = data.ControllerName,
                    Icon = data.Icon,
                    ParentId = data.ParentId,
                    Parent = Convert(data.user_function2)
                };
                return record;
            }
            return null;
        }
        public static List<UserFunctionVM> Convert(List<user_function> data)
        {
            List<UserFunctionVM> records = new List<UserFunctionVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region RolesDetails
        public static roles_detail Convert(RoleDetailsVM data)
        {
            if (data != null)
            {
                roles_detail record = new roles_detail
                {
                    role_id = data.role_id,
                    function_id = data.function_id,
                    rowid = data.rowid,
                    IsActive = data.IsActive
                };
                return record;
            }
            return null;
        }
        public static RoleDetailsVM Convert(roles_detail data)
        {
            if (data != null)
            {
                RoleDetailsVM record = new RoleDetailsVM
                {
                    role_id = data.role_id,
                    function_id = data.function_id,
                    rowid = data.rowid,
                    IsActive = data.IsActive,
                    //UserFunction=Convert(data.user_function)
                };
                return record;
            }
            return null;
        }
        public static List<RoleDetailsVM> Convert(List<roles_detail> data)
        {
            List<RoleDetailsVM> records = new List<RoleDetailsVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region StaticDataDetail
        public static StaticDataDetail Convert(StaticDataDetailVM data)
        {
            if (data != null)
            {
                StaticDataDetail record = new StaticDataDetail
                {
                    ROWID = data.ROWID,
                    add_PF = data.add_PF,
                    applyOT = data.applyOT,
                    CEAvalue = data.CEAvalue,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    DETAIL_DESC = data.DETAIL_DESC,
                    DETAIL_TITLE = data.DETAIL_TITLE,
                    isdepartment = data.isdepartment,
                    isdistrict = data.isdistrict,
                    iszone = data.iszone,
                    IS_DELETE = data.IS_DELETE,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    TYPE_ID = data.TYPE_ID,
                    value = data.value,
                    IsActive = data.IsActive
                };
                return record;
            }
            return null;
        }
        public static StaticDataDetailVM Convert(StaticDataDetail data)
        {
            if (data != null)
            {
                StaticDataDetailVM record = new StaticDataDetailVM
                {
                    ROWID = data.ROWID,
                    add_PF = data.add_PF,
                    applyOT = data.applyOT,
                    CEAvalue = data.CEAvalue,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    DETAIL_DESC = data.DETAIL_DESC,
                    DETAIL_TITLE = data.DETAIL_TITLE,
                    isdepartment = data.isdepartment,
                    isdistrict = data.isdistrict,
                    iszone = data.iszone,
                    IS_DELETE = data.IS_DELETE,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    TYPE_ID = data.TYPE_ID,
                    value = data.value,
                    IsActive = data.IsActive,
                    StaticDataType = Convert(data.StaticDataType)
                };
                return record;
            }
            return null;
        }
        public static List<StaticDataDetailVM> Convert(List<StaticDataDetail> data)
        {
            List<StaticDataDetailVM> records = new List<StaticDataDetailVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region StaticDataType
        public static StaticDataType Convert(StaticDataTypeVM data)
        {
            if (data != null)
            {
                StaticDataType record = new StaticDataType
                {
                    ROWID = data.ROWID,
                    TYPE_DESC = data.TYPE_DESC,
                    TYPE_TITLE = data.TYPE_TITLE,
                    IsActive = data.IsActive
                };
                return record;
            }
            return null;
        }
        public static StaticDataTypeVM Convert(StaticDataType data)
        {
            if (data != null)
            {
                StaticDataTypeVM record = new StaticDataTypeVM
                {
                    ROWID = data.ROWID,
                    TYPE_DESC = data.TYPE_DESC,
                    TYPE_TITLE = data.TYPE_TITLE,
                    IsActive = data.IsActive
                };
                return record;
            }
            return null;
        }
        public static List<StaticDataTypeVM> Convert(List<StaticDataType> data)
        {
            List<StaticDataTypeVM> records = new List<StaticDataTypeVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #endregion

        #region IMS Notification

        #region NotificationType
        public static NotificationType Convert(NotificationTypeVM data)
        {
            if (data != null)
            {
                NotificationType record = new NotificationType
                {
                    Id = data.Id,
                    Icon = data.Icon,
                    Name = data.Name
                };
                return record;
            }
            return null;
        }
        public static NotificationTypeVM Convert(NotificationType data)
        {
            if (data != null)
            {
                NotificationTypeVM record = new NotificationTypeVM
                {
                    Id = data.Id,
                    Icon = data.Icon,
                    Name = data.Name
                };
                return record;
            }
            return null;
        }
        public static List<NotificationTypeVM> Convert(List<NotificationType> data)
        {
            List<NotificationTypeVM> records = new List<NotificationTypeVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region Notification
        public static Notification Convert(NotificationVM data)
        {
            if (data != null)
            {
                Notification record = new Notification
                {
                    Id = data.Id,
                    Icon = data.Icon,
                    ActionType = data.ActionType,
                    NotificationTypeId = data.NotificationTypeId,
                    CreatedDate = data.CreatedDate,
                    SpecialId = data.SpecialId,
                    Subject = data.Subject,
                    Url = data.Url
                };
                return record;
            }
            return null;
        }
        public static NotificationVM Convert(Notification data)
        {
            if (data != null)
            {
                NotificationVM record = new NotificationVM
                {
                    Id = data.Id,
                    Icon = data.Icon,
                    ActionType = data.ActionType,
                    NotificationTypeId = data.NotificationTypeId,
                    CreatedDate = data.CreatedDate,
                    SpecialId = data.SpecialId,
                    Subject = data.Subject,
                    Url = data.Url,
                    NotificationTypes = Convert(data.NotificationType)
                };
                return record;
            }
            return null;
        }
        public static List<NotificationVM> Convert(List<Notification> data)
        {
            List<NotificationVM> records = new List<NotificationVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region IMSNotifications
        public static IN_Notifications Convert(InNotificationsVM data)
        {
            if (data != null)
            {
                IN_Notifications record = new IN_Notifications
                {
                    Id = data.Id,
                    Icon = data.Icon,
                    CreatedDate = data.CreatedDate,
                    SpecialId = data.SpecialId,
                    Subject = data.Subject,
                    Forwardedby = data.Forwardedby,
                    Forwardedto = data.Forwardedto,
                    ReadDate = data.ReadDate,
                    Status = data.Status,
                    URL = data.URL
                };
                return record;
            }
            return null;
        }
        public static InNotificationsVM Convert(IN_Notifications data)
        {
            if (data != null)
            {
                InNotificationsVM record = new InNotificationsVM
                {
                    Id = data.Id,
                    Icon = data.Icon,
                    CreatedDate = data.CreatedDate,
                    SpecialId = data.SpecialId,
                    Subject = data.Subject,
                    Forwardedby = data.Forwardedby,
                    Forwardedto = data.Forwardedto,
                    ReadDate = data.ReadDate,
                    Status = data.Status,
                    URL = data.URL
                };
                return record;
            }
            return null;
        }
        public static List<InNotificationsVM> Convert(List<IN_Notifications> data)
        {
            List<InNotificationsVM> records = new List<InNotificationsVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region NotificationUser
        public static NotificationUser Convert(NotificationUserVM data)
        {
            if (data != null)
            {
                NotificationUser record = new NotificationUser
                {
                    Id = data.Id,
                    EmployeeId = data.EmployeeId,
                    NotificationId = data.NotificationId,
                    ReadDate = data.ReadDate
                };
                return record;
            }
            return null;
        }
        public static NotificationUserVM Convert(NotificationUser data)
        {
            if (data != null)
            {
                NotificationUserVM record = new NotificationUserVM
                {
                    Id = data.Id,
                    EmployeeId = data.EmployeeId,
                    NotificationId = data.NotificationId,
                    ReadDate = data.ReadDate,
                    Employees = Convert(data.Employee),
                    Notifications = Convert(data.Notification)
                };
                return record;
            }
            return null;
        }
        public static List<NotificationUserVM> Convert(List<NotificationUser> data)
        {
            List<NotificationUserVM> records = new List<NotificationUserVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #endregion

        #region Inventory Management System

        #region Employeemanagement
        #region Employee
        public static Employee Convert(EmployeeVM data)
        {
            if (data != null)
            {
                Employee record = new Employee
                {
                    EMPLOYEE_ID = data.EMPLOYEE_ID,
                    DEPARTMENT_ID = data.DEPARTMENT_ID,
                    BRANCH_ID = data.BRANCH_ID,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    APPOINTMENT_DATE = data.APPOINTMENT_DATE,
                    ATT_CARD_ID = data.ATT_CARD_ID,
                    AVAAILED_VEHICLE_FACILITY = data.AVAAILED_VEHICLE_FACILITY,
                    AVAILED_HOUSE_RENT_FACILITY = data.AVAILED_HOUSE_RENT_FACILITY,
                    birthdayFlag = data.birthdayFlag,
                    BIRTH_DATE = data.BIRTH_DATE,
                    BLOOD_GROUP = data.BLOOD_GROUP,
                    branch_state = data.branch_state,
                    CARD_NUMBER = data.CARD_NUMBER,
                    C_END_DATE = data.C_END_DATE,
                    C_START_DATE = data.C_START_DATE,
                    DISABLED_ID = data.DISABLED_ID,
                    DRIVARY_LICENCE_NUMBER = data.DRIVARY_LICENCE_NUMBER,
                    EDUCATION_DETAILS = data.EDUCATION_DETAILS,
                    EMP_CODE = data.EMP_CODE,
                    EMP_STATUS = data.EMP_STATUS,
                    EMP_TYPE = data.EMP_TYPE,
                    EM_ADDRESS = data.EM_ADDRESS,
                    EM_CONTACTNO1 = data.EM_CONTACTNO1,
                    EM_CONTACTNO2 = data.EM_CONTACTNO2,
                    EM_CONTACTNO3 = data.EM_CONTACTNO3,
                    EM_EMAIL = data.EM_EMAIL,
                    EM_NAME = data.EM_NAME,
                    EM_RELATIONSHIP = data.EM_RELATIONSHIP,
                    EXTENSION_NUMBER = data.EXTENSION_NUMBER,
                    FIRST_NAME = data.FIRST_NAME,
                    FUNCTIONAL_TITLE = data.FUNCTIONAL_TITLE,
                    GENDER = data.GENDER,
                    GRADE = data.GRADE,
                    GRATUITY_EFFECTIVE_DATE = data.GRATUITY_EFFECTIVE_DATE,
                    HOME_PHONE = data.HOME_PHONE,
                    Individual_Profile_update = data.Individual_Profile_update,
                    IS_DISABLED = data.IS_DISABLED,
                    IS_PENSION_HOLDER = data.IS_PENSION_HOLDER,
                    JOINED_DATE = data.JOINED_DATE,
                    LASTPROMOTED = data.LASTPROMOTED,
                    LAST_NAME = data.LAST_NAME,
                    MAP_CODE = data.MAP_CODE,
                    MARITAL_STATUS = data.MARITAL_STATUS,
                    MERITAL_STATUS = data.MERITAL_STATUS,
                    MIDDLE_NAME = data.MIDDLE_NAME,
                    NATIONALITY = data.NATIONALITY,
                    OFFICE_FAX = data.OFFICE_FAX,
                    OFFICE_MOBILE = data.OFFICE_MOBILE,
                    OFFICE_PHONE = data.OFFICE_PHONE,
                    OFFICIAL_EMAIL = data.OFFICIAL_EMAIL,
                    OUTSOURCE_COMPANY = data.OUTSOURCE_COMPANY,
                    PAN_NUMBER = data.PAN_NUMBER,
                    PASSPORT_NUMBER = data.PASSPORT_NUMBER,
                    PENSION_AMOUNT = data.PENSION_AMOUNT,
                    PERMANENT_DATE = data.PERMANENT_DATE,
                    PERSONAL_EMAIL = data.PERSONAL_EMAIL,
                    PERSONAL_FAX = data.PERSONAL_FAX,
                    PERSONAL_MOBILE = data.PERSONAL_MOBILE,
                    PER_COUNTRY = data.PER_COUNTRY,
                    PER_DISTRICT = data.PER_DISTRICT,
                    PER_HOUSE_NO = data.PER_HOUSE_NO,
                    PER_MUNICIPALITY_VDC = data.PER_MUNICIPALITY_VDC,
                    per_state = data.per_state,
                    PER_STREET_NAME = data.PER_STREET_NAME,
                    PER_WARD_NO = data.PER_WARD_NO,
                    PER_PROVINCE = data.PER_PROVINCE,
                    POSITION_ID = data.POSITION_ID,
                    Rl_group = data.Rl_group,
                    Salary_Title = data.Salary_Title,
                    SALUTATION = data.SALUTATION,
                    SYSTEM_ID = data.SYSTEM_ID,
                    TEMP_COUNTRY = data.TEMP_COUNTRY,
                    TEMP_DISTRICT = data.TEMP_DISTRICT,
                    TEMP_HOUSE_NO = data.TEMP_HOUSE_NO,
                    TEMP_MUNICIPALITY_VDC = data.TEMP_MUNICIPALITY_VDC,
                    temp_STATE = data.temp_STATE,
                    TEMP_STREET_NAME = data.TEMP_STREET_NAME,
                    TEMP_WARD_NO = data.TEMP_WARD_NO,
                    TEMP_PROVINCE = data.TEMP_PROVINCE,
                    WORKING_MONTH = data.WORKING_MONTH
                };
                return record;
            }
            return null;
        }
        public static EmployeeVM Convert(Employee data)
        {
            if (data != null)
            {
                EmployeeVM record = new EmployeeVM
                {
                    EMPLOYEE_ID = data.EMPLOYEE_ID,
                    DEPARTMENT_ID = data.DEPARTMENT_ID,
                    BRANCH_ID = data.BRANCH_ID,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    APPOINTMENT_DATE = data.APPOINTMENT_DATE,
                    ATT_CARD_ID = data.ATT_CARD_ID,
                    AVAAILED_VEHICLE_FACILITY = data.AVAAILED_VEHICLE_FACILITY,
                    AVAILED_HOUSE_RENT_FACILITY = data.AVAILED_HOUSE_RENT_FACILITY,
                    birthdayFlag = data.birthdayFlag,
                    BIRTH_DATE = data.BIRTH_DATE,
                    BLOOD_GROUP = data.BLOOD_GROUP,
                    branch_state = data.branch_state,
                    CARD_NUMBER = data.CARD_NUMBER,
                    C_END_DATE = data.C_END_DATE,
                    C_START_DATE = data.C_START_DATE,
                    DISABLED_ID = data.DISABLED_ID,
                    DRIVARY_LICENCE_NUMBER = data.DRIVARY_LICENCE_NUMBER,
                    EDUCATION_DETAILS = data.EDUCATION_DETAILS,
                    EMP_CODE = data.EMP_CODE,
                    EMP_STATUS = data.EMP_STATUS,
                    EMP_TYPE = data.EMP_TYPE,
                    EM_ADDRESS = data.EM_ADDRESS,
                    EM_CONTACTNO1 = data.EM_CONTACTNO1,
                    EM_CONTACTNO2 = data.EM_CONTACTNO2,
                    EM_CONTACTNO3 = data.EM_CONTACTNO3,
                    EM_EMAIL = data.EM_EMAIL,
                    EM_NAME = data.EM_NAME,
                    EM_RELATIONSHIP = data.EM_RELATIONSHIP,
                    EXTENSION_NUMBER = data.EXTENSION_NUMBER,
                    FIRST_NAME = data.FIRST_NAME,
                    FUNCTIONAL_TITLE = data.FUNCTIONAL_TITLE,
                    GENDER = data.GENDER,
                    GRADE = data.GRADE,
                    GRATUITY_EFFECTIVE_DATE = data.GRATUITY_EFFECTIVE_DATE,
                    HOME_PHONE = data.HOME_PHONE,
                    Individual_Profile_update = data.Individual_Profile_update,
                    IS_DISABLED = data.IS_DISABLED,
                    IS_PENSION_HOLDER = data.IS_PENSION_HOLDER,
                    JOINED_DATE = data.JOINED_DATE,
                    LASTPROMOTED = data.LASTPROMOTED,
                    LAST_NAME = data.LAST_NAME,
                    MAP_CODE = data.MAP_CODE,
                    MARITAL_STATUS = data.MARITAL_STATUS,
                    MERITAL_STATUS = data.MERITAL_STATUS,
                    MIDDLE_NAME = data.MIDDLE_NAME,
                    NATIONALITY = data.NATIONALITY,
                    OFFICE_FAX = data.OFFICE_FAX,
                    OFFICE_MOBILE = data.OFFICE_MOBILE,
                    OFFICE_PHONE = data.OFFICE_PHONE,
                    OFFICIAL_EMAIL = data.OFFICIAL_EMAIL,
                    OUTSOURCE_COMPANY = data.OUTSOURCE_COMPANY,
                    PAN_NUMBER = data.PAN_NUMBER,
                    PASSPORT_NUMBER = data.PASSPORT_NUMBER,
                    PENSION_AMOUNT = data.PENSION_AMOUNT,
                    PERMANENT_DATE = data.PERMANENT_DATE,
                    PERSONAL_EMAIL = data.PERSONAL_EMAIL,
                    PERSONAL_FAX = data.PERSONAL_FAX,
                    PERSONAL_MOBILE = data.PERSONAL_MOBILE,
                    PER_COUNTRY = data.PER_COUNTRY,
                    PER_DISTRICT = data.PER_DISTRICT,
                    PER_HOUSE_NO = data.PER_HOUSE_NO,
                    PER_MUNICIPALITY_VDC = data.PER_MUNICIPALITY_VDC,
                    per_state = data.per_state,
                    PER_STREET_NAME = data.PER_STREET_NAME,
                    PER_WARD_NO = data.PER_WARD_NO,
                    PER_PROVINCE = data.PER_PROVINCE,
                    POSITION_ID = data.POSITION_ID,
                    Rl_group = data.Rl_group,
                    Salary_Title = data.Salary_Title,
                    SALUTATION = data.SALUTATION,
                    SYSTEM_ID = data.SYSTEM_ID,
                    TEMP_COUNTRY = data.TEMP_COUNTRY,
                    TEMP_DISTRICT = data.TEMP_DISTRICT,
                    TEMP_HOUSE_NO = data.TEMP_HOUSE_NO,
                    TEMP_MUNICIPALITY_VDC = data.TEMP_MUNICIPALITY_VDC,
                    temp_STATE = data.temp_STATE,
                    TEMP_STREET_NAME = data.TEMP_STREET_NAME,
                    TEMP_WARD_NO = data.TEMP_WARD_NO,
                    TEMP_PROVINCE = data.TEMP_PROVINCE,
                    WORKING_MONTH = data.WORKING_MONTH,
                    BranchName = CodeService.GetBranchName(data.BRANCH_ID),
                    DepartmentName = CodeService.GetDepartmentName(data.DEPARTMENT_ID),
                    FullName = data.FIRST_NAME + " " + data.MIDDLE_NAME + " " + data.LAST_NAME,
                    Designation = CodeService.GetDesignation(data.POSITION_ID)
                };
                return record;
            }
            return null;
        }
        public static List<EmployeeVM> Convert(List<Employee> data)
        {
            List<EmployeeVM> records = new List<EmployeeVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region Admins
        public static Admins Convert(AdminVM data)
        {
            if (data != null)
            {
                Admins record = new Admins
                {
                    AdminID = data.AdminID,
                    forceChangePwd = data.forceChangePwd,
                    Address = data.Address,
                    agent_id = data.agent_id,
                    approved_by = data.approved_by,
                    approved_date = data.approved_date,
                    branch_level_access = data.branch_level_access,
                    Cell_phone = data.Cell_phone,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    Email = data.Email,
                    LastLogin = data.LastLogin,
                    lastPwdChangedOn = data.lastPwdChangedOn,
                    LOGINTIMEFROM = data.LOGINTIMEFROM,
                    LOGINTIMETO = data.LOGINTIMETO,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    Name = data.Name,
                    new_user = data.new_user,
                    Phone = data.Phone,
                    Post = data.Post,
                    pwdChangeDays = data.pwdChangeDays,
                    PwdChangeWaringDays = data.PwdChangeWaringDays,
                    REPORTACCESS = data.REPORTACCESS,
                    session = data.session,
                    status = data.status,
                    UserName = data.UserName,
                    UserPassword = data.UserPassword,
                    user_type = data.user_type,
                    IsTemporary = data.IsTemporary
                };
                return record;
            }
            return null;
        }
        public static AdminVM Convert(Admins data)
        {
            if (data != null)
            {
                AdminVM record = new AdminVM
                {
                    AdminID = data.AdminID,
                    forceChangePwd = data.forceChangePwd,
                    Address = data.Address,
                    agent_id = data.agent_id,
                    approved_by = data.approved_by,
                    approved_date = data.approved_date,
                    branch_level_access = data.branch_level_access,
                    Cell_phone = data.Cell_phone,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    Email = data.Email,
                    LastLogin = data.LastLogin,
                    lastPwdChangedOn = data.lastPwdChangedOn,
                    LOGINTIMEFROM = data.LOGINTIMEFROM,
                    LOGINTIMETO = data.LOGINTIMETO,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    Name = data.Name,
                    new_user = data.new_user,
                    Phone = data.Phone,
                    Post = data.Post,
                    pwdChangeDays = data.pwdChangeDays,
                    PwdChangeWaringDays = data.PwdChangeWaringDays,
                    REPORTACCESS = data.REPORTACCESS,
                    session = data.session,
                    status = data.status,
                    UserName = data.UserName,
                    UserPassword = data.UserPassword,
                    user_type = data.user_type,
                    IsTemporary = data.IsTemporary,
                    Employees = Convert(data.Employee)
                };
                return record;
            }
            return null;
        }
        public static List<AdminVM> Convert(List<Admins> data)
        {
            List<AdminVM> records = new List<AdminVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region EmployeeLog
        public static emp_log Convert(EmployeeLogVM data)
        {
            if (data != null)
            {
                emp_log record = new emp_log
                {
                    id = data.id,
                    emp_id = data.emp_id,
                    branch_id = data.branch_id,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    dept_id = data.dept_id,
                    effective_date = data.effective_date,
                    emp_type = data.emp_type,
                    flag = data.flag,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    position_id = data.position_id
                };
                return record;
            }
            return null;
        }
        public static EmployeeLogVM Convert(emp_log data)
        {
            if (data != null)
            {
                EmployeeLogVM record = new EmployeeLogVM
                {
                    id = data.id,
                    emp_id = data.emp_id,
                    branch_id = data.branch_id,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    dept_id = data.dept_id,
                    effective_date = data.effective_date,
                    emp_type = data.emp_type,
                    flag = data.flag,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    position_id = data.position_id,
                    Employees = Convert(data.Employee)
                };
                return record;
            }
            return null;
        }
        public static List<EmployeeLogVM> Convert(List<emp_log> data)
        {
            List<EmployeeLogVM> records = new List<EmployeeLogVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region EmployeeContract
        public static Employee_Contract Convert(EmployeeContractVM data)
        {
            if (data != null)
            {
                Employee_Contract record = new Employee_Contract
                {
                    RowID = data.RowID,
                    dept_id = data.dept_id,
                    Cont_DateFrm = data.Cont_DateFrm,
                    EMPLOYEE_ID = data.EMPLOYEE_ID,
                    branch_id = data.branch_id,
                    Cont_DateTo = data.Cont_DateTo,
                    Created_By = data.Created_By,
                    Created_Date = data.Created_Date,
                    emp_type = data.emp_type,
                    flag = data.flag,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    position_id = data.position_id
                };
                return record;
            }
            return null;
        }
        public static EmployeeContractVM Convert(Employee_Contract data)
        {
            if (data != null)
            {
                EmployeeContractVM record = new EmployeeContractVM
                {
                    RowID = data.RowID,
                    dept_id = data.dept_id,
                    Cont_DateFrm = data.Cont_DateFrm,
                    EMPLOYEE_ID = data.EMPLOYEE_ID,
                    branch_id = data.branch_id,
                    Cont_DateTo = data.Cont_DateTo,
                    Created_By = data.Created_By,
                    Created_Date = data.Created_Date,
                    emp_type = data.emp_type,
                    flag = data.flag,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    position_id = data.position_id,
                    Employees = Convert(data.Employee)
                };
                return record;
            }
            return null;
        }
        public static List<EmployeeContractVM> Convert(List<Employee_Contract> data)
        {
            List<EmployeeContractVM> records = new List<EmployeeContractVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region SuperVisorAssignment
        public static SuperVisroAssignment Convert(SuperVisorAssignmentVM data)
        {
            if (data != null)
            {
                SuperVisroAssignment record = new SuperVisroAssignment
                {
                    SV_ASSIGN_ID = data.SV_ASSIGN_ID,
                    BRANCH = data.BRANCH,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    DEPT = data.DEPT,
                    EMP = data.EMP,
                    EMP_TYPE = data.EMP_TYPE,
                    MODIFY_BY = data.MODIFY_BY,
                    MODIFY_DATE = data.MODIFY_DATE,
                    OPERATION_TYPE = data.OPERATION_TYPE,
                    POSITION = data.POSITION,
                    record_status = data.record_status,
                    SUPERVISOR = data.SUPERVISOR,
                    SUPERVISOR_TYPE = data.SUPERVISOR_TYPE
                };
                return record;
            }
            return null;
        }
        public static SuperVisorAssignmentVM Convert(SuperVisroAssignment data)
        {
            if (data != null)
            {
                SuperVisorAssignmentVM record = new SuperVisorAssignmentVM
                {
                    SV_ASSIGN_ID = data.SV_ASSIGN_ID,
                    BRANCH = data.BRANCH,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    DEPT = data.DEPT,
                    EMP = data.EMP,
                    EMP_TYPE = data.EMP_TYPE,
                    MODIFY_BY = data.MODIFY_BY,
                    MODIFY_DATE = data.MODIFY_DATE,
                    OPERATION_TYPE = data.OPERATION_TYPE,
                    POSITION = data.POSITION,
                    record_status = data.record_status,
                    SUPERVISOR = data.SUPERVISOR,
                    SUPERVISOR_TYPE = data.SUPERVISOR_TYPE,
                    BranchName = CodeService.GetBranchName((int)data.BRANCH),
                    SuperVisorName = CodeService.GetEmployeeFullName((int)data.SUPERVISOR)
                };
                return record;
            }
            return null;
        }
        public static List<SuperVisorAssignmentVM> Convert(List<SuperVisroAssignment> data)
        {
            List<SuperVisorAssignmentVM> records = new List<SuperVisorAssignmentVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #endregion

        #region CompanyManagement
        #region Company
        public static COMPANY Convert(CompanyVM data)
        {
            if (data != null)
            {
                COMPANY record = new COMPANY
                {
                    COMP_ID = data.COMP_ID,
                    COMP_ADDRESS = data.COMP_ADDRESS,
                    COMP_ADDRESS2 = data.COMP_ADDRESS2,
                    COMP_CONTACT_PERSON = data.COMP_CONTACT_PERSON,
                    COMP_EMAIL = data.COMP_EMAIL,
                    COMP_FAX_NO = data.COMP_FAX_NO,
                    COMP_MAP_CODE = data.COMP_MAP_CODE,
                    COMP_NAME = data.COMP_NAME,
                    COMP_PHONE_NO = data.COMP_PHONE_NO,
                    COMP_SHORT_NAME = data.COMP_SHORT_NAME,
                    COMP_STATUS = data.COMP_STATUS,
                    COMP_URL = data.COMP_URL,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    EPS = data.EPS,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    POST_BOX = data.POST_BOX,
                    BranchId = data.BranchId
                };
                return record;
            }
            return null;
        }
        public static CompanyVM Convert(COMPANY data)
        {
            if (data != null)
            {
                CompanyVM record = new CompanyVM
                {
                    COMP_ID = data.COMP_ID,
                    COMP_ADDRESS = data.COMP_ADDRESS,
                    COMP_ADDRESS2 = data.COMP_ADDRESS2,
                    COMP_CONTACT_PERSON = data.COMP_CONTACT_PERSON,
                    COMP_EMAIL = data.COMP_EMAIL,
                    COMP_FAX_NO = data.COMP_FAX_NO,
                    COMP_MAP_CODE = data.COMP_MAP_CODE,
                    COMP_NAME = data.COMP_NAME,
                    COMP_PHONE_NO = data.COMP_PHONE_NO,
                    COMP_SHORT_NAME = data.COMP_SHORT_NAME,
                    COMP_STATUS = data.COMP_STATUS,
                    COMP_URL = data.COMP_URL,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    EPS = data.EPS,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    POST_BOX = data.POST_BOX,
                    BranchId = data.BranchId,
                    Branches = Convert(data.Branches)
                };
                return record;
            }
            return null;
        }
        public static List<CompanyVM> Convert(List<COMPANY> data)
        {
            List<CompanyVM> records = new List<CompanyVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region Branch
        public static Branches Convert(BranchVM data)
        {
            if (data != null)
            {
                Branches record = new Branches
                {
                    BRANCH_ID = data.BRANCH_ID,
                    Batch_Code = data.Batch_Code,
                    BRANCH_ADDRESS = data.BRANCH_ADDRESS,
                    BRANCH_CITY = data.BRANCH_CITY,
                    BRANCH_COUNTRY = data.BRANCH_COUNTRY,
                    BRANCH_DISTRICT = data.BRANCH_DISTRICT,
                    BRANCH_EMAIL = data.BRANCH_EMAIL,
                    BRANCH_FAX = data.BRANCH_FAX,
                    BRANCH_GROUP = data.BRANCH_GROUP,
                    BRANCH_MOBILE = data.BRANCH_MOBILE,
                    BRANCH_NAME = data.BRANCH_NAME,
                    BRANCH_PHONE = data.BRANCH_PHONE,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    EPS = data.EPS,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    BRANCH_POST_BOX = data.BRANCH_POST_BOX,
                    BRANCH_SHORT_NAME = data.BRANCH_SHORT_NAME,
                    BRANCH_Municipality = data.BRANCH_Municipality,
                    COMPANY_ID = data.COMPANY_ID,
                    CONTACT_PERSON = data.CONTACT_PERSON,
                    expensesAc = data.expensesAc,
                    IBT_Account = data.IBT_Account,
                    isDirectExp = data.isDirectExp,
                    stockAc = data.stockAc,
                    transitAc = data.transitAc,
                    Is_Active = data.Is_Active,
                    ExtNo = data.ExtNo
                };
                return record;
            }
            return null;
        }
        public static BranchVM Convert(Branches data)
        {
            if (data != null)
            {
                BranchVM record = new BranchVM
                {
                    BRANCH_ID = data.BRANCH_ID,
                    Batch_Code = data.Batch_Code,
                    BRANCH_ADDRESS = data.BRANCH_ADDRESS,
                    BRANCH_CITY = data.BRANCH_CITY,
                    BRANCH_COUNTRY = data.BRANCH_COUNTRY,
                    BRANCH_DISTRICT = data.BRANCH_DISTRICT,
                    BRANCH_EMAIL = data.BRANCH_EMAIL,
                    BRANCH_FAX = data.BRANCH_FAX,
                    BRANCH_GROUP = data.BRANCH_GROUP,
                    BRANCH_MOBILE = data.BRANCH_MOBILE,
                    BRANCH_NAME = data.BRANCH_NAME,
                    BRANCH_PHONE = data.BRANCH_PHONE,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    EPS = data.EPS,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    BRANCH_POST_BOX = data.BRANCH_POST_BOX,
                    BRANCH_SHORT_NAME = data.BRANCH_SHORT_NAME,
                    BRANCH_Municipality = data.BRANCH_Municipality,
                    COMPANY_ID = data.COMPANY_ID,
                    CONTACT_PERSON = data.CONTACT_PERSON,
                    expensesAc = data.expensesAc,
                    IBT_Account = data.IBT_Account,
                    isDirectExp = data.isDirectExp,
                    stockAc = data.stockAc,
                    transitAc = data.transitAc,
                    Is_Active = data.Is_Active,
                    ExtNo = data.ExtNo
                };
                return record;
            }
            return null;
        }
        public static List<BranchVM> Convert(List<Branches> data)
        {
            List<BranchVM> records = new List<BranchVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region Department
        public static departments Convert(DepartmentsVM data)
        {
            if (data != null)
            {
                departments record = new departments
                {
                    DEPARTMENT_ID = data.DEPARTMENT_ID,
                    BRANCH_ID = data.BRANCH_ID,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    DEPARTMENT_SHORT_NAME = data.DEPARTMENT_SHORT_NAME,
                    DEPARTMENT_HEAD = data.DEPARTMENT_HEAD,
                    DEPARTMENT_NAME = data.DEPARTMENT_NAME,
                    EMAIL = data.EMAIL,
                    EMAIL_DEPARTMENT_HEAD = data.EMAIL_DEPARTMENT_HEAD,
                    FAX = data.FAX,
                    MOBILE_DEPARTMENT_HEAD = data.MOBILE_DEPARTMENT_HEAD,
                    PHONE = data.PHONE,
                    PHONE_EXTENSION = data.PHONE_EXTENSION,
                    STATIC_ID = data.STATIC_ID
                };
                return record;
            }
            return null;
        }
        public static DepartmentsVM Convert(departments data)
        {
            if (data != null)
            {
                DepartmentsVM record = new DepartmentsVM
                {
                    DEPARTMENT_ID = data.DEPARTMENT_ID,
                    BRANCH_ID = data.BRANCH_ID,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    DEPARTMENT_SHORT_NAME = data.DEPARTMENT_SHORT_NAME,
                    DEPARTMENT_HEAD = data.DEPARTMENT_HEAD,
                    DEPARTMENT_NAME = data.DEPARTMENT_NAME,
                    EMAIL = data.EMAIL,
                    EMAIL_DEPARTMENT_HEAD = data.EMAIL_DEPARTMENT_HEAD,
                    FAX = data.FAX,
                    MOBILE_DEPARTMENT_HEAD = data.MOBILE_DEPARTMENT_HEAD,
                    PHONE = data.PHONE,
                    PHONE_EXTENSION = data.PHONE_EXTENSION,
                    STATIC_ID = data.STATIC_ID,
                    BranchName = CodeService.GetBranchName(data.BRANCH_ID)
                };
                return record;
            }
            return null;
        }
        public static List<DepartmentsVM> Convert(List<departments> data)
        {
            List<DepartmentsVM> records = new List<DepartmentsVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #endregion

        #region InvProduct
        #region InItem
        public static IN_ITEM Convert(In_ItemVM data)
        {
            if (data != null)
            {
                IN_ITEM record = new IN_ITEM
                {
                    id = data.id,
                    item_name = data.item_name,
                    item_desc = data.item_desc,
                    Product_Code = data.Product_Code,
                    parent_id = data.parent_id,
                    is_product = data.is_product,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    Is_Active = data.Is_Active
                };
                return record;
            }
            return null;
        }
        public static In_ItemVM Convert(IN_ITEM data)
        {
            if (data != null)
            {
                In_ItemVM record = new In_ItemVM
                {
                    id = data.id,
                    item_name = data.item_name,
                    item_desc = data.item_desc,
                    Product_Code = data.Product_Code,
                    parent_id = data.parent_id,
                    is_product = data.is_product,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    Is_Active = data.Is_Active,
                    productid = CodeService.GetProductIdByItemId(data.id)
                };
                return record;
            }
            return null;
        }
        public static List<In_ItemVM> Convert(List<IN_ITEM> data)
        {
            List<In_ItemVM> records = new List<In_ItemVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InProduct
        public static IN_PRODUCT Convert(In_ProductVM data)
        {
            if (data != null)
            {
                IN_PRODUCT record = new IN_PRODUCT
                {
                    id = data.id,
                    porduct_code = data.porduct_code,
                    product_desc = data.product_desc,
                    is_active = data.is_active,
                    batch_condition = data.batch_condition,
                    bulk_discount = data.bulk_discount,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    conversion_rate = data.conversion_rate,
                    ext_fld1 = data.ext_fld1,
                    ext_fld2 = data.ext_fld2,
                    is_tangible = data.is_tangible,
                    is_taxable = data.is_taxable,
                    item_id = data.item_id,
                    make = data.make,
                    model = data.model,
                    package_unit = data.package_unit,
                    purchase_base_price = data.purchase_base_price,
                    purchase_tolerence_minus = data.purchase_tolerence_minus,
                    purchase_tolerence_plus = data.purchase_tolerence_plus,
                    reorder_level = data.reorder_level,
                    sales_base_price = data.sales_base_price,
                    sales_tolerence_minus = data.sales_tolerence_minus,
                    sales_tolerence_plus = data.sales_tolerence_plus,
                    serial_no = data.serial_no,
                    single_unit = data.single_unit,
                    unit_discount = data.unit_discount,
                };
                return record;
            }
            return null;
        }
        public static In_ProductVM Convert(IN_PRODUCT data)
        {
            if (data != null)
            {
                In_ProductVM record = new In_ProductVM
                {
                    id = data.id,
                    porduct_code = data.porduct_code,
                    product_desc = data.product_desc,
                    is_active = data.is_active,
                    batch_condition = data.batch_condition,
                    bulk_discount = data.bulk_discount,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    conversion_rate = data.conversion_rate,
                    ext_fld1 = data.ext_fld1,
                    ext_fld2 = data.ext_fld2,
                    is_tangible = data.is_tangible,
                    is_taxable = data.is_taxable,
                    item_id = data.item_id,
                    make = data.make,
                    model = data.model,
                    package_unit = data.package_unit,
                    purchase_base_price = data.purchase_base_price,
                    purchase_tolerence_minus = data.purchase_tolerence_minus,
                    purchase_tolerence_plus = data.purchase_tolerence_plus,
                    reorder_level = data.reorder_level,
                    sales_base_price = data.sales_base_price,
                    sales_tolerence_minus = data.sales_tolerence_minus,
                    sales_tolerence_plus = data.sales_tolerence_plus,
                    serial_no = data.serial_no,
                    single_unit = data.single_unit,
                    unit_discount = data.unit_discount,
                    INITEMVM = Convert(data.IN_ITEM)
                };
                return record;
            }
            return null;
        }
        public static List<In_ProductVM> Convert(List<IN_PRODUCT> data)
        {
            List<In_ProductVM> records = new List<In_ProductVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region Customer
        public static Customer Convert(CustomerVM data)
        {
            if (data != null)
            {
                Customer record = new Customer
                {
                    ID = data.ID,
                    BusinessDetails = data.BusinessDetails,
                    ContactPersonEmail1 = data.ContactPersonEmail1,
                    ContactPersonEmail2 = data.ContactPersonEmail2,
                    ContactPersonEmail3 = data.ContactPersonEmail3,
                    ContactPersonFirst = data.ContactPersonFirst,
                    ContactPersonMobile1 = data.ContactPersonMobile1,
                    ContactPersonMobile2 = data.ContactPersonMobile2,
                    ContactPersonMobile3 = data.ContactPersonMobile3,
                    ContactPersonSec = data.ContactPersonSec,
                    ContactPersonThird = data.ContactPersonThird,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    CustomeFax = data.CustomeFax,
                    CustomerAddress = data.CustomerAddress,
                    CustomerCode = data.CustomerCode,
                    CustomerEmail = data.CustomerEmail,
                    CUSTOMERMOBILENO = data.CUSTOMERMOBILENO,
                    CustomerName = data.CustomerName,
                    CustomerPANNo = data.CustomerPANNo,
                    CustomerTelNo = data.CustomerTelNo,
                    CustomerTelNoSec = data.CustomerTelNoSec,
                    CustomerWebsite = data.CustomerWebsite,
                    FacilityDetails = data.FacilityDetails,
                    IsActive = data.IsActive,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate
                };
                return record;
            }
            return null;
        }
        public static CustomerVM Convert(Customer data)
        {
            if (data != null)
            {
                CustomerVM record = new CustomerVM
                {
                    ID = data.ID,
                    BusinessDetails = data.BusinessDetails,
                    ContactPersonEmail1 = data.ContactPersonEmail1,
                    ContactPersonEmail2 = data.ContactPersonEmail2,
                    ContactPersonEmail3 = data.ContactPersonEmail3,
                    ContactPersonFirst = data.ContactPersonFirst,
                    ContactPersonMobile1 = data.ContactPersonMobile1,
                    ContactPersonMobile2 = data.ContactPersonMobile2,
                    ContactPersonMobile3 = data.ContactPersonMobile3,
                    ContactPersonSec = data.ContactPersonSec,
                    ContactPersonThird = data.ContactPersonThird,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    CustomeFax = data.CustomeFax,
                    CustomerAddress = data.CustomerAddress,
                    CustomerCode = data.CustomerCode,
                    CustomerEmail = data.CustomerEmail,
                    CUSTOMERMOBILENO = data.CUSTOMERMOBILENO,
                    CustomerName = data.CustomerName,
                    CustomerPANNo = data.CustomerPANNo,
                    CustomerTelNo = data.CustomerTelNo,
                    CustomerTelNoSec = data.CustomerTelNoSec,
                    CustomerWebsite = data.CustomerWebsite,
                    FacilityDetails = data.FacilityDetails,
                    IsActive = data.IsActive,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate
                };
                return record;
            }
            return null;
        }
        public static List<CustomerVM> Convert(List<Customer> data)
        {
            List<CustomerVM> records = new List<CustomerVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region Vendor_Bid_Price(VendorAssign)
        public static Vendor_Bid_Price Convert(VendorBidPriceVM data)
        {
            if (data != null)
            {
                Vendor_Bid_Price record = new Vendor_Bid_Price
                {
                    id = data.id,
                    vendor_id = data.vendor_id,
                    product_id = data.product_id,
                    price = data.price,
                    is_active = data.is_active,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate
                };
                return record;
            }
            return null;
        }
        public static VendorBidPriceVM Convert(Vendor_Bid_Price data)
        {
            if (data != null)
            {
                VendorBidPriceVM record = new VendorBidPriceVM
                {
                    id = data.id,
                    vendor_id = data.vendor_id,
                    product_id = data.product_id,
                    price = data.price,
                    is_active = data.is_active,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                    Customers = Convert(data.Customer),
                    INPRODUCTs = Convert(data.IN_PRODUCT)
                };
                return record;
            }
            return null;
        }
        public static List<VendorBidPriceVM> Convert(List<Vendor_Bid_Price> data)
        {
            List<VendorBidPriceVM> records = new List<VendorBidPriceVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region IN_Branch(Branch Assign for Product)
        public static IN_BRANCH Convert(INBranchVM data)
        {
            if (data != null)
            {
                IN_BRANCH record = new IN_BRANCH
                {
                    ID = data.ID,
                    BRANCH_ID = data.BRANCH_ID,
                    COMM_AC = data.COMM_AC,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    EXCESS_LEVEL = data.EXCESS_LEVEL,
                    INVENTORY_AC = data.INVENTORY_AC,
                    MAX_HOLDING_QTY = data.MAX_HOLDING_QTY,
                    IS_ACTIVE = data.IS_ACTIVE,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    PRODUCT_ID = data.PRODUCT_ID,
                    PURCHASE_AC = data.PURCHASE_AC,
                    REORDER_LEVEL = data.REORDER_LEVEL,
                    REORDER_QTY = data.REORDER_QTY,
                    SALES_AC = data.SALES_AC,
                    stock_in_hand = data.stock_in_hand,
                    ProductGroupId = data.ProductGroupId,
                    departmentid = data.departmentid
                };
                return record;
            }
            return null;
        }
        public static INBranchVM Convert(IN_BRANCH data)
        {
            if (data != null)
            {
                INBranchVM record = new INBranchVM
                {
                    ID = data.ID,
                    BRANCH_ID = data.BRANCH_ID,
                    COMM_AC = data.COMM_AC,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    EXCESS_LEVEL = data.EXCESS_LEVEL,
                    INVENTORY_AC = data.INVENTORY_AC,
                    MAX_HOLDING_QTY = data.MAX_HOLDING_QTY,
                    IS_ACTIVE = data.IS_ACTIVE,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    PRODUCT_ID = data.PRODUCT_ID,
                    PURCHASE_AC = data.PURCHASE_AC,
                    REORDER_LEVEL = data.REORDER_LEVEL,
                    REORDER_QTY = data.REORDER_QTY,
                    SALES_AC = data.SALES_AC,
                    stock_in_hand = data.stock_in_hand,
                    ProductGroupId = data.ProductGroupId,
                    departmentid = data.departmentid,
                    Branches = Convert(data.Branches),
                    INPRODUCTs = Convert(data.IN_PRODUCT)
                };
                return record;
            }
            return null;
        }
        public static List<INBranchVM> Convert(List<IN_BRANCH> data)
        {
            List<INBranchVM> records = new List<INBranchVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #endregion

        #region InvPurchase
        #region PurchaseOrderMessage
        public static Purchase_Order_Message Convert(PurchaseOrderMessageVM data)
        {
            if (data != null)
            {
                Purchase_Order_Message record = new Purchase_Order_Message
                {
                    id = data.id,
                    appropiate_cond = data.appropiate_cond,
                    approved_by = data.approved_by,
                    approved_date = data.approved_date,
                    branch_id = data.branch_id,
                    cancelled_by = data.cancelled_by,
                    cancelled_date = data.cancelled_date,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    delivery_date = data.delivery_date,
                    department_id = data.department_id,
                    forwarded_date = data.forwarded_date,
                    forwarded_to = data.forwarded_to,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    order_date = data.order_date,
                    order_no = data.order_no,
                    prod_specfic = data.prod_specfic,
                    received_by = data.received_by,
                    received_date = data.received_date,
                    received_desc = data.received_desc,
                    remarks = data.remarks,
                    status = data.status,
                    vat_amt = data.vat_amt,
                    vendor_code = data.vendor_code,
                };
                return record;
            }
            return null;
        }
        public static PurchaseOrderMessageVM Convert(Purchase_Order_Message data)
        {
            if (data != null)
            {
                PurchaseOrderMessageVM record = new PurchaseOrderMessageVM
                {
                    id = data.id,
                    appropiate_cond = data.appropiate_cond,
                    approved_by = data.approved_by,
                    approved_date = data.approved_date,
                    branch_id = data.branch_id,
                    cancelled_by = data.cancelled_by,
                    cancelled_date = data.cancelled_date,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    delivery_date = data.delivery_date,
                    department_id = data.department_id,
                    forwarded_date = data.forwarded_date,
                    forwarded_to = data.forwarded_to,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    order_date = data.order_date,
                    order_no = data.order_no,
                    prod_specfic = data.prod_specfic,
                    received_by = data.received_by,
                    received_date = data.received_date,
                    received_desc = data.received_desc,
                    remarks = data.remarks,
                    status = data.status,
                    vat_amt = data.vat_amt,
                    vendor_code = data.vendor_code,
                    vendorname = CodeService.GetVendorNameFromPurchaseOrderMessage(data.id),
                    orderby = CodeService.GetEmployeeFullName((int)data.created_by),
                    forwardedto = CodeService.GetEmployeeFullName(data.forwarded_to),
                    approvername = data.approved_by == null ? " " : CodeService.GetEmployeeFullName((int)data.approved_by),
                    receivername = data.received_by == null ? " " : CodeService.GetEmployeeFullName((int)data.received_by)
                };
                return record;
            }
            return null;
        }
        public static List<PurchaseOrderMessageVM> Convert(List<Purchase_Order_Message> data)
        {
            List<PurchaseOrderMessageVM> records = new List<PurchaseOrderMessageVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region BillInfo
        public static Bill_info Convert(BillInfoVM data)
        {
            if (data != null)
            {
                Bill_info record = new Bill_info
                {
                    bill_id = data.bill_id,
                    entered_by = data.entered_by,
                    entered_date = data.entered_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    billno = data.billno,
                    bill_amount = data.bill_amount,
                    bill_date = data.bill_date,
                    bill_discount = data.bill_discount,
                    bill_notes = data.bill_notes,
                    bill_type = data.bill_type,
                    last_paid_date = data.last_paid_date,
                    nontax_amt = data.nontax_amt,
                    paid_amount = data.paid_amount,
                    paid_by = data.paid_by,
                    paid_date = data.paid_date,
                    party_code = data.party_code,
                    taxable_amt = data.taxable_amt,
                    vat_amt = data.vat_amt,
                    forwarded_to = data.forwarded_to, 
                    CancelledBy = data.CancelledBy,
                    CancelledDate=data.CancelledDate,
                    Status = data.Status,
                    branch_id = data.branch_id,
                    VendorName = data.VendorName,
                    Approved_Message=data.Approved_Message,
                    ApprovedBy = string.IsNullOrEmpty(data.ApprovedBy) ? (int?)null : int.Parse(data.ApprovedBy),
                    ApprovedDate = data.ApprovedDate
                };
                return record;
            }
            return null;
        }
        public static BillInfoVM Convert(Bill_info data)
        {
            if (data != null)
            {
                BillInfoVM record = new BillInfoVM
                {
                    bill_id = data.bill_id,
                    entered_by = data.entered_by,
                    entered_date = data.entered_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    billno = data.billno,
                    bill_amount = data.bill_amount,
                    bill_date = data.bill_date,
                    bill_discount = data.bill_discount,
                    bill_notes = data.bill_notes,
                    bill_type = data.bill_type,
                    last_paid_date = data.last_paid_date,
                    nontax_amt = data.nontax_amt,
                    paid_amount = data.paid_amount,
                    paid_by = data.paid_by,
                    paid_date = data.paid_date,
                    party_code = data.party_code,
                    taxable_amt = data.taxable_amt,
                    vat_amt = data.vat_amt,
                    VendorName = data.VendorName,
                    Status = data.Status,
                    branch_id = data.branch_id,
                    forwarded_to = data.forwarded_to,
                    CancelledBy = data.CancelledBy,
                    Approved_Message = data.Approved_Message ,
                    ApprovedBy = CodeService.GetEmployeeFullName((int)(data.ApprovedBy ?? 0)),
                    ApprovedDate = data.ApprovedDate.HasValue ? data.ApprovedDate.Value : (DateTime?)null,
                    CancelledByName = CodeService.GetEmployeeFullName((int)(data.CancelledBy ?? 0)),
                    CancelledDate = data.CancelledDate.HasValue ? data.CancelledDate.Value : (DateTime?)null,
                    forwardedToName = CodeService.GetEmployeeFullName((int)(data.forwarded_to ?? 0)),
                    orderby = CodeService.GetEmployeeFullName(int.Parse(data.entered_by)),
                    paidby = data.paid_by == null ? "" : CodeService.GetEmployeeFullName(int.Parse(data.paid_by)),
                    //RequestingBranch = data.branch_id,
                    PartyName = CodeService.GetVendorNameFromBillInfo(data.bill_id)
                };
                return record;
            }
            return null;
        }
        public static List<BillInfoVM> Convert(List<Bill_info> data)
        {
            try
            {
                List<BillInfoVM> records = new List<BillInfoVM>();
                foreach (var item in data)
                {
                    records.Add(Convert(item));
                }
                return records;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        #region BillSetting
        public static BillSetting Convert(BillSettingVM data)
        {
            if (data != null)
            {
                BillSetting record = new BillSetting
                {
                    company_id = data.company_id,
                    bill_cash_sales = data.bill_cash_sales,
                    bill_purchase = data.bill_purchase,
                    bill_sales = data.bill_sales,
                    contra_voucher = data.contra_voucher,
                    depreciation_voucher = data.depreciation_voucher,
                    disposal_voucher = data.disposal_voucher,
                    EXCHANGE_VOUCHER = data.EXCHANGE_VOUCHER,
                    journal_voucher = data.journal_voucher,
                    payment_voucher = data.payment_voucher,
                    purchase_voucher = data.purchase_voucher,
                    receipt_voucher = data.receipt_voucher,
                    rowid = data.rowid,
                    sales_voucher = data.sales_voucher,
                    TRADING_VOUCHER = data.TRADING_VOUCHER,
                    TRANSIT_VOUCHER = data.TRANSIT_VOUCHER,
                    TRAN_VOUCHER = data.TRAN_VOUCHER,
                    voucher_number = data.voucher_number
                };
                return record;
            }
            return null;
        }
        public static BillSettingVM Convert(BillSetting data)
        {
            if (data != null)
            {
                BillSettingVM record = new BillSettingVM
                {
                    company_id = data.company_id,
                    bill_cash_sales = data.bill_cash_sales,
                    bill_purchase = data.bill_purchase,
                    bill_sales = data.bill_sales,
                    contra_voucher = data.contra_voucher,
                    depreciation_voucher = data.depreciation_voucher,
                    disposal_voucher = data.disposal_voucher,
                    EXCHANGE_VOUCHER = data.EXCHANGE_VOUCHER,
                    journal_voucher = data.journal_voucher,
                    payment_voucher = data.payment_voucher,
                    purchase_voucher = data.purchase_voucher,
                    receipt_voucher = data.receipt_voucher,
                    rowid = data.rowid,
                    sales_voucher = data.sales_voucher,
                    TRADING_VOUCHER = data.TRADING_VOUCHER,
                    TRANSIT_VOUCHER = data.TRANSIT_VOUCHER,
                    TRAN_VOUCHER = data.TRAN_VOUCHER,
                    voucher_number = data.voucher_number
                };
                return record;
            }
            return null;
        }
        public static List<BillSettingVM> Convert(List<BillSetting> data)
        {
            List<BillSettingVM> records = new List<BillSettingVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InPurchase
        public static IN_PURCHASE Convert(InPurchaseVM data)
        {
            if (data != null)
            {
                IN_PURCHASE record = new IN_PURCHASE
                {
                    pur_id = data.pur_id,
                    bill_id = data.bill_id,
                    branch_id = data.branch_id,
                    entered_by = data.entered_by,
                    entered_date = data.entered_date,
                    foc_qty = data.foc_qty,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    order_msg_id = data.order_msg_id,
                    prod_code = data.prod_code,
                    p_batch = data.p_batch,
                    p_expiry = data.p_expiry,
                    p_qty = data.p_qty,
                    p_rate = data.p_rate,
                    p_sn_from = data.p_sn_from,
                    p_sn_to = data.p_sn_to,
                    p_stk_remain = data.p_stk_remain,
                    req_id = data.req_id,
                    departmentid = data.departmentid
                };
                return record;
            }
            return null;
        }
        public static InPurchaseVM Convert(IN_PURCHASE data)
        {
            if (data != null)
            {
                InPurchaseVM record = new InPurchaseVM
                {
                    pur_id = data.pur_id,
                    bill_id = data.bill_id,
                    branch_id = data.branch_id,
                    entered_by = data.entered_by,
                    entered_date = data.entered_date,
                    foc_qty = data.foc_qty,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    order_msg_id = data.order_msg_id,
                    prod_code = data.prod_code,
                    p_batch = data.p_batch,
                    p_expiry = data.p_expiry,
                    p_qty = data.p_qty,
                    p_rate = data.p_rate,
                    p_sn_from = data.p_sn_from,
                    p_sn_to = data.p_sn_to,
                    p_stk_remain = data.p_stk_remain,
                    req_id = data.req_id,
                    departmentid = data.departmentid,
                    ProductName = CodeService.GetInProductName(data.prod_code),
                    Amount = (data.p_rate * data.p_qty),
                    BillNo = data.bill_id == null ? " " : CodeService.GetBillNoFromBillId((int)data.bill_id),// CodeService.GetBillNoFromBillId((int)data.bill_id),
                    VendorName = data.bill_id == null ? " " : CodeService.GetVendorNameFromBillId((int)data.bill_id)//CodeService.GetVendorNameFromBillId((int)data.bill_id),
                };
                return record;
            }
            return null;
        }
        public static List<InPurchaseVM> Convert(List<IN_PURCHASE> data)
        {
            List<InPurchaseVM> records = new List<InPurchaseVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region PurchaseOrder
        public static Purchase_Order Convert(PurchaseOrderVM data)
        {
            if (data != null)
            {
                Purchase_Order record = new Purchase_Order
                {
                    id = data.id,
                    amount = data.amount,
                    order_message_id = data.order_message_id,
                    product_code = data.product_code,
                    qty = data.qty,
                    rate = data.rate,
                    Received_Qty = data.Received_Qty,
                    bill_id = data.bill_id
                };
                return record;
            }
            return null;
        }
        public static PurchaseOrderVM Convert(Purchase_Order data)
        {
            if (data != null)
            {
                PurchaseOrderVM record = new PurchaseOrderVM
                {
                    id = data.id,
                    amount = data.amount,
                    order_message_id = data.order_message_id,
                    product_code = data.product_code,
                    qty = data.qty,
                    rate = data.rate,
                    Received_Qty = data.Received_Qty,
                    bill_id = data.bill_id,
                    productname = CodeService.GetInProductName(data.product_code),
                    serialstatus = CodeService.GetSerialStatusForProductById(data.product_code),
                    PurchaseOrderMessages = Convert(data.Purchase_Order_Message),
                    BillInfoVM = Convert(data.Bill_Info)
                };

                return record;
            }
            return null;
        }
        public static List<PurchaseOrderVM> Convert(List<Purchase_Order> data)
        {
            List<PurchaseOrderVM> records = new List<PurchaseOrderVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region PurchaseOrderMessageHistory
        public static Purchase_Order_Message_History Convert(PurchaseOrderMessageHistoryVM data)
        {
            if (data != null)
            {
                Purchase_Order_Message_History record = new Purchase_Order_Message_History
                {
                    id = data.id,
                    from_user = data.from_user,
                    narration = data.narration,
                    Ord_id = data.Ord_id,
                    Requ_date = data.Requ_date,
                    to_user = data.to_user,
                };
                return record;
            }
            return null;
        }
        public static PurchaseOrderMessageHistoryVM Convert(Purchase_Order_Message_History data)
        {
            if (data != null)
            {
                PurchaseOrderMessageHistoryVM record = new PurchaseOrderMessageHistoryVM
                {
                    id = data.id,
                    Ord_id = data.Ord_id,
                    to_user = data.to_user,
                    Requ_date = data.Requ_date,
                    narration = data.narration,
                    from_user = data.from_user,
                    PurchaseOrderMessages = Convert(data.Purchase_Order_Message)
                };
                return record;
            }
            return null;
        }
        public static List<PurchaseOrderMessageHistoryVM> Convert(List<Purchase_Order_Message_History> data)
        {
            List<PurchaseOrderMessageHistoryVM> records = new List<PurchaseOrderMessageHistoryVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region PurchaseOrderReceived
        public static Purchase_Order_Received Convert(PurchaseOrderReceivedVM data)
        {
            if (data != null)
            {
                Purchase_Order_Received record = new Purchase_Order_Received
                {
                    Id = data.Id,
                    Amount = data.Amount,
                    foc_qty = data.foc_qty,
                    Order_Message_Id = data.Order_Message_Id,
                    Product_Id = data.Product_Id,
                    Qty = data.Qty,
                    Rate = data.Rate,
                    Received_By = data.Received_By,
                    Received_Date = data.Received_Date,
                    //Purchase_Order_Message = Convert(data.PurchaseOrderMessages)
                };
                return record;
            }
            return null;
        }
        public static PurchaseOrderReceivedVM Convert(Purchase_Order_Received data)
        {
            if (data != null)
            {
                PurchaseOrderReceivedVM record = new PurchaseOrderReceivedVM
                {
                    Id = data.Id,
                    Amount = data.Amount,
                    foc_qty = data.foc_qty,
                    Order_Message_Id = data.Order_Message_Id,
                    Product_Id = data.Product_Id,
                    Qty = data.Qty,
                    Rate = data.Rate,
                    Received_By = data.Received_By,
                    Received_Date = data.Received_Date,
                    PurchaseOrderMessages = Convert(data.Purchase_Order_Message)
                };
                return record;
            }
            return null;
        }
        public static List<PurchaseOrderReceivedVM> Convert(List<Purchase_Order_Received> data)
        {
            List<PurchaseOrderReceivedVM> records = new List<PurchaseOrderReceivedVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InPurchaseReturn
        public static In_PurchaseReturn Convert(InPurchaseReturnVM data)
        {
            if (data != null)
            {
                In_PurchaseReturn record = new In_PurchaseReturn
                {
                    Id = data.Id,
                    Bill_Id = data.Bill_Id,
                    Bill_No = data.Bill_No,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    Narration = data.Narration,
                    PR_No = data.PR_No,
                    Vat = data.Vat,
                    Vendor_Id = data.Vendor_Id,
                    Discount = data.Discount,
                    GrandTotal = data.GrandTotal
                };
                return record;
            }
            return null;
        }
        public static InPurchaseReturnVM Convert(In_PurchaseReturn data)
        {
            if (data != null)
            {
                InPurchaseReturnVM record = new InPurchaseReturnVM
                {
                    Id = data.Id,
                    Bill_Id = data.Bill_Id,
                    Bill_No = data.Bill_No,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    Narration = data.Narration,
                    PR_No = data.PR_No,
                    Vat = data.Vat,
                    Vendor_Id = data.Vendor_Id,
                    Discount = data.Discount,
                    GrandTotal = data.GrandTotal,
                };
                return record;
            }
            return null;
        }
        public static List<InPurchaseReturnVM> Convert(List<In_PurchaseReturn> data)
        {
            List<InPurchaseReturnVM> records = new List<InPurchaseReturnVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InPurchaseReturnDetails
        public static In_PurchaseReturnDetails Convert(InPurchaseReturnDetailsVM data)
        {
            if (data != null)
            {
                In_PurchaseReturnDetails record = new In_PurchaseReturnDetails
                {
                    Id = data.Id,
                    Amount = data.Amount,
                    ProductId = data.ProductId,
                    PR_Id = data.PR_Id,
                    Rate = data.Rate,
                    ReturnQty = data.ReturnQty,
                    Vat = data.Vat
                };
                return record;
            }
            return null;
        }
        public static InPurchaseReturnDetailsVM Convert(In_PurchaseReturnDetails data)
        {
            if (data != null)
            {
                InPurchaseReturnDetailsVM record = new InPurchaseReturnDetailsVM
                {
                    Id = data.Id,
                    Amount = data.Amount,
                    ProductId = data.ProductId,
                    PR_Id = data.PR_Id,
                    Rate = data.Rate,
                    ReturnQty = data.ReturnQty,
                    Vat = data.Vat,
                    InPurchaseReturns = Convert(data.In_PurchaseReturn)
                };
                return record;
            }
            return null;
        }
        public static List<InPurchaseReturnDetailsVM> Convert(List<In_PurchaseReturnDetails> data)
        {
            List<InPurchaseReturnDetailsVM> records = new List<InPurchaseReturnDetailsVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region TempPurchase
        public static Temp_Purchase Convert(TempPurchaseVM data)
        {
            if (data != null)
            {
                Temp_Purchase record = new Temp_Purchase
                {
                    id = data.id,
                    account_no = data.account_no,
                    ac_amount = data.ac_amount,
                    ac_type = data.ac_type,
                    amount = data.amount,
                    asset_req_id = data.asset_req_id,
                    batch = data.batch,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    flag = data.flag,
                    foc_qty = data.foc_qty,
                    order_message_id = data.order_message_id,
                    product_code = data.product_code,
                    qty = data.qty,
                    rate = data.rate,
                    serial_end = data.serial_end,
                    serial_start = data.serial_start,
                    session_id = data.session_id,
                    IsActive = data.IsActive,
                    bill_id=data.bill_id??0
                };
                return record;
            }
            return null;
        }
        public static TempPurchaseVM Convert(Temp_Purchase data)
        {
            if (data != null)
            {
                TempPurchaseVM record = new TempPurchaseVM
                {
                    id = data.id,
                    account_no = data.account_no,
                    ac_amount = data.ac_amount,
                    ac_type = data.ac_type,
                    amount = data.amount,
                    asset_req_id = data.asset_req_id,
                    batch = data.batch,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    flag = data.flag,
                    foc_qty = data.foc_qty,
                    order_message_id = data.order_message_id,
                    product_code = data.product_code,
                    qty = data.qty,
                    rate = data.rate,
                    serial_end = data.serial_end,
                    serial_start = data.serial_start,
                    session_id = data.session_id,
                    IsActive = data.IsActive,
                    bill_id=data.bill_id??0,
                    Unit = CodeService.GetUnitForProductName(data.product_code),
                    SerialStatus = CodeService.GetSerialStatusForProductById(data.product_code),
                    productname = CodeService.GetInProductName(data.product_code)
                };
                return record;
            }
            return null;
        }
        public static List<TempPurchaseVM> Convert(List<Temp_Purchase> data)
        {
            List<TempPurchaseVM> records = new List<TempPurchaseVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region TempPurchaseOther
        public static Temp_Purchase_Other Convert(TempPurchaseOtherVM data)
        {
            if (data != null)
            {
                Temp_Purchase_Other record = new Temp_Purchase_Other
                {
                    id = data.id,
                    batch = data.batch,
                    is_approved = data.is_approved,
                    temp_purchase_id = data.temp_purchase_id,
                    qty = data.qty,
                    session_id = data.session_id,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to,
                    IsActive = data.IsActive
                };
                return record;
            }
            return null;
        }
        public static TempPurchaseOtherVM Convert(Temp_Purchase_Other data)
        {
            if (data != null)
            {
                TempPurchaseOtherVM record = new TempPurchaseOtherVM
                {
                    id = data.id,
                    batch = data.batch,
                    is_approved = data.is_approved,
                    temp_purchase_id = data.temp_purchase_id,
                    qty = data.qty,
                    session_id = data.session_id,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to,
                    IsActive = data.IsActive,
                };
                return record;
            }
            return null;
        }
        public static List<TempPurchaseOtherVM> Convert(List<Temp_Purchase_Other> data)
        {
            List<TempPurchaseOtherVM> records = new List<TempPurchaseOtherVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region OtherBillsInfo
        public static OtherBillsInfo Convert(OtherBillsInfoVM data)
        {
            if (data != null)
            {
                OtherBillsInfo record = new OtherBillsInfo
                {
                    Id = data.Id,
                    entered_by = data.entered_by,
                    entered_date = data.entered_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    billno = data.billno,
                    bill_amount = data.bill_amount,
                    bill_date = data.bill_date,
                    bill_discount = data.bill_discount,
                    bill_notes = data.bill_notes,
                    paid_by = data.paid_by,
                    paid_date = data.paid_date,
                    vat_amt = data.vat_amt,
                    VendorName = data.VendorName,
                    Invoice = data.Invoice,
                    is_paid = data.is_paid,
                    Received_By = data.Received_By,
                    Received_Date = data.Received_Date,
                    deletevoucher = data.deletevoucher
                };
                return record;
            }
            return null;
        }
        public static OtherBillsInfoVM Convert(OtherBillsInfo data)
        {
            if (data != null)
            {
                OtherBillsInfoVM record = new OtherBillsInfoVM
                {
                    Id = data.Id,
                    entered_by = data.entered_by,
                    entered_date = data.entered_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    billno = data.billno,
                    bill_amount = data.bill_amount,
                    bill_date = data.bill_date,
                    bill_discount = data.bill_discount,
                    bill_notes = data.bill_notes,
                    paid_by = data.paid_by,
                    paid_date = data.paid_date,
                    vat_amt = data.vat_amt,
                    VendorName = data.VendorName,
                    Invoice = data.Invoice,
                    is_paid = data.is_paid,
                    Received_By = data.Received_By,
                    Received_Date = data.Received_Date,
                    deletevoucher = data.deletevoucher
                };
                return record;
            }
            return null;
        }
        public static List<OtherBillsInfoVM> Convert(List<OtherBillsInfo> data)
        {
            List<OtherBillsInfoVM> records = new List<OtherBillsInfoVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region SerialProductStock
        public static SerialProductStock Convert(SerialProductStockVM data)
        {
            if (data != null)
            {
                SerialProductStock record = new SerialProductStock
                {
                    batchNum = data.batchNum,
                    branchId = data.branchId,
                    cardNum = data.cardNum,
                    customerId = data.customerId,
                    issuedBy = data.issuedBy,
                    issuedDate = data.issuedDate,
                    lastmovementId = data.lastmovementId,
                    productId = data.productId,
                    purId = data.purId,
                    reqId = data.reqId,
                    sequenceNum = data.sequenceNum,
                    status = data.status,
                    departmentid = data.departmentid,
                    disposemesid = data.disposemesid
                };
                return record;
            }
            return null;
        }
        public static SerialProductStockVM Convert(SerialProductStock data)
        {
            if (data != null)
            {
                SerialProductStockVM record = new SerialProductStockVM
                {
                    batchNum = data.batchNum,
                    branchId = data.branchId,
                    cardNum = data.cardNum,
                    customerId = data.customerId,
                    issuedBy = data.issuedBy,
                    issuedDate = data.issuedDate,
                    lastmovementId = data.lastmovementId,
                    productId = data.productId,
                    purId = data.purId,
                    reqId = data.reqId,
                    sequenceNum = data.sequenceNum,
                    status = data.status,
                    departmentid = data.departmentid,
                    disposemesid = data.disposemesid
                };
                return record;
            }
            return null;
        }
        public static List<SerialProductStockVM> Convert(List<SerialProductStock> data)
        {
            List<SerialProductStockVM> records = new List<SerialProductStockVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region SerialProductStockHistory
        public static SerialProductStockHistory Convert(SerialProductStockHistoryVM data)
        {
            if (data != null)
            {
                SerialProductStockHistory record = new SerialProductStockHistory
                {
                    batchNum = data.batchNum,
                    status = data.status,
                    reqId = data.reqId,
                    purId = data.purId,
                    branchId = data.branchId,
                    cardNum = data.cardNum,
                    customerId = data.customerId,
                    fsequenceNum = data.fsequenceNum,
                    issuedBy = data.issuedBy,
                    issuedDate = data.issuedDate,
                    lastmovementId = data.lastmovementId,
                    productId = data.productId,
                    tsequenceNum = data.tsequenceNum
                };
                return record;
            }
            return null;
        }
        public static SerialProductStockHistoryVM Convert(SerialProductStockHistory data)
        {
            if (data != null)
            {
                SerialProductStockHistoryVM record = new SerialProductStockHistoryVM
                {
                    batchNum = data.batchNum,
                    status = data.status,
                    reqId = data.reqId,
                    purId = data.purId,
                    branchId = data.branchId,
                    cardNum = data.cardNum,
                    customerId = data.customerId,
                    fsequenceNum = data.fsequenceNum,
                    issuedBy = data.issuedBy,
                    issuedDate = data.issuedDate,
                    lastmovementId = data.lastmovementId,
                    productId = data.productId,
                    tsequenceNum = data.tsequenceNum
                };
                return record;
            }
            return null;
        }
        public static List<SerialProductStockHistoryVM> Convert(List<SerialProductStockHistory> data)
        {
            List<SerialProductStockHistoryVM> records = new List<SerialProductStockHistoryVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #endregion

        #region InvMovement
        #region InRequisitionMessage
        public static IN_Requisition_Message Convert(InRequisitionMessageVM data)
        {
            if (data != null)
            {
                IN_Requisition_Message record = new IN_Requisition_Message
                {
                    id = data.id,
                    Acknowledged_By = data.Acknowledged_By,
                    Acknowledged_Date = data.Acknowledged_Date,
                    Acknowledged_Message = data.Acknowledged_Message,
                    Approved_Date = data.Approved_Date,
                    Approver_id = data.Approver_id,
                    Approver_message = data.Approver_message,
                    branch_id = data.branch_id,
                    Delivered_By = data.Delivered_By,
                    Delivered_Date = data.Delivered_Date,
                    Delivery_Message = data.Delivery_Message,
                    dept_id = data.dept_id,
                    Forwarded_To = data.Forwarded_To,
                    IS_SCHEDULE = data.IS_SCHEDULE,
                    priority = data.priority,
                    recommed_by = data.recommed_by,
                    recommed_date = data.recommed_date,
                    recommed_message = data.recommed_message,
                    rejected_by = data.rejected_by,
                    rejected_date = data.rejected_date,
                    rejected_message = data.rejected_message,
                    Requ_by = data.Requ_by,
                    Requ_date = data.Requ_date,
                    Requ_Message = data.Requ_Message,
                    status = data.status,
                    Req_no = data.Req_no,
                    UNSCHEDULE_REASON = data.UNSCHEDULE_REASON
                };
                return record;
            }
            return null;
        }
        public static InRequisitionMessageVM Convert(IN_Requisition_Message data)
        {
            if (data != null)
            {
                InRequisitionMessageVM record = new InRequisitionMessageVM
                {
                    id = data.id,
                    Acknowledged_By = data.Acknowledged_By,
                    Acknowledged_Date = data.Acknowledged_Date,
                    Acknowledged_Message = data.Acknowledged_Message,
                    Approved_Date = data.Approved_Date,
                    Approver_id = data.Approver_id,
                    Approver_message = data.Approver_message,
                    branch_id = data.branch_id,
                    Delivered_By = data.Delivered_By,
                    Delivered_Date = data.Delivered_Date,
                    Delivery_Message = data.Delivery_Message,
                    dept_id = data.dept_id,
                    Forwarded_To = data.Forwarded_To,
                    IS_SCHEDULE = data.IS_SCHEDULE,
                    priority = data.priority,
                    recommed_by = data.recommed_by,
                    recommed_date = data.recommed_date,
                    recommed_message = data.recommed_message,
                    rejected_by = data.rejected_by,
                    rejected_date = data.rejected_date,
                    rejected_message = data.rejected_message,
                    Requ_by = data.Requ_by,
                    Requ_date = data.Requ_date,
                    Requ_Message = data.Requ_Message,
                    status = data.status,
                    Req_no = data.Req_no,
                    UNSCHEDULE_REASON = data.UNSCHEDULE_REASON,
                    RequestingBranch = CodeService.GetBranchName(data.branch_id),
                    RequestedBy = CodeService.GetEmployeeFullName(data.Requ_by),
                    RecommendedBy = CodeService.GetEmployeeFullName(data.recommed_by),
                    ApprovedBy = data.Approver_id == null ? " " : CodeService.GetEmployeeFullName((int)data.Approver_id),
                    DispatchedBy = data.Delivered_By == null ? " " : CodeService.GetEmployeeFullName((int)data.Delivered_By),
                    RequestingDepartment = CodeService.GetDepartmentName(data.dept_id),
                    ForwardedBranch = CodeService.GetBranchName(data.Forwarded_To),
                    RequestingContactNo = CodeService.GetRequestingBranchContactNoByBranchId(data.branch_id),
                    dispatchedid = CodeService.GetInDispatchMessageIdWithReqMesId(data.id)
                };
                return record;
            }
            return null;
        }
        public static List<InRequisitionMessageVM> Convert(List<IN_Requisition_Message> data)
        {
            List<InRequisitionMessageVM> records = new List<InRequisitionMessageVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InRequisition
        public static IN_Requisition Convert(InRequisitionVM data)
        {
            if (data != null)
            {
                IN_Requisition record = new IN_Requisition
                {
                    id = data.id,
                    Approved_Quantity = data.Approved_Quantity,
                    Delivered_Quantity = data.Delivered_Quantity,
                    item = data.item,
                    quantity = data.quantity,
                    Received_Quantity = data.Received_Quantity,
                    REMAIN = data.REMAIN,
                    Requistion_message_id = data.Requistion_message_id,
                    unit = data.unit
                };
                return record;
            }
            return null;
        }
        public static InRequisitionVM Convert(IN_Requisition data)
        {
            //var Session = HttpContext.Current.Session;
            var bid = CodeService.getBranchIdFromInRequisitionMessage(data.Requistion_message_id); //(int)(Session["BranchId"]);
            var rbid = CodeService.getReqBranchIdFromInRequisitionMessage(data.Requistion_message_id); //(int)(Session["BranchId"]);

            if (data != null)
            {
                InRequisitionVM record = new InRequisitionVM
                {
                    id = data.id,
                    Approved_Quantity = data.Approved_Quantity,
                    Delivered_Quantity = data.Delivered_Quantity,
                    item = data.item,
                    quantity = data.quantity,
                    Received_Quantity = data.Received_Quantity,
                    REMAIN = data.REMAIN,
                    Requistion_message_id = data.Requistion_message_id,
                    unit = data.unit,
                    p_rate = CodeService.GetRateForProductId(data.item),
                    ProductName = CodeService.GetInProductName(data.item),
                    serialstatus = CodeService.GetSerialStatusForProductInDispatchRequisition(data.item),
                    stockinhand = CodeService.GetStockInHandfromPId(data.item, bid),
                    branchstock = CodeService.GetStockInHandfromPId(data.item, rbid),
                    INRequisitionMessage = Convert(data.IN_Requisition_Message)
                };
                return record;
            }
            return null;
        }
        public static List<InRequisitionVM> Convert(List<IN_Requisition> data)
        {
            List<InRequisitionVM> records = new List<InRequisitionVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InRequisitionDetail
        public static IN_Requisition_Detail Convert(InRequisitionDetailVM data)
        {
            if (data != null)
            {
                IN_Requisition_Detail record = new IN_Requisition_Detail
                {
                    id = data.id,
                    Approved_Quantity = data.Approved_Quantity,
                    Delivered_Quantity = data.Delivered_Quantity,
                    item = data.item,
                    quantity = data.quantity,
                    Received_Quantity = data.Received_Quantity,
                    Requistion_message_id = data.Requistion_message_id,
                    unit = data.unit,
                    remain = data.remain,
                    session_id = data.session_id
                };
                return record;
            }
            return null;
        }
        public static InRequisitionDetailVM Convert(IN_Requisition_Detail data)
        {
            if (data != null)
            {
                InRequisitionDetailVM record = new InRequisitionDetailVM
                {
                    id = data.id,
                    Approved_Quantity = data.Approved_Quantity,
                    Delivered_Quantity = data.Delivered_Quantity,
                    item = data.item,
                    quantity = data.quantity,
                    Received_Quantity = data.Received_Quantity,
                    Requistion_message_id = data.Requistion_message_id,
                    unit = data.unit,
                    remain = data.remain,
                    session_id = data.session_id,
                    INRequisitionMessage = Convert(data.IN_Requisition_Message)
                };
                return record;
            }
            return null;
        }
        public static List<InRequisitionDetailVM> Convert(List<IN_Requisition_Detail> data)
        {
            List<InRequisitionDetailVM> records = new List<InRequisitionDetailVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InRequisitionDetailOther
        public static IN_Requisition_Detail_Other Convert(InRequisitionDetailOtherVM data)
        {
            if (data != null)
            {
                IN_Requisition_Detail_Other record = new IN_Requisition_Detail_Other
                {
                    id = data.id,
                    batch = data.batch,
                    detail_id = data.detail_id,
                    is_approved = data.is_approved,
                    productid = data.productid,
                    qty = data.qty,
                    session_id = data.session_id,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to
                };
                return record;
            }
            return null;
        }
        public static InRequisitionDetailOtherVM Convert(IN_Requisition_Detail_Other data)
        {
            if (data != null)
            {
                InRequisitionDetailOtherVM record = new InRequisitionDetailOtherVM
                {
                    id = data.id,
                    batch = data.batch,
                    detail_id = data.detail_id,
                    is_approved = data.is_approved,
                    productid = data.productid,
                    qty = data.qty,
                    session_id = data.session_id,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to,
                };
                return record;
            }
            return null;
        }
        public static List<InRequisitionDetailOtherVM> Convert(List<IN_Requisition_Detail_Other> data)
        {
            List<InRequisitionDetailOtherVM> records = new List<InRequisitionDetailOtherVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InReceivedMessage
        public static IN_RECEIVED_MESSAGE Convert(InReceivedMessageVM data)
        {
            if (data != null)
            {
                IN_RECEIVED_MESSAGE record = new IN_RECEIVED_MESSAGE
                {
                    id = data.id,
                    dis_msg_id = data.dis_msg_id,
                    received_by = data.received_by,
                    received_date = data.received_date,
                    received_msg = data.received_msg,
                    req_msg_id = data.req_msg_id
                };
                return record;
            }
            return null;
        }
        public static InReceivedMessageVM Convert(IN_RECEIVED_MESSAGE data)
        {
            if (data != null)
            {
                InReceivedMessageVM record = new InReceivedMessageVM
                {
                    id = data.id,
                    dis_msg_id = data.dis_msg_id,
                    received_by = data.received_by,
                    received_date = data.received_date,
                    received_msg = data.received_msg,
                    req_msg_id = data.req_msg_id
                };
                return record;
            }
            return null;
        }
        public static List<InReceivedMessageVM> Convert(List<IN_RECEIVED_MESSAGE> data)
        {
            List<InReceivedMessageVM> records = new List<InReceivedMessageVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InReceived
        public static IN_RECEIVED Convert(InReceivedVM data)
        {
            if (data != null)
            {
                IN_RECEIVED record = new IN_RECEIVED
                {
                    id = data.id,
                    product_id = data.product_id,
                    received_msg_id = data.received_msg_id,
                    received_qty = data.received_qty,
                    unit = data.unit
                };
                return record;
            }
            return null;
        }
        public static InReceivedVM Convert(IN_RECEIVED data)
        {
            if (data != null)
            {
                InReceivedVM record = new InReceivedVM
                {
                    id = data.id,
                    product_id = data.product_id,
                    received_msg_id = data.received_msg_id,
                    received_qty = data.received_qty,
                    unit = data.unit,
                    p_rate = CodeService.GetRateForProductId(data.product_id),
                    //InReceivedMessage = Convert(data.IN_RECEIVED_MESSAGE)
                };
                return record;
            }
            return null;
        }
        public static List<InReceivedVM> Convert(List<IN_RECEIVED> data)
        {
            List<InReceivedVM> records = new List<InReceivedVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InDispatchedMessage
        public static IN_DISPATCH_MESSAGE Convert(InDispatchedMessageVM data)
        {
            if (data != null)
            {
                IN_DISPATCH_MESSAGE record = new IN_DISPATCH_MESSAGE
                {
                    id = data.id,
                    dispatched_by = data.dispatched_by,
                    dispatched_date = data.dispatched_date,
                    dispatch_message = data.dispatch_message,
                    req_id = data.req_id,
                    status = data.status,
                    stkFlag = data.stkFlag
                };
                return record;
            }
            return null;
        }
        public static InDispatchedMessageVM Convert(IN_DISPATCH_MESSAGE data)
        {
            if (data != null)
            {
                InDispatchedMessageVM record = new InDispatchedMessageVM
                {
                    id = data.id,
                    dispatched_by = data.dispatched_by,
                    dispatched_date = data.dispatched_date,
                    dispatch_message = data.dispatch_message,
                    req_id = data.req_id,
                    status = data.status,
                    stkFlag = data.stkFlag
                };
                return record;
            }
            return null;
        }
        public static List<InDispatchedMessageVM> Convert(List<IN_DISPATCH_MESSAGE> data)
        {
            List<InDispatchedMessageVM> records = new List<InDispatchedMessageVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InDispatched
        public static IN_DISPATCH Convert(InDispatchedVM data)
        {
            if (data != null)
            {
                IN_DISPATCH record = new IN_DISPATCH
                {
                    id = data.id,
                    product_id = data.product_id,
                    received_qty = data.received_qty,
                    dispatched_date = data.dispatched_date,
                    dispatched_qty = data.dispatched_qty,
                    dispatch_msg_id = data.dispatch_msg_id,
                    from_branch = data.from_branch,
                    rate = data.rate,
                    received_date = data.received_date,
                    remain = data.remain,
                    to_branch = data.to_branch
                };
                return record;
            }
            return null;
        }
        public static InDispatchedVM Convert(IN_DISPATCH data)
        {
            if (data != null)
            {
                InDispatchedVM record = new InDispatchedVM
                {
                    id = data.id,
                    product_id = data.product_id,
                    received_qty = data.received_qty,
                    dispatched_date = data.dispatched_date,
                    dispatched_qty = data.dispatched_qty,
                    dispatch_msg_id = data.dispatch_msg_id,
                    from_branch = data.from_branch,
                    rate = data.rate,
                    received_date = data.received_date,
                    remain = data.remain,
                    to_branch = data.to_branch,
                    p_rate = CodeService.GetRateForProductId(data.product_id),
                    approveqty = CodeService.GetApproveQtyByPIdAndDispacthId(data.product_id, data.dispatch_msg_id),
                    productname = CodeService.GetInProductName(data.product_id),
                    unit = CodeService.GetUnitForProductName(data.product_id),
                    //InDispatchedMessage = Convert(data.IN_DISPATCH_MESSAGE)
                };
                return record;
            }
            return null;
        }
        public static List<InDispatchedVM> Convert(List<IN_DISPATCH> data)
        {
            List<InDispatchedVM> records = new List<InDispatchedVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InTempRequisition
        public static IN_Temp_Requisition Convert(InTempRequitionVM data)
        {
            if (data != null)
            {
                IN_Temp_Requisition record = new IN_Temp_Requisition
                {
                    id = data.id,
                    Item = data.Item,
                    quantity = data.quantity,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    Product_Code = data.Product_Code,
                    session_id = data.session_id,
                    unit = data.unit
                };
                return record;
            }
            return null;
        }
        public static InTempRequitionVM Convert(IN_Temp_Requisition data)
        {
            if (data != null)
            {
                InTempRequitionVM record = new InTempRequitionVM
                {
                    id = data.id,
                    Item = data.Item,
                    quantity = data.quantity,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    Product_Code = data.Product_Code,
                    session_id = data.session_id,
                    unit = data.unit,
                    SerialStatus = CodeService.GetSerialStatusForProductById(data.Item),
                    productname = CodeService.GetInProductName(data.Item)
                };
                return record;
            }
            return null;
        }
        public static List<InTempRequitionVM> Convert(List<IN_Temp_Requisition> data)
        {
            List<InTempRequitionVM> records = new List<InTempRequitionVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InDisposeMessage
        public static In_Dispose_Message Convert(InDisposeMessageVM data)
        {
            if (data != null)
            {
                In_Dispose_Message record = new In_Dispose_Message
                {
                    Id = data.Id,
                    ApprovedBy = data.ApprovedBy,
                    ApprovedDate = data.ApprovedDate,
                    DisposedBy = data.DisposedBy,
                    DisposedDate = data.DisposedDate,
                    DisposeReason = data.DisposeReason,
                    DisposingBranchId = data.DisposingBranchId,
                    DisposingDepartmentId = data.DisposingDepartmentId,
                    ForwardedForApproval = data.ForwardedForApproval,
                    ModidiedDate = data.ModidiedDate,
                    ModifiedBy = data.ModifiedBy,
                    RejectedBy = data.RejectedBy,
                    RejectionDate = data.RejectionDate,
                    RequestBy = data.RequestBy,
                    RequestDate = data.RequestDate,
                    Status = data.Status,
                    TotalAmount = data.TotalAmount,
                    VatAmount = data.VatAmount
                };
                return record;
            }
            return null;
        }
        public static InDisposeMessageVM Convert(In_Dispose_Message data)
        {
            if (data != null)
            {
                InDisposeMessageVM record = new InDisposeMessageVM
                {
                    Id = data.Id,
                    ApprovedBy = data.ApprovedBy,
                    ApprovedDate = data.ApprovedDate,
                    DisposedBy = data.DisposedBy,
                    DisposedDate = data.DisposedDate,
                    DisposeReason = data.DisposeReason,
                    DisposingBranchId = data.DisposingBranchId,
                    DisposingDepartmentId = data.DisposingDepartmentId,
                    ForwardedForApproval = data.ForwardedForApproval,
                    ModidiedDate = data.ModidiedDate,
                    ModifiedBy = data.ModifiedBy,
                    RejectedBy = data.RejectedBy,
                    RejectionDate = data.RejectionDate,
                    RequestBy = data.RequestBy,
                    RequestDate = data.RequestDate,
                    Status = data.Status,
                    TotalAmount = data.TotalAmount,
                    VatAmount = data.VatAmount,
                    BranchName = CodeService.GetBranchName((int)data.DisposingBranchId),
                    DepartmentName = CodeService.GetDepartmentName((int)data.DisposingDepartmentId),
                    RequestByName = data.RequestBy == null ? " " : CodeService.GetEmployeeFullName((int)data.RequestBy),
                    ApproveByName = data.ApprovedBy == null ? " " : CodeService.GetEmployeeFullName((int)data.ApprovedBy)
                };
                return record;
            }
            return null;
        }
        public static List<InDisposeMessageVM> Convert(List<In_Dispose_Message> data)
        {
            List<InDisposeMessageVM> records = new List<InDisposeMessageVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InDisposeDetails
        public static In_Dispose_Details Convert(InDisposeDetailsVM data)
        {
            if (data != null)
            {
                In_Dispose_Details record = new In_Dispose_Details
                {
                    Id = data.Id,
                    Amount = data.Amount,
                    DisposeMessageId = data.DisposeMessageId,
                    DisposeQty = data.DisposeQty,
                    ProductId = data.ProductId,
                    Rate = data.Rate,
                    RequestedQty = data.RequestedQty
                };
                return record;
            }
            return null;
        }
        public static InDisposeDetailsVM Convert(In_Dispose_Details data)
        {
            var Session = HttpContext.Current.Session;
            var bid = (int)(Session["BranchId"]);
            if (data != null)
            {
                InDisposeDetailsVM record = new InDisposeDetailsVM
                {
                    Id = data.Id,
                    Amount = data.Amount,
                    DisposeMessageId = data.DisposeMessageId,
                    DisposeQty = data.DisposeQty,
                    ProductId = data.ProductId,
                    Rate = data.Rate,
                    RequestedQty = data.RequestedQty,
                    InDisposeMessageVM = Convert(data.In_Dispose_Message),
                    ProductName = CodeService.GetInProductName(data.ProductId),
                    StockInHand = CodeService.GetStockInHandfromPId(data.ProductId, bid),
                    Unit = CodeService.GetUnitForProductName(data.ProductId)
                };
                return record;
            }
            return null;
        }
        public static List<InDisposeDetailsVM> Convert(List<In_Dispose_Details> data)
        {
            List<InDisposeDetailsVM> records = new List<InDisposeDetailsVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region TempDispose
        public static Temp_Dispose Convert(TempDisposeVM data)
        {
            if (data != null)
            {
                Temp_Dispose record = new Temp_Dispose
                {
                    Id = data.Id,
                    DisposedMessageId = data.DisposedMessageId,
                    Amount = data.Amount,
                    ProductId = data.ProductId,
                    Qty = data.Qty,
                    Rate = data.Rate
                };
                return record;
            }
            return null;
        }
        public static TempDisposeVM Convert(Temp_Dispose data)
        {
            if (data != null)
            {
                TempDisposeVM record = new TempDisposeVM
                {
                    Id = data.Id,
                    DisposedMessageId = data.DisposedMessageId,
                    Amount = data.Amount,
                    ProductId = data.ProductId,
                    Qty = data.Qty,
                    Rate = data.Rate,
                    Unit = CodeService.GetUnitForProductName(data.ProductId),
                    SerialStatus = CodeService.GetSerialStatusForProductById(data.ProductId),
                    productname = CodeService.GetInProductName(data.ProductId)
                };
                return record;
            }
            return null;
        }
        public static List<TempDisposeVM> Convert(List<Temp_Dispose> data)
        {
            List<TempDisposeVM> records = new List<TempDisposeVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region TempDisposeOther
        public static Temp_Dispose_Other Convert(TempDisposeOtherVM data)
        {
            if (data != null)
            {
                Temp_Dispose_Other record = new Temp_Dispose_Other
                {
                    Id = data.Id,
                    TempDisposedId = data.TempDisposedId,
                    Qty = data.Qty,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to
                };
                return record;
            }
            return null;
        }
        public static TempDisposeOtherVM Convert(Temp_Dispose_Other data)
        {
            if (data != null)
            {
                TempDisposeOtherVM record = new TempDisposeOtherVM
                {
                    Id = data.Id,
                    TempDisposedId = data.TempDisposedId,
                    Qty = data.Qty,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to
                };
                return record;
            }
            return null;
        }
        public static List<TempDisposeOtherVM> Convert(List<Temp_Dispose_Other> data)
        {
            List<TempDisposeOtherVM> records = new List<TempDisposeOtherVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region TempReturnProduct
        public static Temp_Return_Product Convert(TempReturnProductVM data)
        {
            if (data != null)
            {
                Temp_Return_Product record = new Temp_Return_Product
                {
                    id = data.id,
                    qty = data.qty,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to,
                    productid = data.productid
                };
                return record;
            }
            return null;
        }
        public static TempReturnProductVM Convert(Temp_Return_Product data)
        {
            if (data != null)
            {
                TempReturnProductVM record = new TempReturnProductVM
                {
                    id = data.id,
                    qty = data.qty,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to,
                    productid = data.productid
                };
                return record;
            }
            return null;
        }
        public static List<TempReturnProductVM> Convert(List<Temp_Return_Product> data)
        {
            List<TempReturnProductVM> records = new List<TempReturnProductVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region SerialProduct_TransferBranch
        public static SerialProduct_TransferBranch Convert(SerialProductTransferBranchVM data)
        {
            if (data != null)
            {
                SerialProduct_TransferBranch record = new SerialProduct_TransferBranch
                {
                    Id = data.Id,
                    qty = data.qty,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to,
                    productid = data.productid,
                    fbranchid = data.fbranchid,
                    reqid = data.reqid,
                    tbranchid = data.tbranchid,
                    TransferDate = data.TransferDate
                };
                return record;
            }
            return null;
        }
        public static SerialProductTransferBranchVM Convert(SerialProduct_TransferBranch data)
        {
            if (data != null)
            {
                SerialProductTransferBranchVM record = new SerialProductTransferBranchVM
                {
                    Id = data.Id,
                    qty = data.qty,
                    sn_from = data.sn_from,
                    sn_to = data.sn_to,
                    productid = data.productid,
                    fbranchid = data.fbranchid,
                    reqid = data.reqid,
                    tbranchid = data.tbranchid,
                    TransferDate = data.TransferDate
                };
                return record;
            }
            return null;
        }
        public static List<SerialProductTransferBranchVM> Convert(List<SerialProduct_TransferBranch> data)
        {
            List<SerialProductTransferBranchVM> records = new List<SerialProductTransferBranchVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InStaticTempDispatch
        public static In_Static_Temp_Dispatch Convert(StaticTempDispatchVM data)
        {
            if (data != null)
            {
                In_Static_Temp_Dispatch record = new In_Static_Temp_Dispatch
                {
                    Id = data.Id,
                    ProductId = data.ProductId,
                    Qty = data.Qty,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    ProductGroupId = data.ProductGroupId
                };
                return record;
            }
            return null;
        }
        public static StaticTempDispatchVM Convert(In_Static_Temp_Dispatch data)
        {
            if (data != null)
            {
                StaticTempDispatchVM record = new StaticTempDispatchVM
                {
                    Id = data.Id,
                    ProductId = data.ProductId,
                    Qty = data.Qty,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    ProductGroupId = data.ProductGroupId,
                    ProductName = CodeService.GetInProductName((int)data.ProductId),
                    Unit = CodeService.GetUnitForProductName((int)data.ProductId),
                    Rate = CodeService.GetRateForProductId((int)data.ProductId),
                    SerialStatus = CodeService.GetSerialStatusForProductById((int)data.ProductId),
                    StockInHand = CodeService.GetStockInHandfromPId((int)data.ProductId, 12)//12==corporate 999 
                };
                return record;
            }
            return null;
        }
        public static List<StaticTempDispatchVM> Convert(List<In_Static_Temp_Dispatch> data)
        {
            List<StaticTempDispatchVM> records = new List<StaticTempDispatchVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region InStaticTempDispatchOther
        public static In_Static_Temp_Dispatch_Other Convert(StaticTempDispatchOtherVM data)
        {
            if (data != null)
            {
                In_Static_Temp_Dispatch_Other record = new In_Static_Temp_Dispatch_Other
                {
                    Id = data.Id,
                    ProductId = data.ProductId,
                    Qty = data.Qty,
                    InStaticTempDispatchId = data.InStaticTempDispatchId,
                    snf = data.snf,
                    snt = data.snt,
                };
                return record;
            }
            return null;
        }
        public static StaticTempDispatchOtherVM Convert(In_Static_Temp_Dispatch_Other data)
        {
            if (data != null)
            {
                StaticTempDispatchOtherVM record = new StaticTempDispatchOtherVM
                {
                    Id = data.Id,
                    ProductId = data.ProductId,
                    Qty = data.Qty,
                    InStaticTempDispatchId = data.InStaticTempDispatchId,
                    snf = data.snf,
                    snt = data.snt
                };
                return record;
            }
            return null;
        }
        public static List<StaticTempDispatchOtherVM> Convert(List<In_Static_Temp_Dispatch_Other> data)
        {
            List<StaticTempDispatchOtherVM> records = new List<StaticTempDispatchOtherVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #endregion

        #region FuelRequestsMessage
        public static Fuel_Requests_Message Convert(FuelRequestsMessageVM data)
        {
            if (data != null)
            {
                Fuel_Requests_Message record = new Fuel_Requests_Message
                {
                    Id = data.Id,
                    FuelRequestNo = data.FuelRequestNo,
                    Requested_By = data.Requested_By,
                    Requested_Date = data.Requested_Date,
                    Requested_Message = data.Requested_Message,
                    Recommended_By = data.Recommended_By,
                    Recommended_Date = data.Recommended_Date,
                    Recommended_Message = data.Recommended_Message,
                    Approved_By = data.Approved_By,
                    Approved_Date = data.Approved_Date,
                    Approved_Message = data.Approved_Message,
                    Rejected_By = data.Rejected_By,
                    Rejected_Date = data.Rejected_Date,
                    Rejected_Message = data.Rejected_Message,
                    Priority = data.Priority,
                    Status = data.Status,
                    Branch_id = data.Branch_id,
                };
                return record;
            }
            return null;
        }
        public static FuelRequestsMessageVM Convert(Fuel_Requests_Message data)
        {
            if (data != null)
            {
                FuelRequestsMessageVM record = new FuelRequestsMessageVM
                {
                    Id = data.Id,
                    FuelRequestNo = data.FuelRequestNo,
                    Requested_By = data.Requested_By,
                    Requested_Date = data.Requested_Date,
                    Requested_Message = data.Requested_Message,
                    Recommended_By = data.Recommended_By,
                    Recommended_Date = data.Recommended_Date,
                    Recommended_Message = data.Recommended_Message,
                    Approved_By = data.Approved_By,
                    Approved_Date = data.Approved_Date,
                    Approved_Message = data.Approved_Message,
                    Rejected_By = data.Rejected_By,
                    Rejected_Date = data.Rejected_Date,
                    Rejected_Message = data.Rejected_Message,
                    Priority = data.Priority,
                    Status = data.Status,
                    Branch_id = data.Branch_id,
                    BranchName = CodeService.GetBranchName(data.Branch_id),
                    RequestedBy = CodeService.GetEmployeeFullName(data.Requested_By),
                    RecommendedBy = CodeService.GetEmployeeFullName(data.Recommended_By),
                    ApprovedBy = data.Approved_By == null ? " " : CodeService.GetEmployeeFullName((int)data.Approved_By),
                    
                };
                return record;
            }
            return null;
        }
        public static List<FuelRequestsMessageVM> Convert(List<Fuel_Requests_Message> data)
        {
            List<FuelRequestsMessageVM> records = new List<FuelRequestsMessageVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region FuelRequests
        public static Fuel_Requests Convert(FuelRequestsVM data)
        {
            if (data != null)
            {
                Fuel_Requests record = new Fuel_Requests
                {
                    Id = data.Id,
                    Unit = data.Unit,
                    Vehicle_Category = data.Vehicle_Category,
                    Fuel_Category = data.Fuel_Category,
                    Vendor = data.Vendor,
                    Vehicle_No = data.Vehicle_No,
                    KM_Run = data.KM_Run,
                    Fuel_Requests_Message_Id = data.Fuel_Requests_Message_Id,
                    Requested_Quantity = data.Requested_Quantity,
                    Recommended_Quantity = data.Recommended_Quantity,
                    Approved_Quantity = data.Approved_Quantity,
                    Received_Quantity = data.Received_Quantity,
                    Coupon_No = data.Coupon_No,
                    Previous_KM_Run = data.Previous_KM_Run,
                    FilePath = data.FilePath,   
                };
                return record;
            }
            return null;
        }
        public static FuelRequestsVM Convert(Fuel_Requests data)
        {

            if (data != null)
            {
                FuelRequestsVM record = new FuelRequestsVM
                {
                    Id = data.Id,
                    Unit = data.Unit,
                    Vehicle_Category = data.Vehicle_Category,
                    Fuel_Category = data.Fuel_Category,
                    Vendor = data.Vendor,
                    Vehicle_No = data.Vehicle_No,
                    KM_Run = data.KM_Run,
                    Fuel_Requests_Message_Id = data.Fuel_Requests_Message_Id,
                    Requested_Quantity = data.Requested_Quantity,
                    Recommended_Quantity = data.Recommended_Quantity,
                    Approved_Quantity = data.Approved_Quantity,
                    Received_Quantity = data.Received_Quantity,
                    Coupon_No = data.Coupon_No,
                    Previous_KM_Run = data.Previous_KM_Run,
                    FilePath = data.FilePath,
                };
                return record;
            }
            return null;
        }
        public static List<FuelRequestsVM> Convert(List<Fuel_Requests> data)
        {
            List<FuelRequestsVM> records = new List<FuelRequestsVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #endregion

        #region Fixed Asset System

        #region Assets Parameter
        #region AssetItem
        public static ASSET_ITEM Convert(AssetItemVM data)
        {
            if (data != null)
            {
                ASSET_ITEM record = new ASSET_ITEM
                {
                    id = data.id,
                    item_name = data.item_name,
                    item_desc = data.item_desc,
                    Product_Code = data.Product_Code,
                    parent_id = data.parent_id,
                    is_product = data.is_product,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    Is_Active = data.Is_Active,
                    depre_pct = data.depre_pct
                };
                return record;
            }
            return null;
        }
        public static AssetItemVM Convert(ASSET_ITEM data)
        {
            if (data != null)
            {
                AssetItemVM record = new AssetItemVM
                {
                    id = data.id,
                    item_name = data.item_name,
                    item_desc = data.item_desc,
                    Product_Code = data.Product_Code,
                    parent_id = data.parent_id,
                    is_product = data.is_product,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    Is_Active = data.Is_Active,
                    depre_pct = data.depre_pct

                };
                return record;
            }
            return null;
        }
        public static List<AssetItemVM> Convert(List<ASSET_ITEM> data)
        {
            List<AssetItemVM> records = new List<AssetItemVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region AssetProduct
        public static ASSET_PRODUCT Convert(AssetProductVM data)
        {
            if (data != null)
            {
                ASSET_PRODUCT record = new ASSET_PRODUCT
                {
                    id = data.id,
                    porduct_code = data.porduct_code,
                    product_desc = data.product_desc,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    item_id = data.item_id,
                    ASSET_CODE = data.ASSET_CODE,
                    Is_Active = data.Is_Active
                };
                return record;
            }
            return null;
        }
        public static AssetProductVM Convert(ASSET_PRODUCT data)
        {
            if (data != null)
            {
                AssetProductVM record = new AssetProductVM
                {
                    id = data.id,
                    porduct_code = data.porduct_code,
                    product_desc = data.product_desc,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    item_id = data.item_id,
                    ASSET_CODE = data.ASSET_CODE,
                    Is_Active = data.Is_Active,
                    AssetItems = Convert(data.ASSET_ITEM)
                };
                return record;
            }
            return null;
        }
        public static List<AssetProductVM> Convert(List<ASSET_PRODUCT> data)
        {
            List<AssetProductVM> records = new List<AssetProductVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region AssetBranch
        public static ASSET_BRANCH Convert(AssetBranchVM data)
        {
            if (data != null)
            {
                ASSET_BRANCH record = new ASSET_BRANCH
                {
                    ID = data.ID,
                    ACCUMULATED_DEP_AC = data.ACCUMULATED_DEP_AC,
                    ASSET_AC = data.ASSET_AC,
                    ASSET_NEXT_NUM = data.ASSET_NEXT_NUM,
                    BRANCH_ID = data.BRANCH_ID,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    DEPRECIATION_EXP_AC = data.DEPRECIATION_EXP_AC,
                    IS_ACTIVE = data.IS_ACTIVE,
                    MAINTAINANCE_EXP_AC = data.MAINTAINANCE_EXP_AC,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    PRODUCT_ID = data.PRODUCT_ID,
                    SALES_PROFIT_LOSS_AC = data.SALES_PROFIT_LOSS_AC,
                    WRITE_OFF_EXP_AC = data.WRITE_OFF_EXP_AC
                };
                return record;
            }
            return null;
        }
        public static AssetBranchVM Convert(ASSET_BRANCH data)
        {
            if (data != null)
            {
                AssetBranchVM record = new AssetBranchVM
                {
                    ID = data.ID,
                    ACCUMULATED_DEP_AC = data.ACCUMULATED_DEP_AC,
                    ASSET_AC = data.ASSET_AC,
                    ASSET_NEXT_NUM = data.ASSET_NEXT_NUM,
                    BRANCH_ID = data.BRANCH_ID,
                    CREATED_BY = data.CREATED_BY,
                    CREATED_DATE = data.CREATED_DATE,
                    DEPRECIATION_EXP_AC = data.DEPRECIATION_EXP_AC,
                    IS_ACTIVE = data.IS_ACTIVE,
                    MAINTAINANCE_EXP_AC = data.MAINTAINANCE_EXP_AC,
                    MODIFIED_BY = data.MODIFIED_BY,
                    MODIFIED_DATE = data.MODIFIED_DATE,
                    PRODUCT_ID = data.PRODUCT_ID,
                    SALES_PROFIT_LOSS_AC = data.SALES_PROFIT_LOSS_AC,
                    WRITE_OFF_EXP_AC = data.WRITE_OFF_EXP_AC,
                    BranchName = CodeService.GetBranchName(data.BRANCH_ID),
                    ProductName = CodeService.GetAssetProductName(data.PRODUCT_ID)
                };
                return record;
            }
            return null;
        }
        public static List<AssetBranchVM> Convert(List<ASSET_BRANCH> data)
        {
            List<AssetBranchVM> records = new List<AssetBranchVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #region AssetNumberSequence
        public static ASSET_NumberSequence Convert(AssetNumberSequenceVM data)
        {
            if (data != null)
            {
                ASSET_NumberSequence record = new ASSET_NumberSequence
                {
                    ID = data.ID,
                    AssetSeparator = data.AssetSeparator,
                    BranchSeparator = data.BranchSeparator,
                    CompSeparator = data.CompSeparator,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    IsAssetCode = data.IsAssetCode,
                    IsAssetCodeSep = data.IsAssetCodeSep,
                    IsBranchCode = data.IsBranchCode,
                    IsBranchSep = data.IsBranchSep,
                    IsCompSep = data.IsCompSep,
                    IsCompShortCode = data.IsCompShortCode,
                    IsDateCode = data.IsDateCode,
                    IsSequenceSep = data.IsSequenceSep,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                    NumSequence = data.NumSequence,
                    SeqDateFormat = data.SeqDateFormat,
                    SequenceSep = data.SequenceSep
                };
                return record;
            }
            return null;
        }
        public static AssetNumberSequenceVM Convert(ASSET_NumberSequence data)
        {
            if (data != null)
            {
                AssetNumberSequenceVM record = new AssetNumberSequenceVM
                {
                    ID = data.ID,
                    AssetSeparator = data.AssetSeparator,
                    BranchSeparator = data.BranchSeparator,
                    CompSeparator = data.CompSeparator,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    IsAssetCode = data.IsAssetCode,
                    IsAssetCodeSep = data.IsAssetCodeSep,
                    IsBranchCode = data.IsBranchCode,
                    IsBranchSep = data.IsBranchSep,
                    IsCompSep = data.IsCompSep,
                    IsCompShortCode = data.IsCompShortCode,
                    IsDateCode = data.IsDateCode,
                    IsSequenceSep = data.IsSequenceSep,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                    NumSequence = data.NumSequence,
                    SeqDateFormat = data.SeqDateFormat,
                    SequenceSep = data.SequenceSep
                };
                return record;
            }
            return null;
        }
        public static List<AssetNumberSequenceVM> Convert(List<ASSET_NumberSequence> data)
        {
            List<AssetNumberSequenceVM> records = new List<AssetNumberSequenceVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #endregion

        #region AssetAquisition
        #region AssetRequisitionMessage
        public static ASSET_REQUISITION_MESSAGE Convert(AssetRequisitionMessageVM data)
        {
            if (data != null)
            {
                ASSET_REQUISITION_MESSAGE record = new ASSET_REQUISITION_MESSAGE
                {
                    id = data.id,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    approval_message = data.approval_message,
                    approved_by = data.approved_by,
                    approved_date = data.approved_date,
                    branch_id = data.branch_id,
                    dept_id = data.dept_id,
                    forwarded_branch = data.forwarded_branch,
                    narration = data.narration,
                    priority = data.priority,
                    status = data.status
                };
                return record;
            }
            return null;
        }
        public static AssetRequisitionMessageVM Convert(ASSET_REQUISITION_MESSAGE data)
        {
            if (data != null)
            {
                AssetRequisitionMessageVM record = new AssetRequisitionMessageVM
                {
                    id = data.id,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    approval_message = data.approval_message,
                    approved_by = data.approved_by,
                    approved_date = data.approved_date,
                    branch_id = data.branch_id,
                    dept_id = data.dept_id,
                    forwarded_branch = data.forwarded_branch,
                    narration = data.narration,
                    priority = data.priority,
                    status = data.status,
                    BranchName = CodeService.GetBranchName(data.forwarded_branch),
                    ForwardedTo = CodeService.GetEmployeeFullName((int)data.approved_by)
                };
                return record;
            }
            return null;
        }
        public static List<AssetRequisitionMessageVM> Convert(List<ASSET_REQUISITION_MESSAGE> data)
        {
            List<AssetRequisitionMessageVM> records = new List<AssetRequisitionMessageVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion

        #region AssetRequisition
        public static ASSET_REQUISITION Convert(AssetRequisitionVM data)
        {
            if (data != null)
            {
                ASSET_REQUISITION record = new ASSET_REQUISITION
                {
                    id = data.id,
                    approved_qty = data.approved_qty,
                    asset_id = data.asset_id,
                    order_qty = data.order_qty,
                    price = data.price,
                    qty = data.qty,
                    received_qty = data.received_qty,
                    requistion_message_id = data.requistion_message_id
                };
                return record;
            }
            return null;
        }
        public static AssetRequisitionVM Convert(ASSET_REQUISITION data)
        {
            if (data != null)
            {
                AssetRequisitionVM record = new AssetRequisitionVM
                {
                    id = data.id,
                    approved_qty = data.approved_qty,
                    asset_id = data.asset_id,
                    order_qty = data.order_qty,
                    price = data.price,
                    qty = data.qty,
                    received_qty = data.received_qty,
                    requistion_message_id = data.requistion_message_id,
                    assettype = CodeService.GetAssetTypeName(data.asset_id),
                    AssetRequisitionMessage = Convert(data.ASSET_REQUISITION_MESSAGE)
                };
                return record;
            }
            return null;
        }
        public static List<AssetRequisitionVM> Convert(List<ASSET_REQUISITION> data)
        {
            List<AssetRequisitionVM> records = new List<AssetRequisitionVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #endregion

        #region AssetManagement
        #region AssetInventoryTemp
        public static ASSET_INVENTORY_TEMP Convert(AssetInventoryTempVM data)
        {
            if (data != null)
            {
                ASSET_INVENTORY_TEMP record = new ASSET_INVENTORY_TEMP
                {
                    id = data.id,
                    acc_depriciation = data.acc_depriciation,
                    asset_holder = data.asset_holder,
                    asset_number = data.asset_number,
                    asset_serial = data.asset_serial,
                    asset_status = data.asset_status,
                    asset_type = data.asset_type,
                    bill_id = data.bill_id,
                    booked_date = data.booked_date,
                    branch_id = data.branch_id,
                    brand = data.brand,
                    depr_start_date = data.depr_start_date,
                    dept_id = data.dept_id,
                    forwarded_by = data.forwarded_by,
                    forwarded_date = data.forwarded_date,
                    group_id = data.group_id,
                    ins_id = data.ins_id,
                    in_out = data.in_out,
                    IsActive = data.IsActive,
                    is_amortised = data.is_amortised,
                    life_in_month = data.life_in_month,
                    model = data.model,
                    narration = data.narration,
                    next_maintenance_date = data.next_maintenance_date,
                    old_asset_code = data.old_asset_code,
                    old_asset_no = data.old_asset_no,
                    product_id = data.product_id,
                    purchase_date = data.purchase_date,
                    purchase_value = data.purchase_value,
                    receive_id = data.receive_id,
                    rejection_reason = data.rejection_reason,
                    remain_month = data.remain_month,
                    status = data.status,
                    VendorId = data.VendorId,
                    warr_expiry = data.warr_expiry,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    depre_pct = data.depre_pct
                };
                return record;
            }
            return null;
        }
        public static AssetInventoryTempVM Convert(ASSET_INVENTORY_TEMP data)
        {
            if (data != null)
            {
                AssetInventoryTempVM record = new AssetInventoryTempVM
                {
                    id = data.id,
                    acc_depriciation = data.acc_depriciation,
                    asset_holder = data.asset_holder,
                    asset_number = data.asset_number,
                    asset_serial = data.asset_serial,
                    asset_status = data.asset_status,
                    asset_type = data.asset_type,
                    bill_id = data.bill_id,
                    booked_date = data.booked_date,
                    branch_id = data.branch_id,
                    brand = data.brand,
                    depr_start_date = data.depr_start_date,
                    dept_id = data.dept_id,
                    forwarded_by = data.forwarded_by,
                    forwarded_date = data.forwarded_date,
                    group_id = data.group_id,
                    ins_id = data.ins_id,
                    in_out = data.in_out,
                    IsActive = data.IsActive,
                    is_amortised = data.is_amortised,
                    life_in_month = data.life_in_month,
                    model = data.model,
                    narration = data.narration,
                    next_maintenance_date = data.next_maintenance_date,
                    old_asset_code = data.old_asset_code,
                    old_asset_no = data.old_asset_no,
                    product_id = data.product_id,
                    purchase_date = data.purchase_date,
                    purchase_value = data.purchase_value,
                    receive_id = data.receive_id,
                    rejection_reason = data.rejection_reason,
                    remain_month = data.remain_month,
                    status = data.status,
                    VendorId = data.VendorId,
                    warr_expiry = data.warr_expiry,
                    created_by = data.created_by,
                    created_date = data.created_date,
                    modified_by = data.modified_by,
                    modified_date = data.modified_date,
                    depre_pct = data.depre_pct,
                };
                return record;
            }
            return null;
        }
        public static List<AssetInventoryTempVM> Convert(List<ASSET_INVENTORY_TEMP> data)
        {
            List<AssetInventoryTempVM> records = new List<AssetInventoryTempVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }

        #endregion
        #region ChangesApprovalQueue
        public static changesApprovalQueue Convert(changesApprovalQueueVM data)
        {
            if (data != null)
            {
                changesApprovalQueue record = new changesApprovalQueue
                {
                    rowId = data.rowId,
                    dataid = data.dataid,
                    AddEditfunctionId = data.AddEditfunctionId,
                    approveFlag = data.approveFlag,
                    changeStatus = data.changeStatus,
                    createdBy = data.createdBy,
                    createdDate = data.createdDate,
                    description = data.description,
                    editLink = data.editLink,
                    forwarded_date = data.forwarded_date,
                    forwarded_to = data.forwarded_to,
                    functionId = data.functionId,
                    identifierField = data.identifierField,
                    IsActive = data.IsActive,
                    modType = data.modType,
                    module = data.module,
                    reasonForRejection = data.reasonForRejection,
                    tableDescription = data.tableDescription,
                    tableName = data.tableName
                };
                return record;
            }
            return null;
        }
        public static changesApprovalQueueVM Convert(changesApprovalQueue data)
        {
            if (data != null)
            {
                changesApprovalQueueVM record = new changesApprovalQueueVM
                {
                    rowId = data.rowId,
                    dataid = data.dataid,
                    AddEditfunctionId = data.AddEditfunctionId,
                    approveFlag = data.approveFlag,
                    changeStatus = data.changeStatus,
                    createdBy = data.createdBy,
                    createdDate = data.createdDate,
                    description = data.description,
                    editLink = data.editLink,
                    forwarded_date = data.forwarded_date,
                    forwarded_to = data.forwarded_to,
                    functionId = data.functionId,
                    identifierField = data.identifierField,
                    IsActive = data.IsActive,
                    modType = data.modType,
                    module = data.module,
                    reasonForRejection = data.reasonForRejection,
                    tableDescription = data.tableDescription,
                    tableName = data.tableName,
                    AssetInventoryTemp = Convert(data.ASSET_INVENTORY_TEMP)
                };
                return record;
            }
            return null;
        }
        public static List<changesApprovalQueueVM> Convert(List<changesApprovalQueue> data)
        {
            List<changesApprovalQueueVM> records = new List<changesApprovalQueueVM>();
            foreach (var item in data)
            {
                records.Add(Convert(item));
            }
            return records;
        }
        #endregion
        #endregion

        #endregion
    }
}
