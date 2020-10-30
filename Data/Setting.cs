using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostRegApp2.Data
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserSetting> UserSetting { get; set; }
    }
}
