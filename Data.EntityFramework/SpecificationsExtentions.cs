namespace Framework.Data.EF
{
    public static class SpecificationExtention
    {
        public static ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> spec1, ISpecification<TEntity> spec2)
            where TEntity: class
        {
            return new Specification<TEntity>(spec1.Expression.Or(spec2.Expression));
        }

        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> spec1, ISpecification<TEntity> spec2)
            where TEntity : class
        {
            return new Specification<TEntity>(spec1.Expression.And(spec2.Expression));
        }
    }
}
