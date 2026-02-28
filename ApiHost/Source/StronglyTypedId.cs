namespace MyApiClientGenerateSample;

public interface IStronglyTypedId<out TSource>
{
    TSource Id { get; }
}

public interface IInt64StronglyTypedId : IStronglyTypedId<long>
{
}