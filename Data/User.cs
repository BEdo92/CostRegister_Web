using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CostRegApp2.Data
{
    public class User
    {
        [Key]        
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Created { get; set; }
        public string Email { get; set; }
        public ICollection<Costs> Costs { get; set; }
        public ICollection<Income> Income { get; set; }
        public ICollection<CostPlans> CostPlans { get; set; }
    }
}
