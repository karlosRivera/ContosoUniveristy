using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contoso.Domain;
using Contoso.Data.Infrastructure;

namespace Contoso.Data
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IDepartmentRepository : IRepository<Department>
    {
    }
}
