using System;
using System.ComponentModel.DataAnnotations;

namespace CostRegApp2.Data
{
    public class Costs
    {
        [Key]
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int ShopID { get; set; }
        [Required]
        public int AmountOfCost { get; set; }
        [Required]
        public DateTime DateOfCost { get; set; }
        [MaxLength(100)]
        public string AdditionalInformation { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}