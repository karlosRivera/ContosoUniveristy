using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contoso.Domain;
using Contoso.Data;
using Contoso.Data.Infrastructure;
using Contoso.Service.Infrastructure;

namespace Contoso.Service
{
    public class DepartmentService : RepositoryService<Department>, IRepositoryService<Department>
    {
        public DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
            : base(departmentRepository, unitOfWork)
        {
        }
    }
}


