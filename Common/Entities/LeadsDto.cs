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
        public DateTime? Created_at { get; set; } = DateTime.Now;
        public DateTime? Updated_at { get; set; }= DateTime.Now;
        public override string ToString()
        {
            return "id:" + this.Id + ". name:" + this.First_Name + ". phone:" + this.Phone + ". Email:" + this.Email;
        }
    }
}
