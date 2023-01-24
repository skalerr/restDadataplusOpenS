using Domain.HttpResult;

namespace Domain.Entities;

public class PositionModel
{
    public string Geo { get; set; }

    public double Lat { get; set; }
    public double Lon { get; set; }
    
    public List<Location> Locations { get; set; }
}