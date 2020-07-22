using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostRegApp2.Data
{
    public class Shops
    {
        [Key]
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public ICollection<Costs> Costs { get; set; }
    }
}
