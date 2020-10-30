using System.ComponentModel.DataAnnotations;

namespace CostRegApp2.Data
{
    public class UserSetting
    {
        [Key]
        public int Id { get; set; }
        public int SettingId { get; set; }
        public Setting Setting { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public string Value { get; set; }
    }
}
