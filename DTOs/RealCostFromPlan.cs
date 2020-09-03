using System;

namespace CostRegApp2.DTOs
{
    public class RealCostFromPlan
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string Title { get; set; }
        public DateTime DatePlanned { get; set; }
        public int Cost { get; set; }
        public string AdditionalInformation { get; set; }
        public string CategoryName { get; set; }
    }
}
