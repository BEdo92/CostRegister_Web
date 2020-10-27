using System.ComponentModel.DataAnnotations;

namespace CostRegApp2.Data
{
    public class Settings
    {
        [Key]
        public int SettingId { get; set; }
        public bool PlansShownInBalance { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
