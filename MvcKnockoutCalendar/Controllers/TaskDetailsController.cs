using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKnockoutCalendar.Models;

namespace MvcKnockoutCalendar.Controllers
{   
    public class TaskDetailsController : Controller
    {
		private readonly ITaskDayRepository taskdayRepository;
		private readonly ITaskDetailRepository taskdetailRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TaskDetailsController() : this(new TaskDayRepository(), new TaskDetailRepository())
        {
        }

        public TaskDetailsController(ITaskDayRepository taskdayRepository, ITaskDetailRepository taskdetailRepository)
        {
			this.taskdayRepository = taskdayRepository;
			this.taskdetailRepository = taskdetailRepository;
        }

        //
        // GET: /TaskDetails/

        public ViewResult Index()
        {
            return View(taskdetailRepository.AllIncluding(taskdetail => taskdetail.ParentTask));
        }

        //
        // GET: /TaskDetails/Details/5

        public ViewResult Details(int id)
        {
            return View(taskdetailRepository.Find(id));
        }

        //
        // GET: /TaskDetails/Create

        public ActionResult Create()
        {
			ViewBag.PossibleParentTasks = taskdayRepository.All;
            return View();
        } 

        //
        // POST: /TaskDetails/Create

        [HttpPost]
        public ActionResult Create(TaskDetail taskdetail)
        {
            if (ModelState.IsValid) {
                taskdetailRepository.InsertOrUpdate(taskdetail);
                taskdetailRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleParentTasks = taskdayRepository.All;
				return View();
			}
        }
        
        //
        // GET: /TaskDetails/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleParentTasks = taskdayRepository.All;
             return View(taskdetailRepository.Find(id));
        }

        //
        // POST: /TaskDetails/Edit/5

        [HttpPost]
        public ActionResult Edit(TaskDetail taskdetail)
        {
            if (ModelState.IsValid) {
                taskdetailRepository.InsertOrUpdate(taskdetail);
                taskdetailRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleParentTasks = taskdayRepository.All;
				return View();
			}
        }

        //
        // GET: /TaskDetails/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(taskdetailRepository.Find(id));
        }

        //
        // POST: /TaskDetails/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            taskdetailRepository.Delete(id);
            taskdetailRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                taskdayRepository.Dispose();
                taskdetailRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

