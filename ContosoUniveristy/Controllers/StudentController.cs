using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contoso.Data;
using Contoso.Domain;
using Contoso.Service;
using Contoso.Service.Infrastructure;
using PagedList;
using Contoso.Web.Helpers;

namespace ContosoUniveristy.Controllers
{
    public class StudentController : Controller
    {
        private readonly IRepositoryService<Student> studentService;

        public StudentController(IRepositoryService<Student> studentService)
        {
            this.studentService = studentService;
        }

        // GET: /Student/
        public ViewResult Index(string currentFilter, string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            var students = studentService.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || s.FirstMidName.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(students);
        }

        // GET: /Student/Details/5
        public ViewResult Details(int id)
        {
            var student = studentService.GetById(id);
            return View(student);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var student = new Student();
            student.EnrollmentDate = DateTime.Now;
            return View(student);
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    studentService.Insert(student);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
        }

        // GET: /Studenty/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = studentService.GetById(id); 
            return View(student);
        }

        // POST: /Studenty/Edit/5
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                studentService.Update(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: /Studenty/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var student = studentService.GetById(id);
            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            studentService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //studentService.Dispose();
            //base.Dispose(disposing);
        }

    }
}
