using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.Domain.ViewModels
{
    public class DepartmentIndexData
    {
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
