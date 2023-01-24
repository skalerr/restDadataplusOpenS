using Domain.Entities;
using Domain.Response;

namespace Service.Interfaces;

public interface IDadataService
{
    Task<BaseResponse<PositionModel>> GetAddress(string address);
}