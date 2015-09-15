using System.Data.Entity;

namespace Framework.Data.EF
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly DbContext _context;

        public UnitOfWorkFactory(DbContext context)
        {
            _context = context;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_context);
        }
    }
}
