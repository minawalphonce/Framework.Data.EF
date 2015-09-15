namespace Framework.Data.EF
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
