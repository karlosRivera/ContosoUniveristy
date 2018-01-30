using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contoso.Domain;
using Contoso.Data.Infrastructure;

namespace Contoso.Data
{
    public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
    }
}


