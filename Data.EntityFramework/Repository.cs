using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using System.Linq.Dynamic;
using AutoMapper;

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
        private readonly IConfigurationProvider _mapperConfig;

        internal DbContext Context => _context;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(
            DbContext context,
            IConfigurationProvider mapperConfig
            )
        {
            Check.Argument.IsNotNull(context, "context");
            Check.Argument.IsNotNull(mapperConfig, "mapperConfig");
            _context = context;
            _mapperConfig = mapperConfig;
        }

        #endregion Constructors

        #region Methods

        #region [ Add ]
        public void Add(TEntity entity)
        {
            //Check.Argument.IsNotNull(entity, "entity");
            _context.Set<TEntity>().Add(entity);
        }
        public void AddOrUpdate(params TEntity[] entities)
        {
            _context.Set<TEntity>().AddOrUpdate(entities);
        }
        public void AddOrUpdate(Expression<Func<TEntity, object>> identifierExpression, params TEntity[] entities)
        {
            _context.Set<TEntity>().AddOrUpdate(identifierExpression, entities);
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
                return GetQuery(criteria).ProjectTo<TSelectEntity>(_mapperConfig, parameters).SingleOrDefault();
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
                return GetQuery(criteria).ProjectTo<TSelectEntity>(_mapperConfig, parameters).FirstOrDefault();
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
                return query.ProjectTo<TSelectEntity>(_mapperConfig, parameters).ToArray();
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
                return query.ProjectTo<TSelectEntity>(_mapperConfig, parameters).ToArray();
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
                return query.ProjectTo<TSelectEntity>(_mapperConfig, parameters).ToArray();
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
                return query.ProjectTo<TSelectEntity>(_mapperConfig, parameters).ToArray();
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
                return result.ProjectTo<TSelectEntity>(_mapperConfig, parameters).ToArray();
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
                return GetQuery().ProjectTo<TSelectEntity>(_mapperConfig, parameters).ToArray();
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