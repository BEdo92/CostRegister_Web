using System;
using System.ComponentModel.DataAnnotations;

namespace CostRegApp2.Data
{
    public class Income
    {
        [Key]
        public int IncomeID { get; set; }
        [Required]
        [MaxLength(100)]
        public string TypeOfIncome { get; set; }
        [Required]
        public int AmountOfIncome { get; set; }
        [Required]
        public DateTime DateOFIncome { get; set; }
        [MaxLength(100)]
        public string IncomeAdditionalInformation { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}