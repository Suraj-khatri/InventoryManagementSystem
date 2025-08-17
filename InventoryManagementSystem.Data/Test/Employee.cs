namespace InventoryManagementSystem.Data.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Admins = new HashSet<Admins>();
            emp_log = new HashSet<emp_log>();
            Employee_Contract = new HashSet<Employee_Contract>();
            NotificationUser = new HashSet<NotificationUser>();
        }

        [Key]
        public int EMPLOYEE_ID { get; set; }

        [Required]
        [StringLength(30)]
        public string EMP_CODE { get; set; }

        [Required]
        [StringLength(50)]
        public string SALUTATION { get; set; }

        [Required]
        [StringLength(200)]
        public string FIRST_NAME { get; set; }

        [StringLength(200)]
        public string MIDDLE_NAME { get; set; }

        [Required]
        [StringLength(200)]
        public string LAST_NAME { get; set; }

        [StringLength(50)]
        public string OFFICE_PHONE { get; set; }

        [StringLength(50)]
        public string HOME_PHONE { get; set; }

        [StringLength(50)]
        public string OFFICE_MOBILE { get; set; }

        [StringLength(50)]
        public string PERSONAL_MOBILE { get; set; }

        [StringLength(50)]
        public string OFFICE_FAX { get; set; }

        [StringLength(50)]
        public string PERSONAL_FAX { get; set; }

        [StringLength(500)]
        public string OFFICIAL_EMAIL { get; set; }

        [StringLength(200)]
        public string PERSONAL_EMAIL { get; set; }

        [Required]
        [StringLength(8)]
        public string GENDER { get; set; }

        public int DEPARTMENT_ID { get; set; }

        public int BRANCH_ID { get; set; }

        public int POSITION_ID { get; set; }

        [StringLength(50)]
        public string BLOOD_GROUP { get; set; }

        [StringLength(100)]
        public string NATIONALITY { get; set; }

        [StringLength(100)]
        public string DRIVARY_LICENCE_NUMBER { get; set; }

        [StringLength(100)]
        public string PASSPORT_NUMBER { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BIRTH_DATE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JOINED_DATE { get; set; }

        [StringLength(50)]
        public string MERITAL_STATUS { get; set; }

        [StringLength(50)]
        public string PAN_NUMBER { get; set; }

        [StringLength(20)]
        public string MAP_CODE { get; set; }

        [StringLength(100)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(100)]
        public string MODIFIED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }

        [StringLength(100)]
        public string TEMP_STREET_NAME { get; set; }

        public int? TEMP_WARD_NO { get; set; }

        [StringLength(50)]
        public string TEMP_HOUSE_NO { get; set; }

        [StringLength(200)]
        public string TEMP_MUNICIPALITY_VDC { get; set; }

        [StringLength(100)]
        public string TEMP_DISTRICT { get; set; }

        [StringLength(100)]
        public string TEMP_PROVINCE { get; set; }

        [StringLength(100)]
        public string TEMP_COUNTRY { get; set; }

        [StringLength(100)]
        public string PER_STREET_NAME { get; set; }

        public int? PER_WARD_NO { get; set; }

        [StringLength(50)]
        public string PER_HOUSE_NO { get; set; }

        [StringLength(200)]
        public string PER_MUNICIPALITY_VDC { get; set; }

        [StringLength(100)]
        public string PER_DISTRICT { get; set; }

        [StringLength(100)]
        public string PER_PROVINCE { get; set; }

        [StringLength(100)]
        public string PER_COUNTRY { get; set; }

        [StringLength(50)]
        public string AVAAILED_VEHICLE_FACILITY { get; set; }

        [StringLength(50)]
        public string AVAILED_HOUSE_RENT_FACILITY { get; set; }

        [StringLength(50)]
        public string IS_PENSION_HOLDER { get; set; }

        [StringLength(50)]
        public string IS_DISABLED { get; set; }

        public float? PENSION_AMOUNT { get; set; }

        public int? DISABLED_ID { get; set; }

        [StringLength(50)]
        public string EMP_STATUS { get; set; }

        [StringLength(50)]
        public string EMP_TYPE { get; set; }

        [StringLength(10)]
        public string MARITAL_STATUS { get; set; }

        [Column(TypeName = "date")]
        public DateTime? APPOINTMENT_DATE { get; set; }

        [StringLength(200)]
        public string EM_NAME { get; set; }

        [StringLength(1000)]
        public string EM_ADDRESS { get; set; }

        [StringLength(100)]
        public string EM_RELATIONSHIP { get; set; }

        [StringLength(50)]
        public string EM_CONTACTNO1 { get; set; }

        [StringLength(50)]
        public string EM_CONTACTNO2 { get; set; }

        [StringLength(50)]
        public string EM_CONTACTNO3 { get; set; }

        [StringLength(100)]
        public string EM_EMAIL { get; set; }

        public int? EXTENSION_NUMBER { get; set; }

        public int? WORKING_MONTH { get; set; }

        public int? FUNCTIONAL_TITLE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PERMANENT_DATE { get; set; }

        [StringLength(50)]
        public string SYSTEM_ID { get; set; }

        [StringLength(10)]
        public string Individual_Profile_update { get; set; }

        [Column(TypeName = "date")]
        public DateTime? C_START_DATE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? C_END_DATE { get; set; }

        public string CARD_NUMBER { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GRATUITY_EFFECTIVE_DATE { get; set; }

        public string EDUCATION_DETAILS { get; set; }

        public string OUTSOURCE_COMPANY { get; set; }

        [StringLength(200)]
        public string ATT_CARD_ID { get; set; }

        public int? Salary_Title { get; set; }

        [StringLength(1)]
        public string Rl_group { get; set; }

        public double? GRADE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LASTPROMOTED { get; set; }

        [StringLength(1)]
        public string birthdayFlag { get; set; }

        [StringLength(50)]
        public string temp_STATE { get; set; }

        [StringLength(50)]
        public string per_state { get; set; }

        [StringLength(50)]
        public string branch_state { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admins> Admins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<emp_log> emp_log { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee_Contract> Employee_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NotificationUser> NotificationUser { get; set; }
    }
}
