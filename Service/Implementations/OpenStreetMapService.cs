using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.Entities;
using Domain.Enum;
using Domain.Response;
using Newtonsoft.Json;
using Service.Interfaces;

namespace Service.Implementations;

public class OpenStreetMapService : IOpenStreetMapService
{
    private readonly HttpClient _httpClient;
    private readonly ILoggerMessage _logger;

    public OpenStreetMapService(HttpClient httpClient, ILoggerMessage logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        _httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
        
    }
    public async Task<BaseResponse<string>> GetAddress(string country, string street, string city)
    {
        var url = _httpClient.BaseAddress + $"search?country={country}&city={city}&street={street}&format=json&limit=2";
        IList<OpenStreetMapModel> response = null;

        try
        {
            //response = await _httpClient.GetFromJsonAsync<IList<OpenStreetMapModel>>(url);
            
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
            {
                request.Headers.TryAddWithoutValidation("authority", "nominatim.openstreetmap.org");
                request.Headers.TryAddWithoutValidation("accept", "application/json");
                request.Headers.TryAddWithoutValidation("accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.TryAddWithoutValidation("cache-control", "max-age=0");
                request.Headers.TryAddWithoutValidation("sec-ch-ua", "^^");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
                request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "^^");
                request.Headers.TryAddWithoutValidation("sec-fetch-dest", "document");
                request.Headers.TryAddWithoutValidation("sec-fetch-mode", "navigate");
                request.Headers.TryAddWithoutValidation("sec-fetch-site", "none");
                request.Headers.TryAddWithoutValidation("sec-fetch-user", "?1");
                request.Headers.TryAddWithoutValidation("upgrade-insecure-requests", "1");
                request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36"); 
            
                var result = await _httpClient.SendAsync(request);
                response = JsonConvert.DeserializeObject<IList<OpenStreetMapModel>>(request.ToString());
            
            }

            if (response != null)
            {
                
                string result = response.Select(x => new string(x.Longitude + " " + x.Latitude)).ToString();
                
                await _logger.AddLog(new Log
                {
                    Message = "GetAddress(Ok)",
                    StackTrace = null,
                    InnerException = null,
                    Source = null,
                    TargetSite = null,
                    Data = JsonConvert.SerializeObject(response),
                    HelpLink = null,
                    HResult = null,
                    Date = DateTime.Now.ToString(),
                });
                
                return new BaseResponse<string>()
                {
                    Data = result,
                    IsSuccess = true,
                    StatusCode = StatusCode.Ok,
                    Descripton = "Ok"
                };
            }
            else
            {
                await _logger.AddLog(new Log
                {
                    Message = "GetAddress(NotFound)",
                    StackTrace = null,
                    InnerException = null,
                    Source = null,
                    TargetSite = null,
                    Data = StatusCode.NotFound.ToString(),
                    HelpLink = null,
                    HResult = null,
                    Date = DateTime.Now.ToString(),
                });
                return new BaseResponse<string>()
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
                Message = "GetAddress(Error)",
                StackTrace = null,
                InnerException = e.InnerException?.ToString(),
                Source = e?.Source,
                TargetSite = e.TargetSite.ToString(),
                Data = StatusCode.NotFound.ToString(),
                HelpLink = e.HelpLink?.ToString(),
                HResult = e.HResult.ToString(),
                Date = DateTime.Now.ToString(),

            });
            return new BaseResponse<string>()
            {
                Data = null,
                IsSuccess = false,
                StatusCode = StatusCode.NotFound,
                Descripton = e.Message + e.InnerException + e.InnerException?.InnerException
            };
        }
        
        
    }
}