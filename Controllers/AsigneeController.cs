using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkExercise.Controllers
{
    [Route("asignee")]
    public class AsigneeController : Controller
    {
        private ApplicationContext ApplicationContext;
        public AsigneeController(ApplicationContext applicationContext)
        {
            this.ApplicationContext = applicationContext;
        }

        [Route("")]
        public IActionResult AsigneeList()
        {
            List<Asignee> asigneeList = ApplicationContext.Asignees.ToList();
            return View(asigneeList);
        }

        [Route("{id}/editasignee")]
        public IActionResult EditAsignee()
        {
            return View();
        }

        [HttpPost("{id}/editasignee")]
        public IActionResult EditAsignee([FromRoute] long id, string name)
        {
            using (var context = ApplicationContext)
            {
                var asigneeToUpdate = context.Asignees.First(a => a.Id == id);
                asigneeToUpdate.Name = name;
                context.SaveChanges();
            }
            return RedirectToAction("");
        }

        [HttpGet("addasignee")]
        public IActionResult AddAsignee()
        {
            return View();
        }

        [HttpPost("addasignee")]
        public IActionResult AddAsignee(string name, string email)
        {
            if (name == null)
            {
                ViewData["Alert"] = "<p class=\"alert\">No title provided, try again!</p>";
                return View();
            }

            using (var context = ApplicationContext)
            {
                var newAsignee = new Asignee(name, email);
                context.Asignees.Add(newAsignee);
                context.SaveChanges();
            }
            return RedirectToAction("");
        }

        [Route("/asignee/{id}/delete")]
        public IActionResult DeleteTask([FromRoute] int id)
        {
            using (var context = ApplicationContext)
            {
                var deleteAsignee = context.Asignees.FirstOrDefault(a => a.Id == id);
                context.Asignees.Remove(deleteAsignee);
                context.SaveChanges();
            }
            return RedirectToAction("");
        }

        [Route("/asignee/{id}/asigneetasks")]
        public IActionResult AsigneeTasks([FromRoute] int id)
        {
            var asignee = ApplicationContext.Asignees.Include(a => a.Tasks).FirstOrDefault(a => a.Id == id);
            return View(asignee);
        }
    }
}