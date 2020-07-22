using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.DTOs
{
    public class CostPlansDto
    {
        public int ID { get; set; }
        public string TypeOfCostPlan { get; set; }
        public string CategoryName { get; set; }
        public int CostPlanned { get; set; }
        public DateTime DateOfPlan { get; set; }
        public string PlanAdditionalInformation { get; set; }
    }
}
