namespace netcoreTemplate.Domain.Base;

public class EntityBase<T> where T:struct
{
    public T Id { get; set; }
}
