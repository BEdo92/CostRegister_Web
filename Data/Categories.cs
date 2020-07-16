using System.ComponentModel.DataAnnotations;

namespace CostRegApp2.Data
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
