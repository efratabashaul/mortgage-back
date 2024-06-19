using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Notifications
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public int Message { get; set; }
        public bool? Is_read { get; set; }
        public DateTime? created_at { get; set; }


        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
    }
}
