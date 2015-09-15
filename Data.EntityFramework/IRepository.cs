using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Data.EF
{

    #region Enumerations

    #endregion Enumerations

    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        #region Methods

        #region [ Add ]
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Add(TEntity entity);
        #endregion

        #region [ Delete ]
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes entities which satify specificatiion
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        void Delete(ISpecification<TEntity> criteria);


        #endregion

        #region [ Count ]

        /// <summary>
        /// Counts the specified entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Counts entities satifying specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        int Count(ISpecification<TEntity> criteria);

        #endregion

        #region [ Search ]

        /// <summary>
        /// Gets single entity using specification
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity Single(ISpecification<TEntity> criteria);

        TSelectEntity Single<TSelectEntity>(ISpecification<TEntity> criteria, object parameters = null);
        /// <summary>
        /// Gets first entity with specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        TEntity First(ISpecification<TEntity> criteria);

        TSelectEntity First<TSelectEntity>(ISpecification<TEntity> criteria, object parameters = null);
        
        /// <summary>
        /// Gets entities which satifies a specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(ISpecification<TEntity> specification);

        IEnumerable<TSelectEntity> Get<TSelectEntity>(ISpecification<TEntity> specification, object parameters = null);
        ///// <summary>
        ///// Gets entities which satifies a specification.
        ///// </summary>
        ///// <typeparam name="TEntity">The type of the entity.</typeparam>
        ///// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        ///// <param name="specification">The specification.</param>
        ///// <param name="orderBy">The order by.</param>
        ///// <param name="pageIndex">Index of the page.</param>
        ///// <param name="pageSize">Size of the page.</param>
        ///// <param name="sortOrder">The sort order.</param>
        ///// <returns></returns>
        IEnumerable<TEntity> Get<TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, SortDirection sortOrder = SortDirection.Ascending);

        IEnumerable<TSelectEntity> Get<TOrderBy, TSelectEntity>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, SortDirection sortOrder = SortDirection.Ascending, object parameters = null);
        /// <summary>
        /// Gets entities which satifies a specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(ISpecification<TEntity> specification, string orderby);

        IEnumerable<TSelectEntity> Get<TSelectEntity>(ISpecification<TEntity> specification, string orderby, object parameters = null);
        ///// <summary>
        ///// Gets entities which satifies a specification.
        ///// </summary>
        ///// <typeparam name="TEntity">The type of the entity.</typeparam>
        ///// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        ///// <param name="specification">The specification.</param>
        ///// <param name="orderBy">The order by.</param>
        ///// <param name="pageIndex">Index of the page.</param>
        ///// <param name="pageSize">Size of the page.</param>
        ///// <param name="sortOrder">The sort order.</param>
        ///// <returns></returns>
        IEnumerable<TEntity> Get<TOrderBy>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int startIndex, int pageSize, SortDirection sortOrder = SortDirection.Ascending);
        IEnumerable<TSelectEntity> Get<TOrderBy, TSelectEntity>(ISpecification<TEntity> specification, Expression<Func<TEntity, TOrderBy>> orderBy, int startIndex, int pageSize, SortDirection sortOrder = SortDirection.Ascending, object parameters = null);
        /// <summary>
        /// Gets entities which satifies a specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(ISpecification<TEntity> specification, string orderBy, int startIndex, int pageSize);
        IEnumerable<TSelectEntity> Get<TSelectEntity>(ISpecification<TEntity> specification, string orderBy, int startIndex, int pageSize, object parameters = null);
        

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        IEnumerable<TSelectEntity> GetAll<TSelectEntity>(object parameters = null);
        #endregion

        #region [ Query ]

        ///// <summary>
        ///// Gets all.
        ///// </summary>
        ///// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        ///// <param name="orderBy">The order by.</param>
        ///// <param name="sortOrder">The sort order.</param>
        ///// <returns></returns>
        //IEnumerable<TEntity> GetAll<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, SortDirection sortOrder = SortDirection.Ascending);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="orderby">The orderby.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll<TOrderBy>(string orderby);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TOrderBy">The type of the order by.</typeparam>
        /// <param name="orderBy">The order by.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int startIndex, int pageSize, SortDirection sortOrder = SortDirection.Ascending);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(string orderBy, int startIndex, int pageSize);

        //IQueryable<TEntity> GetQuery(ISpecification<TEntity> criteria);

        #endregion

        //#endregion

        #endregion Methods
    }
}
