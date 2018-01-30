using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contoso.Domain;
using Contoso.Domain.ViewModels;
using Contoso.Service;
using Contoso.Service.Infrastructure;

namespace ContosoUniveristy.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IRepositoryService<Department> departmentService;
        private readonly IRepositoryService<Instructor> instructorService;

        // private readonly IInstructorService instructorService;

        public DepartmentController(IRepositoryService<Department> departmentService, IRepositoryService<Instructor> instructorService)
        {
            this.departmentService = departmentService;
            this.instructorService = instructorService;
        }

        // GET: /Department/
        // id is DepartmentId
        [HttpGet]
        public ViewResult Index(Int32? id, Int32? courseID)
        {
            var viewModel = new DepartmentIndexData();
            viewModel.Departments = departmentService.GetAll();

            if (id != null)
            {
                ViewBag.DepartmentID = id.Value;
                viewModel.Courses = viewModel.Departments.Where(i => i.DepartmentID == id.Value).Single().Courses;
            }
            return View(viewModel);
        }

        // GET: /Department/Details/5
        [HttpGet]
        public ViewResult Details(int id)
        {
            Department department = departmentService.GetById(id);
            return View(department);
        }


        // GET: /Department/Create
        [HttpGet]
        public ActionResult Create()
        {
            var department = new Department();
            department.StartDate = DateTime.Now;
            PopulateAdministratorDropDownList();
            return View(department);
        }

        // POST: /Department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    departmentService.Insert(department);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateAdministratorDropDownList(department.PersonID);
            return View(department);
        }

        // GET: /Department/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Department department = departmentService.GetById(id);
            PopulateAdministratorDropDownList(department.PersonID);
            return View(department);
        }

         // POST: /Department/Edit/5
         [HttpPost]
         public ActionResult Edit(Department department)
         {
           try
            {
                if (ModelState.IsValid)
                {
                 departmentService.Update(department); 
                 return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
               //Log the error (add a variable name after DataException)
               ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateAdministratorDropDownList(department.PersonID);
            return View(department);
         }

         private void PopulateAdministratorDropDownList(object selectedAdministrator = null)
         {
             var administrators = instructorService.GetAll().OrderBy(i => i.LastName);
             ViewBag.PersonID = new SelectList(administrators, "PersonID", "FullName", selectedAdministrator);
         }

         // GET: /Department/Delete/5
         [HttpGet]
         public ActionResult Delete(int id)
         {
             var department = departmentService.GetById(id);
             return View(department);
         }

         // POST: /Department/Delete/5
         [HttpPost, ActionName("Delete")]
         public ActionResult DeleteConfirmed(int id)
         {
             departmentService.Delete(id);
             return RedirectToAction("Index");
         }

         protected override void Dispose(bool disposing)
         {
             //studentService.Dispose();
             //base.Dispose(disposing);
         }

    }
}
