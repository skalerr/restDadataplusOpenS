using System.Globalization;
using Dadata;
using Dadata.Model;
using Service.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Domain.HttpResult;
using Domain.Response;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace Service.Implementations;

public class DadataService : IDadataService
{
    private readonly ILoggerMessager _logger;
    private readonly IOptions<DaDataConfig> _config;
    public DadataService(ILoggerMessager logger, IOptions<DaDataConfig> config)
    {
        _logger = logger;
        _config = config;
    }

    public async Task<BaseResponse<PositionModel>> GetAddress(PositionModel positionModel)
    {
        var api = new SuggestClientAsync(_config.Value.Token);
        try
        {
            var result = await api.Geolocate(lat: positionModel.Lat,
                lon: positionModel.Lon,
                1000,
                10);
        
            positionModel = result.suggestions.Select(x => x?.value)
                .AsEnumerable().Select(x=> new PositionModel { Locations = result.suggestions.Select(v => new Location()
                    {
                        City = v?.data?.city,
                        Flat = v?.data?.flat,
                        House = v?.data?.house,
                        Region = v?.data?.region,
                        Street = v?.data?.street,
                        Value = v?.value,
                        Geo = v?.data?.geo_lat + " " + v?.data?.geo_lon
                    }).ToList()
                }).FirstOrDefault();

            if (positionModel == null || positionModel.Locations.Any(x=>x?.Value == null))
            {
                await _logger.AddLog(new Log
                {
                    Message = "DaDataService/GetAddress(NotFound)",
                    StackTrace = null,
                    InnerException = null,
                    Source = null,
                    TargetSite = null,
                    Data = StatusCode.NotFound.ToString(),
                    HelpLink = null,
                    HResult = null,
                    Date = DateTime.Now.ToString(),

                });
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
                await _logger.AddLog(new Log
                {
                    Message = "DaDataService/GetAddress(Ok)",
                    StackTrace = null,
                    InnerException = null,
                    Source = null,
                    TargetSite = null,
                    Data = JsonConvert.SerializeObject(positionModel),
                    HelpLink = null,
                    HResult = null,
                    Date = DateTime.Now.ToString(),
                });
                return new BaseResponse<PositionModel>()
                {
                    Data = positionModel,
                    IsSuccess = true,
                    StatusCode = StatusCode.Ok,
                    Descripton = "Ok"
                };
            }
        }
        catch (Exception e)
        {
            await _logger.AddLog(new Log
            {
                Message = "DaDataService/GetAddress(Error)",
                StackTrace = null,
                InnerException = e.InnerException?.ToString(),
                Source = e?.Source,
                TargetSite = e.TargetSite.ToString(),
                Data = StatusCode.NotFound.ToString(),
                HelpLink = e.HelpLink?.ToString(),
                HResult = e.HResult.ToString(),
                Date = DateTime.Now.ToString(),

            });
            return new BaseResponse<PositionModel>()
            {
                Data = null,
                IsSuccess = false,
                StatusCode = StatusCode.NotFound,
                Descripton = e.Message + e.InnerException + e.InnerException?.InnerException
            };
        }
        
    }

    public async Task<BaseResponse<PositionModel>> GetGeo(string country, string street, string city)
    {
        var api = new CleanClientAsync(_config.Value.Token, _config.Value.Secret);
        try
        {
            var result = await api.Clean<Address>($"{country } + {street } + {city}");
            if (result != null)
            {
                var data = new BaseResponse<PositionModel>()
                {
                    Data = new PositionModel()
                    {
                        Geo = result.geo_lat + " " + result.geo_lon,
                        Lat = Convert.ToDouble(result.geo_lat.Replace(".", ",")),
                        Lon = Convert.ToDouble(result.geo_lon.Replace(".", ","))
                    },
                    IsSuccess = true,
                    StatusCode = StatusCode.Ok,
                    Descripton = "Ok"
                };
                await _logger.AddLog(new Log
                {
                    Message = "GetGeo(Ok)",
                    StackTrace = null,
                    InnerException = null,
                    Source = null,
                    TargetSite = null,
                    Data = JsonConvert.SerializeObject(data),
                    HelpLink = null,
                    HResult = null,
                    Date = DateTime.Now.ToString(),

                });
                return data;
                
            }
            else
            {
                await _logger.AddLog(new Log
                {
                    Message = "GetGeo(Not Found)",
                    StackTrace = null,
                    InnerException = null,
                    Source = null,
                    TargetSite = null,
                    Data = StatusCode.NotFound.ToString(),
                    HelpLink = null,
                    HResult = null,
                    Date = DateTime.Now.ToString(),

                });
                
                return new BaseResponse<PositionModel>()
                {
                    Data = null,
                    IsSuccess = false,
                    StatusCode = StatusCode.NotFound,
                    Descripton = "Not Found"
                };
            }
        }
        catch (Exception e)
        {
            await _logger.AddLog(new Log
            {
                Message = "GetGeo(Error)",
                StackTrace = null,
                InnerException = e.InnerException?.ToString(),
                Source = e?.Source,
                TargetSite = e.TargetSite.ToString(),
                Data = StatusCode.NotFound.ToString(),
                HelpLink = e.HelpLink?.ToString(),
                HResult = e.HResult.ToString(),
                Date = DateTime.Now.ToString(),

            });
            return new BaseResponse<PositionModel>()
            {
                Data = null,
                IsSuccess = false,
                StatusCode = StatusCode.NotFound,
                Descripton = e.Message + e.InnerException + e.InnerException?.InnerException
            };
        }
    }
}