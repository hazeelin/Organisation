using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organisation.Api.Models
{
    public class Department
    {
        [Key]
        [Column("department_id")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column("department_name")]
        public string DepartmentName { get; set; }
    }
}
