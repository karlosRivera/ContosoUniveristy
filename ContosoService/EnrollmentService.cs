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
    public class EnrollmentService : RepositoryService<Enrollment>, IRepositoryService<Enrollment>
    {
        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
            : base(enrollmentRepository, unitOfWork)
        {
        }
    }
}
