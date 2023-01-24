using Domain.Response;

namespace Service.Interfaces;

public interface IOpenStreetMapService
{
    // Для геокодирования использовать openstreetmap (бесплатно, формат запроса 
    // https://nominatim.openstreetmap.org/search?country={country}&city={City}&street={
    // Street}&format=json&limit=2 )
    Task<BaseResponse<string>> GetAddress(string country, string street, string city);
}