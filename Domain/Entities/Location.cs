using System.Text.Json.Serialization;

namespace Domain.HttpResult;

public class Location
{
    [JsonPropertyName("region")]
    public string Region { get; set; }
    [JsonPropertyName("city")]
    public string City { get; set; }
    [JsonPropertyName("street")]
    public string Street { get; set; }
    [JsonPropertyName("house")]
    public string House { get; set; }
    [JsonPropertyName("flat")]
    public string Flat { get; set; }
    public string Geo { get; set; }
    public string Value { get; set; }
}