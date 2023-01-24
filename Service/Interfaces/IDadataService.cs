using Domain.Entities;
using Domain.Response;

namespace Service.Interfaces;

public interface IDadataService
{
    Task<BaseResponse<PositionModel>> GetAddress(PositionModel positionModel);
    Task<BaseResponse<PositionModel>> GetGeo(string country, string street, string city);
}