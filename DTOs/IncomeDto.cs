using System;

namespace CostRegApp2.DTOs
{
    public class IncomeDto
    {
        public int IncomeID { get; set; }
        public string TypeOfIncome { get; set; }
        public int AmountOfIncome { get; set; }
        public DateTime DateOFIncome { get; set; }
        public string IncomeAdditionalInformation { get; set; }
    }
}
