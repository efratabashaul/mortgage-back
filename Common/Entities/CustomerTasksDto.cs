using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class CustomerTasksDto
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public string Task_description { get; set; }
        public int Document_type_id { get; set; }
        public string? Document_path { get; set; }
        public Status status { get; set; }
        public DateTime? Due_date { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}
