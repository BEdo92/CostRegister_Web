using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostRegApp2.Data
{
    public class CostPlans
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeOfCostPlan { get; set; }

        public int CategoryID { get; set; }

        [Required]
        public int CostPlanned { get; set; }

        [Required]
        public DateTime DateOfPlan { get; set; }

        [MaxLength(100)]
        public string PlanAdditionalInformation { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Categories Category { get; set; }
    }
}