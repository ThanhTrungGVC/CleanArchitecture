namespace Zata.Entities
{
    [Serializable]
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            //EntityHelper.TrySetTenantId(this);
        }

        public override String ToString() => $"[ENTITY: {GetType().Name}] Keys = {GetKeys().JoinAsString(", ")}";

        public abstract object?[] GetKeys();

        public bool EntityEquals(IEntity other) => EntityHelper.EntityEquals(this, other);
    }

    [Serializable]
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        public virtual TKey Id { get; protected set; } = default!;

        protected Entity()
        {
        }

        protected Entity(TKey id)
        {
            Id = id;
        }

        public override object?[] GetKeys() => new object?[] { Id };

        public override String ToString() => $"[ENTITY: {GetType().Name}] Id = {Id}";
    }
}
