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
    public class StudentService : RepositoryService<Student>, IRepositoryService<Student>
    {
        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
            : base(studentRepository, unitOfWork)
        {
        }
    }
}

