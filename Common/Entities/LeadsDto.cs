using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class LeadsDto
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
