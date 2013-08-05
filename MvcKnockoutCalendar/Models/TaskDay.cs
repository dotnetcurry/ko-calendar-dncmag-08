using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcKnockoutCalendar.Models
{
    public class TaskDay
    {
        public TaskDay()
        {
            Tasks = new List<TaskDetail>();
        }
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public List<TaskDetail> Tasks { get; set; }
    }
}