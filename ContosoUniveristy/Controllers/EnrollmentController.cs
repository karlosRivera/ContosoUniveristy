using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Contoso.Domain;
using Contoso.Domain.ViewModels;
using Contoso.Service;
using Contoso.Service.Infrastructure;
using Contoso.Data;

namespace ContosoUniveristy.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IRepositoryService<Enrollment> enrollmentService;
        private readonly IRepositoryServiceGet<Course> courseService;
        private readonly IRepositoryService<Student> studentService;

        public EnrollmentController(IRepositoryService<Enrollment> enrollmentService,
                                    IRepositoryServiceGet<Course> courseService,
                                    IRepositoryService<Student> studentService)
        {
            this.enrollmentService = enrollmentService;
            this.courseService = courseService;
            this.studentService = studentService;
        }

        // GET: /Enrollment/
        public ActionResult Index()
        {
            var enrollments = enrollmentService.GetAll();
            var viewModel = new List<EnrollmentIndexData>() ;

            foreach (var enrollment in enrollments)
            {
                viewModel.Add(new EnrollmentIndexData
                {
                    EnrollmentID = enrollment.EnrollmentID,
                    CourseID = enrollment.CourseID,
                    CourseTitle = enrollment.Course.Title,
                    PersonID = enrollment.PersonID,
                    FullName = enrollment.Student.FullName,
                    Grade = enrollment.Grade
                });
            }

            return View(viewModel);
        }

        //public ViewResult Details(int id)
        //{
        //    Enrollment enrollment = db.Enrollments.Find(id);
        //    return View(enrollment);
        //}


        // GET: /Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(courseService.GetAll(), "CourseID", "Title");
            ViewBag.PersonID = new SelectList(studentService.GetAll(), "PersonID", "FullName");
            return View();
        }

        // POST: /Enrollment/Create
        [HttpPost]
        public ActionResult Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                enrollmentService.Insert(enrollment);
                enrollmentService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(courseService.GetAll(), "CourseID", "Title", enrollment.CourseID);
            ViewBag.PersonID = new SelectList(studentService.GetAll(), "PersonID", "FullName", enrollment.PersonID);
            return View(enrollment);
        }

        // GET: /Enrollment/Edit/5
        public ActionResult Edit(int id)
        {
            Enrollment enrollment = enrollmentService.GetById(id);
            ViewBag.CourseID = new SelectList(courseService.GetAll(), "CourseID", "Title", enrollment.CourseID);
            ViewBag.PersonID = new SelectList(studentService.GetAll(), "PersonID", "FullName", enrollment.PersonID);
            return View(enrollment);
        }

        // POST: /Enrollment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var enrollment = enrollmentService.GetById(id);
            if (TryUpdateModel(enrollment))
            {
                enrollmentService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(courseService.GetAll(), "CourseID", "Title", enrollment.CourseID);
            ViewBag.PersonID = new SelectList(studentService.GetAll(), "PersonID", "FullName", enrollment.PersonID);
            return View(enrollment);
        }

        // GET: /Enrollment/Delete/5
        public ActionResult Delete(int id)
        {
            Enrollment enrollment = enrollmentService.GetById(id);
            return View(enrollment);
        }

        // POST: /Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            enrollmentService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
