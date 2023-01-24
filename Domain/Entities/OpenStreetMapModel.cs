using System.Text.Json.Serialization;

namespace Domain.Entities;

public class OpenStreetMapModel
{
    [JsonPropertyName("place_id")]
    public string IdPlace { get; set; }
    [JsonPropertyName("licence")]
    public string Licence { get; set; }
    [JsonPropertyName("osm_type")]
    public string OsmType { get; set; }
    [JsonPropertyName("osm_id")]
    public string OsmId { get; set; }
    [JsonPropertyName("boundingbox")]
    public string[] Boundingbox { get; set; }
    [JsonPropertyName("lat")]
    public string Latitude { get; set; }
    [JsonPropertyName("lon")]
    public string Longitude { get; set; }
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }
    [JsonPropertyName("class")]
    public string Class { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("importance")]
    public string Importance { get; set; }
    
}