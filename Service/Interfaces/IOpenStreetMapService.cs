using Domain.Entities;
using Domain.Response;

namespace Service.Interfaces;

public interface IOpenStreetMapService //здесь не стал делать наследование т.к не работает сервис с моими запросами 
{
    // Для геокодирования использовать openstreetmap (бесплатно, формат запроса 
    // https://nominatim.openstreetmap.org/search?country={country}&city={City}&street={
    // Street}&format=json&limit=2 )
    Task<BaseResponse<string>> GetAddress(string country, string street, string city);
}