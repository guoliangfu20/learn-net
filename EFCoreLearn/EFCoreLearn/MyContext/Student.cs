using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreLearn.MyContext
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)"), MaxLength(50)]
        public string Name { get; set; }
        public bool Gender { get; set; }
    }
}
