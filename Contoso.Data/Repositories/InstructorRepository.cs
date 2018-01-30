using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Contoso.Domain;
using Contoso.Data.Infrastructure;

namespace Contoso.Data
{
    public class InstructorRepository : RepositoryBase<Instructor>, IInstructorRepository
    {
        public InstructorRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IInstructorRepository : IRepository<Instructor>
    {
    }
}
