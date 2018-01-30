//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using Contoso.Domain;

//namespace Contoso.Data
//{
//    public class SchoolContext : DbContext
//    {
//        public DbSet<Student> Students { get; set; }
//        public DbSet<Enrollment> Enrollments { get; set; }
//        public DbSet<Course> Courses { get; set; }
//        public DbSet<User> Users { get; set; }
//        public DbSet<Role> Roles { get; set; }
//        public virtual void Commit()
//        {
//            base.SaveChanges();
//        }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using Contoso.Domain;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Contoso.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    modelBuilder.Entity<Instructor>()
        //        .HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);
        //    modelBuilder.Entity<Course>()
        //        .HasMany(c => c.Instructors).WithMany(i => i.Courses)
        //        .Map(t => t.MapLeftKey("CourseID")
        //            .MapRightKey("InstructorID")
        //            .ToTable("CourseInstructor"));
        //    modelBuilder.Entity<Department>()
        //        .HasOptional(x => x.Administrator);
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Instructor>()
                .HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("PersonID")
                    .ToTable("CourseInstructor"));
            modelBuilder.Entity<Department>()
                .HasOptional(x => x.Administrator);
        }
    }
}