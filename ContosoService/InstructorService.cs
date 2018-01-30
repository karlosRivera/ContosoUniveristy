using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contoso.Domain;
using Contoso.Data;
using Contoso.Data.Infrastructure;
using Contoso.Service.Infrastructure ;
using System.Linq.Expressions;

namespace Contoso.Service
{
    public class InstructorService : RepositoryService<Instructor>, IRepositoryService<Instructor>
    {
        public InstructorService(IInstructorRepository instructorRepository, IUnitOfWork unitOfWork)
            : base(instructorRepository, unitOfWork)
        {
        }
    }
}

