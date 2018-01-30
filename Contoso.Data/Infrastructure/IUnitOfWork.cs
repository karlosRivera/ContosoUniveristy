using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contoso.Domain;

namespace Contoso.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        //GenericRepository<Department> DepartmentRepository();
    }
}
