using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organisation.Api.Models.Dtos
{
    public class EmployeeDto
    {
        [Key]
        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        public char Gender { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }

        [Required]
        [Column("department_id")]
        public int DepartmentId { get; set; }

        public decimal Salary { get; set; }
    }
}
