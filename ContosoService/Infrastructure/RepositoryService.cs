using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Contoso.Domain;
using Contoso.Data;
using Contoso.Data.Infrastructure;

namespace Contoso.Service.Infrastructure
{
    public abstract class RepositoryService<T> where T : class
    {
        protected IRepository<T> genericRepository;
        private readonly IUnitOfWork unitOfWork;

        public RepositoryService(IRepository<T> genericRepository, IUnitOfWork unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
        }

        public RepositoryService()
        {
            this.unitOfWork = new UnitOfWork(new DatabaseFactory());
        }

        #region IRepositoryService<T> Members

        public IEnumerable<T> GetAll()
        {
            return genericRepository.GetAll();
        }

        public T GetById(int id)
        {
            return genericRepository.GetById(id);
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return genericRepository.GetMany(where);
        }

        public void Update(T Entity)
        {
            // throw new NotImplementedException();

            genericRepository.Update(Entity);
            Save();
        }

        public void Insert(T Entity)
        {
            genericRepository.Add(Entity);
            Save();
        }

        public void Delete(int id)
        {
            var student = genericRepository.GetById(id);
            genericRepository.Delete(student);
            Save();
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}
