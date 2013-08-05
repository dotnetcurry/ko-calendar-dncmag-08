using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKnockoutCalendar.Models;

namespace MvcKnockoutCalendar.Controllers
{   
    public class TaskDayController : Controller
    {
		private readonly ITaskDayRepository taskdayRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TaskDayController() : this(new TaskDayRepository())
        {
        }

        public TaskDayController(ITaskDayRepository taskdayRepository)
        {
			this.taskdayRepository = taskdayRepository;
        }

        //
        // GET: /TaskDay/

        public ViewResult Index()
        {
            return View(taskdayRepository.AllIncluding(taskday => taskday.Tasks));
        }

        //
        // GET: /TaskDay/Details/5

        public ViewResult Details(int id)
        {
            return View(taskdayRepository.Find(id));
        }

        //
        // GET: /TaskDay/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TaskDay/Create

        [HttpPost]
        public ActionResult Create(TaskDay taskday)
        {
            if (ModelState.IsValid) {
                taskdayRepository.InsertOrUpdate(taskday);
                taskdayRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /TaskDay/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(taskdayRepository.Find(id));
        }

        //
        // POST: /TaskDay/Edit/5

        [HttpPost]
        public ActionResult Edit(TaskDay taskday)
        {
            if (ModelState.IsValid) {
                taskdayRepository.InsertOrUpdate(taskday);
                taskdayRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /TaskDay/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(taskdayRepository.Find(id));
        }

        //
        // POST: /TaskDay/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            taskdayRepository.Delete(id);
            taskdayRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                taskdayRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

