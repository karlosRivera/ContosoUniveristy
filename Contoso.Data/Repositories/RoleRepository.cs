using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contoso.Domain;
using Contoso.Data.Infrastructure;

namespace Contoso.Data
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       
    }
    public interface IRoleRepository : IRepository<Role>
    {
       
    }
}
