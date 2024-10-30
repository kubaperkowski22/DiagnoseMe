using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagnoseMe.Models
{
    public class Notification
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public Notification(string title, DateTime dateTime)
        {
            Title = title;
            Date = dateTime;
        }
        
    }
}
