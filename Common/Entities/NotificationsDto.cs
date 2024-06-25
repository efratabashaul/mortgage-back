using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class NotificationsDto
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public string Message { get; set; }
        public bool? Is_read { get; set; }
        public DateTime? created_at { get; set; }
    }
}
