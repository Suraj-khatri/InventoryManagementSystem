using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InventoryManagementSystem.Domain
{
    public class InterBranchTransferVM
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "From Branch")]
        public int FromBranchId { get; set; }
        [Required]
        [Display(Name = "To Branch")]
        public int ToBranchId { get; set; }
        [Required]
        [Display(Name = "Group")]
        public int ProdGroupId { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        [Display(Name = "Send From")]
        [Required]
        public int SenderFrom { get; set; }
        [Required]
        [Display(Name = "Send To")]
        public int SenderTo { get; set; }
        [Required]
        public string Narration { get; set; }
        [Required]
        [Display(Name = "Department")]
        public int ToDeptId { get; set; }
        [Required]
        [Display(Name = "Department")]
        public int FromDeptId { get; set; }
        [Display(Name = "Rate")]
        public decimal Rate { get; set; }

        public List<SelectListItem> BranchList { get; set; }
        public List<SelectListItem> ProductGroupList { get; set; }
        public List<SelectListItem> ProductList { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
    }
}
