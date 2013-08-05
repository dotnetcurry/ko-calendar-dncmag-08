using MvcKnockoutCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcKnockoutCalendar.Controllers
{
    public class CalendarController : ApiController
    {
        ITaskDetailRepository _taskDetail = new TaskDetailRepository();
        ITaskDayRepository _taskDay = new TaskDayRepository();

        [HttpPost]
        public bool SaveTask(TaskDetail task)
        {
            DateTime targetDay = new DateTime(task.Starts.Year, task.Starts.Month, task.Starts.Day);
            TaskDay day = _taskDay.All.FirstOrDefault<TaskDay>(_ => _.Day == targetDay);
            if (day == null)
            {
                day = new TaskDay
                {
                    Day = new DateTime(task.Starts.Year, task.Starts.Month, task.Starts.Day),
                    Tasks = new List<TaskDetail>()
                };
                _taskDay.InsertOrUpdate(day);
                _taskDay.Save();
                task.ParentTaskId = day.Id;
            }
            else
            {
                task.ParentTaskId = day.Id;
                task.ParentTask = null;
            }            
            _taskDetail.InsertOrUpdate(task);
            _taskDetail.Save();
            return true;
        }

        [HttpDelete]
        public bool DeleteTask(int id)
        {
            try
            {
                _taskDetail.Delete(id);
                _taskDetail.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        [HttpGet]
        public List<TaskDetail> GetTaskDetails(DateTime id)
        {
            TaskDay taskDay = _taskDay.All.FirstOrDefault<TaskDay>(_ => _.Day == id);
            if (taskDay != null)
            {
                return taskDay.Tasks;
            }
            return new List<TaskDetail>();
        }
    }
}
