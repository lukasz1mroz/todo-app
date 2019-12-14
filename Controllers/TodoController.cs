using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExercise.Controllers
{
    [Route("")]
    [Route("todo")]
    public class TodoController : Controller
    {
        private ApplicationContext ApplicationContext;
        public TodoController(ApplicationContext applicationContext)
        {
            this.ApplicationContext = applicationContext;
        }

        [Route("")]
        [HttpGet("list")]
        public IActionResult List()
        {
            var tasksList = ApplicationContext.Tasks.Include(t => t.Asignee).ToList();
            return View(tasksList);
        }

        [HttpPost("list")]
        public IActionResult List(string taskDescription)
        {
            var tasksList = ApplicationContext.Tasks.Include(t => t.Asignee).Where(item => item.Title == taskDescription || item.Description == taskDescription || item.Asignee.Name == taskDescription || item.DueAt.ToShortDateString() == taskDescription || item.CreatedAt.ToShortDateString() == taskDescription).ToList();
            return View(tasksList);
        }

        [HttpGet("/todo/add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("/todo/add")] 
        public IActionResult Add(string title, string description, bool isUrgent, bool isDone)
        {
            if (title == null || description == null)
            {
                ViewData["Alert"] = "<p class=\"alert\">Must provide title and description</p>";
                return View();
            }

            using (var context = ApplicationContext)
            {
                var newTask = new Todo(title, description, isUrgent, isDone);
                context.Tasks.Add(newTask);
                context.SaveChanges();
            }
            return RedirectToAction("list");
        }

        [Route("/todo/{id}/delete")]
        public IActionResult DeleteTask([FromRoute] long id)
        {
            using (var context = ApplicationContext)
            {
                var deletedObject = context.Tasks.First(task => task.Id == id);
                context.Tasks.Remove(deletedObject);
                context.SaveChanges();
            }
            return RedirectToAction("list");
        }

        [Route("/todo/{id}/edit")]
        public IActionResult Edit()
        {
            var asigneesList = ApplicationContext.Asignees.ToList();
            return View(asigneesList);
        }

        [HttpPost("/todo/{id}/edit")]
        public IActionResult Edit(long id, string title, bool isUrgent, bool isDone, string asigneeName, string dueDate)
        {
            using (var context = ApplicationContext)
            {
                DateTime dueAtDate = DateTime.Parse(dueDate);
                if (asigneeName != null)
                {
                    var newAsignee = context.Asignees.First(a => a.Name == asigneeName);
                    context.Tasks.Include(t => t.Asignee).ToList().Find(t => t.Id == id).Asignee = newAsignee;
                }
                context.Tasks.ToList().Find(t => t.Id == id).DueAt = dueAtDate;

                if (isUrgent)
                {
                    context.Tasks.ToList().Find(t => t.Id == id).IsUrgent = true;
                }
                else if (isDone)
                {
                    context.Tasks.ToList().Find(t => t.Id == id).IsDone = true;
                }
                context.SaveChanges();
            }
            return RedirectToAction("list");
        }
    }
}