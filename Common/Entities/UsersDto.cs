using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Entities
{
    public class UsersDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime? Created_at { get; set; } = DateTime.Now;
        public DateTime? Updated_at { get; set; }= DateTime.Now;
    }
}
