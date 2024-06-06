namespace MovieApp.Application.Entities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }
    }
}
