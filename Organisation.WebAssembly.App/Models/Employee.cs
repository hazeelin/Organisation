using System.ComponentModel.DataAnnotations;

namespace Organisation.WebAssembly.App.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        public char Gender { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public decimal Salary { get; set; }
    }
}
