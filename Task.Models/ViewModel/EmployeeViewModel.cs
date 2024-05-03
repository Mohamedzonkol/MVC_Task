using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Task.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> UserList { get; set; }
    }
}
