using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MvcKnockoutCalendar.Models
{
    public class TaskDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        [ForeignKey("ParentTaskId")]
        [ScriptIgnore]
        public TaskDay ParentTask { get; set; }
        public int ParentTaskId { get; set; }
    }
}