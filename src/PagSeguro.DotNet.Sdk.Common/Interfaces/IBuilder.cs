namespace PagSeguro.DotNet.Sdk.Common.Interfaces
{
    public interface IBuilder<TTopLevelProvider, TEntity>
        where TEntity : class
        where TTopLevelProvider : IProvider
    {
        protected TEntity Entity { get; set; }

        public void Reset();

        public TTopLevelProvider Load(TEntity entity)
        {
            Entity = entity;
            return (TTopLevelProvider)this;
        }

        public TEntity Build()
        {
            TEntity entity = Entity;
            Reset();
            return entity;
        }
    }
}
