using System;

namespace CostRegApp2.DTOs
{
    public class CostDto
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public string ShopName { get; set; }
        public int AmountOfCost { get; set; }
        public DateTime DateOfCost { get; set; }
        public string AdditionalInformation { get; set; }

    }
}
