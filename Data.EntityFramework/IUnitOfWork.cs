using System;

namespace Framework.Data.EF
{
    public interface IUnitOfWork : IDisposable
    {
        void Flush();
    }
}
