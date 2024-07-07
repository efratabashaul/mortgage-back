using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Leads
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }


    }
}
