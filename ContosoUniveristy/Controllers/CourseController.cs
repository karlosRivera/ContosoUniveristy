using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contoso.Domain;
using Contoso.Service;
using Contoso.Service.Infrastructure;

namespace ContosoUniversity.Controllers
{
    public class CourseController : Controller
    {
        private readonly IRepositoryServiceGet<Course> courseService;
        private readonly IRepositoryService<Department> departmentService;

        public CourseController(IRepositoryServiceGet<Course> courseService, IRepositoryService<Department> departmentService)
        {
            this.courseService = courseService;
            this.departmentService = departmentService;
        }

        // GET: /Course/
        public ActionResult Index(int? SelectedDepartment)
        {
            var departments = departmentService.GetAll().OrderBy(i => i.Name);
            ViewBag.SelectedDepartment = new SelectList(departments, "DepartmentID", "Name", SelectedDepartment);

            int departmentID = SelectedDepartment.GetValueOrDefault();
            return View(courseService.Get(
                filter: d => !SelectedDepartment.HasValue || d.DepartmentID == departmentID,
                orderBy: q => q.OrderBy(d => d.CourseID),
                includeProperties: "Department"));
        }

        // GET: /Course/Details/5
        //public ActionResult Details(int id)
        //{
        //    var query = "SELECT * FROM Course WHERE CourseID = @p0";
        //    return View(unitOfWork.CourseRepository.GetWithRawSql(query, id).Single());
        //}

        // GET: /Course/Details/5
        [HttpGet]
        public ViewResult Details(int id)
        {
            var course = courseService.GetById(id);
            return View(course);
        }

        // GET: /Course/Create
        [HttpGet]
        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = departmentService.GetAll().OrderBy(i => i.Name);
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        }

        // POST: /Course/Create
        [HttpPost]
        public ActionResult Create(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    courseService.Insert(course);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var course = courseService.GetById(id);
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var course = courseService.GetById(id);
            if (TryUpdateModel(course))
            {
                courseService.Save();
                return RedirectToAction("Index");
            }
            else {
                PopulateDepartmentsDropDownList(course.DepartmentID);
                return View(course);
            }
        }

        // GET: /Course/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var course = courseService.GetById(id);
            return View(course);
        }

        // POST: /Course/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            courseService.Delete(id);
            return RedirectToAction("Index");
        }

        //public ActionResult UpdateCourseCredits(int? multiplier)
        //{
        //    if (multiplier != null)
        //    {
        //        ViewBag.RowsAffected = unitOfWork.CourseRepository.UpdateCourseCredits(multiplier.Value);
        //    }
        //    return View();
        //}
        
        protected override void Dispose(bool disposing)
        {
            //unitOfWork.Dispose();
            //base.Dispose(disposing);
        }
    }
}