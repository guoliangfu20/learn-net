using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreSqlLearn.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string OrderName { get; set; }
        [MaxLength(120)]
        public string OrderAddress { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Total { get; set; }
    }
}
