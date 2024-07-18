using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Notification
    {
            // מזהה ייחודי לכל רשומה
            public int ID { get; set; }

            // מזהה משתמש הקשור לטבלה אחרת
            public int UserId { get; set; }

            // הודעה
            public string Message { get; set; }

            // משתנה בוליאני לציון אם ההודעה נקראה או לא
            public bool IsRead { get; set; }

            // תאריך ושעת יצירה
            public DateTime CreatedAt { get; set; }
        }
    }
