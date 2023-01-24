using Domain.HttpResult;

namespace Domain.Entities;

public class PositionModel
{
    public string Geo { get; set; }
    
    public List<Location> Locations { get; set; }
}