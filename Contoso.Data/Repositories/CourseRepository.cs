using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contoso.Domain;
using Contoso.Data.Infrastructure;

namespace Contoso.Data
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ICourseRepository : IRepository<Course>
    {
    }
}

