using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcKnockoutCalendar.Models
{
    public class MvcKnockoutCalendarContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MvcKnockoutCalendar.Models.MvcKnockoutCalendarContext>());

        public MvcKnockoutCalendarContext():
            base("MvcKoCalendarDB")
        {

        }

        public DbSet<MvcKnockoutCalendar.Models.TaskDay> TaskDays { get; set; }

        public DbSet<MvcKnockoutCalendar.Models.TaskDetail> TaskDetails { get; set; }
    }
}