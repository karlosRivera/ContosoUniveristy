using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// using Finance.Domain;
// using MyFinance.Data.Infrastructure;
using Contoso.Domain;
using Contoso.Data.Infrastructure;
using System.Data;


namespace Contoso.Data
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public void AssignRole(string userName, List<string> roleNames)
        {
            var user = this.GetById(userName);
            user.Roles.Clear();
            foreach (string roleName in roleNames)
            {
                var role = this.DataContext.Roles.Find(roleName);
                user.Roles.Add(role);
            }

            this.DataContext.Users.Attach(user);
            this.DataContext.Entry(user).State = EntityState.Modified;
        }

    }
       
    public interface IUserRepository : IRepository<User>
    {
        void AssignRole(string userName, List<string> roleName);        
    }
}
