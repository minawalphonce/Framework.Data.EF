using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Framework.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private DbContext _dbContext;

        #endregion Fields

        #region Constructors

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        #endregion Constructors

        #region Methods

        public void Flush()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new RepositoryException(e);
            }
        }

        public void Dispose()
        {
            Flush();
        }

        #endregion Methods

      
    }
}