using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using System.Linq.Dynamic;

namespace Framework.Data.EF
{
    /// <summary>
    /// Generic repository
    /// </summary>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        #region Fields

        private readonly DbContext _context;

        internal DbContext Context { get { return _context; } }

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            Check.Argument.IsNotNull(context, "context");
            _context = context;
        }

        #endregion Constructors

        #region Methods

        #region [ Add ]
        public void Add(TEntity entity)
        {
            //Check.Argument.IsNotNull(entity, "entity");
            _context.Set<TEntity>().Add(entity);
        }

        #endregion

        #region [ Delete ]

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(ISpecification<TEntity> criteria)
        {
            _context.Set<TEntity>().RemoveRange(GetQuery(criteria));
        }

        #endregion

        #region [ Count ]

        public int Count()
        {
            try
            {
                return GetQuery().Count();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public int Count(ISpecification<TEntity> criteria)
        {
            try
            {
                return GetQuery().Count(criteria.Expression);
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        #endregion

        #region [ Search ]

        public TEntity Single(ISpecification<TEntity> criteria)
        {
            try
            {
                return GetQuery(criteria).SingleOrDefault();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }
        public TSelectEntity Single<TSelectEntity>(ISpecification<TEntity> criteria, object parameters = null)
        {
            try
            {
                return GetQuery(criteria).Project().To<TSelectEntity>(parameters).SingleOrDefault();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }


        public TEntity First(ISpecification<TEntity> criteria)
        {
            try
            {
                return GetQuery(criteria).FirstOrDefault();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }
        public TSelectEntity First<TSelectEntity>(ISpecification<TEntity> criteria, object parameters = null)
        {
            try
            {
                return GetQuery(criteria).Project().To<TSelectEntity>(parameters).FirstOrDefault();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TEntity> Get(ISpecification<TEntity> specification)
        {
            try
            {
                var query = GetQuery().Where(specification.Expression);
                return query.ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TSelectEntity> Get<TSelectEntity>(ISpecification<TEntity> specification, object parameters = null)
        {
            try
            {
                var query = GetQuery().Where(specification.Expression);
                return query.Project().To<TSelectEntity>(parameters).ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }


        public IEnumerable<TEntity> Get<TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, SortDirection sortOrder = SortDirection.Ascending)
        {
            try
            {
                IQueryable<TEntity> query;
                if (sortOrder == SortDirection.Ascending)
                    query = GetQuery().Where(specification.Expression).OrderBy(orderBy);
                else
                    query = GetQuery().Where(specification.Expression).OrderByDescending(orderBy);
                return query.ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TSelectEntity> Get<TOrderBy, TSelectEntity>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, SortDirection sortOrder = SortDirection.Ascending, object parameters = null)
        {
            try
            {
                IQueryable<TEntity> query;
                if (sortOrder == SortDirection.Ascending)
                    query = GetQuery().Where(specification.Expression).OrderBy(orderBy);
                else
                    query = GetQuery().Where(specification.Expression).OrderByDescending(orderBy);
                return query.Project().To<TSelectEntity>(parameters).ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TEntity> Get(ISpecification<TEntity> specification, string orderby)
        {
            try
            {
                var query = GetQuery().Where(specification.Expression).OrderBy(orderby);
                return query.ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TSelectEntity> Get<TSelectEntity>(ISpecification<TEntity> specification, string orderby, object parameters = null)
        {
            try
            {
                var query = GetQuery().Where(specification.Expression).OrderBy(orderby);
                return query.Project().To<TSelectEntity>(parameters).ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }


        public IEnumerable<TEntity> Get<TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int startIndex, int pageSize, SortDirection sortOrder = SortDirection.Ascending)
        {
            try
            {
                IQueryable<TEntity> query;
                if (sortOrder == SortDirection.Ascending)
                    query = GetQuery().Where(specification.Expression).OrderBy(orderBy).Skip((startIndex)).Take(pageSize);
                else
                    query = GetQuery().Where(specification.Expression).OrderByDescending(orderBy).Skip((startIndex)).Take(pageSize);
                return query.ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }
        public IEnumerable<TSelectEntity> Get<TOrderBy, TSelectEntity>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int startIndex, int pageSize, SortDirection sortOrder = SortDirection.Ascending, object parameters = null)
        {
            try
            {
                IQueryable<TEntity> query;
                if (sortOrder == SortDirection.Ascending)
                    query = GetQuery().Where(specification.Expression).OrderBy(orderBy).Skip((startIndex)).Take(pageSize);
                else
                    query = GetQuery().Where(specification.Expression).OrderByDescending(orderBy).Skip((startIndex)).Take(pageSize);
                return query.Project().To<TSelectEntity>(parameters).ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TEntity> Get(ISpecification<TEntity> specification, string orderBy, int startIndex, int pageSize)
        {
            try
            {
                var result = GetQuery().Where(specification.Expression).OrderBy(orderBy).Skip(startIndex).Take(pageSize);
                return result.ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TSelectEntity> Get<TSelectEntity>(ISpecification<TEntity> specification, string orderBy, int startIndex, int pageSize, object parameters = null)
        {
            try
            {
                var result = GetQuery().Where(specification.Expression).OrderBy(orderBy).Skip(startIndex).Take(pageSize);
                return result.Project().To<TSelectEntity>(parameters).ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return GetQuery().ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TSelectEntity> GetAll<TSelectEntity>(object parameters = null)
        {
            try
            {
                return GetQuery().Project().To<TSelectEntity>(parameters).ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        //-------->>>
        public IEnumerable<TEntity> GetAll<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, SortDirection sortOrder = SortDirection.Ascending)
        {
            try
            {
                IQueryable<TEntity> query;
                if (sortOrder == SortDirection.Ascending)
                    query = GetQuery().OrderBy(orderBy);
                else
                    query = GetQuery().OrderByDescending(orderBy);
                return query.ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TEntity> GetAll<TOrderBy>(string orderby)
        {
            try
            {
                var query = GetQuery().OrderBy(orderby);
                return query.ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TEntity> GetAll<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int startIndex, int pageSize, SortDirection sortOrder = SortDirection.Ascending)
        {
            try
            {
                IQueryable<TEntity> query;
                if (sortOrder == SortDirection.Ascending)
                    query = GetQuery().OrderBy(orderBy).Skip((startIndex)).Take(pageSize);
                else
                    query = GetQuery().OrderByDescending(orderBy).Skip((startIndex)).Take(pageSize);
                return query.ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public IEnumerable<TEntity> GetAll(string orderBy, int startIndex, int pageSize)
        {
            try
            {
                var result = GetQuery().OrderBy(orderBy).Skip(startIndex).Take(pageSize);
                return result.ToArray();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        #endregion

        #region [ Query ]

        protected IQueryable<TEntity> GetQuery()
        {
            return _context.Set<TEntity>();
        }

        protected IQueryable<TEntity> GetQuery(ISpecification<TEntity> criteria)
        {
            var query = GetQuery().Where(criteria.Expression);
            return query;
        }

        #endregion


        #endregion Methods 
    }
}