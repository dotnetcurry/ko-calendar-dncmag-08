using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcKnockoutCalendar.Models
{ 
    public class TaskDayRepository : ITaskDayRepository
    {
        MvcKnockoutCalendarContext context = new MvcKnockoutCalendarContext();

        public IQueryable<TaskDay> All
        {
            get { return context.TaskDays.Include("Tasks"); }
        }

        public IQueryable<TaskDay> AllIncluding(params Expression<Func<TaskDay, object>>[] includeProperties)
        {
            IQueryable<TaskDay> query = context.TaskDays;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public TaskDay Find(int id)
        {
            return context.TaskDays.Find(id);
        }

        public void InsertOrUpdate(TaskDay taskday)
        {
            if (taskday.Id == default(int)) {
                // New entity
                context.TaskDays.Add(taskday);
            } else {
                // Existing entity
                context.Entry(taskday).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var taskday = context.TaskDays.Find(id);
            context.TaskDays.Remove(taskday);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface ITaskDayRepository : IDisposable
    {
        IQueryable<TaskDay> All { get; }
        IQueryable<TaskDay> AllIncluding(params Expression<Func<TaskDay, object>>[] includeProperties);
        TaskDay Find(int id);
        void InsertOrUpdate(TaskDay taskday);
        void Delete(int id);
        void Save();
    }
}