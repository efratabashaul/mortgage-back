using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Notification
    {
            public int ID { get; set; }
            public int UserId { get; set; }
            public string Message { get; set; }
            public bool IsRead { get; set; }=false;
            public DateTime created_at { get; set; }
        }
    }
