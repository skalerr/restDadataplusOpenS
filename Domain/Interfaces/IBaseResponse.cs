namespace Domain.Interfaces;

public interface IBaseResponse<T>
{
    T Data { get; }
}