using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contoso.Domain;
using Contoso.Data;
using Contoso.Data.Infrastructure;
using Contoso.Service.Infrastructure;
using System.Data;
using System.Linq.Expressions;

namespace Contoso.Service
{
    public class CourseService : RepositoryService<Course>, IRepositoryServiceGet<Course>
    {
        public CourseService(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
            : base(courseRepository, unitOfWork)
        {
        }

        public IEnumerable<Course> Get(Expression<Func<Course, bool>> filter = null,
            Func<IQueryable<Course>, IOrderedQueryable<Course>> orderBy = null,
            string includeProperties = "")
        {
            var courses = genericRepository.Get(filter, orderBy, includeProperties);
            return courses;
        }
    }
}


