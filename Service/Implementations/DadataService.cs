using Dadata;
using Dadata.Model;
using Service.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Domain.HttpResult;
using Domain.Response;


namespace Service.Implementations;

public class DadataService : IDadataService
{
    public async Task<BaseResponse<PositionModel>> GetAddress(string geo)
    {
        var token = "1f5d452b294554d406b007d4e6e2e182b5ae1d8c1";
        var secret = "5fdd415eaa72b3aa07b2d219c5e2ca5eda3a85bc1";

        var api = new SuggestClientAsync(token);
        var result = await api.Geolocate(lat: 55.878, lon: 37.653);
        PositionModel positionModel = result.suggestions.Select(x => x?.value)
            .ToList().Select(x=> new PositionModel
        {
            Locations = result.suggestions.Select(v => new Location(){Value = v?.value}).ToList()
        }).FirstOrDefault();
        if (positionModel != null && positionModel.Locations.Select(x=>x.Value == null).FirstOrDefault())
        {
            return new BaseResponse<PositionModel>()
            {
                Data = null,
                IsSuccess = false,
                StatusCode = StatusCode.NotFound,
                Descripton = "Not Found"
            };
        }
        else
        {
            return new BaseResponse<PositionModel>()
            {
                Data = positionModel,
                IsSuccess = true,
                StatusCode = StatusCode.Ok,
                Descripton = "Ok"
            };
        }
    }
}