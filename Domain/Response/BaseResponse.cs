using Domain.Enum;
using Domain.Interfaces;

namespace Domain.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Descripton { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
}