using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain
{
  public  class AdminVM
    {
        public int AdminID { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string UserPassword { get; set; }

        [StringLength(50)]
        public string Post { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Cell_phone { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        //[Key]
        //[Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Name { get; set; }

        [StringLength(10)]
        public string agent_id { get; set; }

        public bool status { get; set; }

        [StringLength(50)]
        public string LOGINTIMEFROM { get; set; }

        [StringLength(50)]
        public string LOGINTIMETO { get; set; }

        [StringLength(50)]
        public string REPORTACCESS { get; set; }

        [StringLength(100)]
        public string session { get; set; }

        [StringLength(100)]
        public string user_type { get; set; }

        [StringLength(2)]
        public string new_user { get; set; }

        [StringLength(50)]
        public string created_by { get; set; }

        public DateTime? created_date { get; set; }

        [StringLength(50)]
        public string modified_by { get; set; }

        public DateTime? modified_date { get; set; }

        [StringLength(50)]
        public string approved_by { get; set; }

        public DateTime? approved_date { get; set; }

        [StringLength(5)]
        public string branch_level_access { get; set; }

        public DateTime? LastLogin { get; set; }

        public int? pwdChangeDays { get; set; }

        public int? PwdChangeWaringDays { get; set; }

        public DateTime? lastPwdChangedOn { get; set; }

        [StringLength(5)]
        public string forceChangePwd { get; set; }
        public bool IsTemporary { get; set; }
        public virtual EmployeeVM Employees { get; set; }
    }
}
