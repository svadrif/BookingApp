namespace Domain.Common
{
    public abstract class BaseEntity<TKey> : AuditableEntity, IHasKey<TKey>
    {
        public TKey Id { get; set; }
    }
}
