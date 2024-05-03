using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required, DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required, DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required, DisplayName("Hire Date")]
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        [DisplayName("Profile Image")]
        public string? ProfileImage { get; set; }

        public int DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        [ValidateNever]
        public Department Department { get; set; }

        public int CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        [ValidateNever]
        public User CreatedByUser { get; set; }

        public int ModifiedBy { get; set; } = 7;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
