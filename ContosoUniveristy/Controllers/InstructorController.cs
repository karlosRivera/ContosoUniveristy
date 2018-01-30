using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contoso.Domain;
using Contoso.Domain.ViewModels;
using Contoso.Service;
using Contoso.Service.Infrastructure;
using PagedList;
using Contoso.Web.Helpers;

namespace ContosoUniveristy.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IRepositoryService<Instructor> instructorService;
        private readonly IRepositoryServiceGet<Course> courseService;
        private readonly IRepositoryService<Enrollment> enrollmentService;

        public InstructorController(IRepositoryService<Instructor> instructorService, 
                                    IRepositoryServiceGet<Course> courseService, 
                                    IRepositoryService<Enrollment> enrollmentService)
        {
            this.instructorService = instructorService;
            this.courseService = courseService;
            this.enrollmentService = enrollmentService;
        }

        // GET: /Instructor/
        public ActionResult Index(Int32? id, Int32? courseID)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = instructorService.GetAll();

            if (id != null)
            {
                ViewBag.PersonID = id.Value;
                viewModel.Courses = viewModel.Instructors.Where(i => i.PersonID == id.Value).Single().Courses;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                var enrollments = enrollmentService.GetMany(c => c.CourseID == selectedCourse.CourseID);
                viewModel.Enrollments = enrollments;
            }

            return View(viewModel);
        }

        // GET: /Instructor/Details/5
        [HttpGet]
        public ViewResult Details(int id)
        {
            var instructor = instructorService.GetById(id);
            return View(instructor);
        }

        //
        // GET: /Instructor/Create
        [HttpGet]
        public ActionResult Create()
        {
            // ViewBag.PersonID = new SelectList(db.OfficeAssignments, "PersonID", "Location");
            var instructor = new Instructor();
            instructor.HireDate = DateTime.Now;
            return View(instructor);
        }

        [HttpPost]
        public ActionResult Create(Instructor instructor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    instructorService.Insert(instructor);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(instructor);
        }

        //
        // GET: /Instructor/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Instructor instructor = db.Instructors
            //    .Include(i => i.Courses)
            //    .Where(i => i.PersonID == id)
            //    .Single();
            var instructor = instructorService.GetById(id);
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = courseService.GetAll();
            var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();

            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }

        // POST: /Instructor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedCourses)
        {
            var instructorToUpdate = instructorService.GetById(id);
            if (TryUpdateModel(instructorToUpdate, "", null, new string[] { "Courses" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }

                    UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                    instructorService.Update(instructorToUpdate);

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }

        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                var allCoursesForDelete = courseService.GetAll();
                var instructorCoursesCurrent = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.CourseID));

                // foreach (var course in instructorToUpdate.Courses)
                foreach (var course in allCoursesForDelete)
                {
                    if (instructorCoursesCurrent.Contains(course.CourseID))
                    {
                    instructorToUpdate.Courses.Remove(course);
                    }
                }
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.CourseID));
            var allCourses = courseService.GetAll();

            foreach (var course in allCourses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }

        // GET: /Instructor/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var instructor = instructorService.GetById(id);
            return View(instructor);
        }

        // POST: /Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            instructorService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
