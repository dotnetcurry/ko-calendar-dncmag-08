using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcKnockoutCalendar.Models
{
    public class TaskDetailRepository : ITaskDetailRepository
    {
        MvcKnockoutCalendarContext context = new MvcKnockoutCalendarContext();

        public IQueryable<TaskDetail> All
        {
            get { return context.TaskDetails; }
        }

        public IQueryable<TaskDetail> AllIncluding(params Expression<Func<TaskDetail, object>>[] includeProperties)
        {
            IQueryable<TaskDetail> query = context.TaskDetails;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public TaskDetail Find(int id)
        {
            return context.TaskDetails.Find(id);
        }

        public void InsertOrUpdate(TaskDetail taskdetail)
        {
            if (taskdetail.Id == default(int))
            {
                // New entity
                context.TaskDetails.Add(taskdetail);
            }
            else
            {
                // Existing entity
                context.Entry(taskdetail).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var taskdetail = context.TaskDetails.Find(id);
            context.TaskDetails.Remove(taskdetail);
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

    public interface ITaskDetailRepository : IDisposable
    {
        IQueryable<TaskDetail> All { get; }
        IQueryable<TaskDetail> AllIncluding(params Expression<Func<TaskDetail, object>>[] includeProperties);
        TaskDetail Find(int id);
        void InsertOrUpdate(TaskDetail taskdetail);
        void Delete(int id);
        void Save();
    }
}