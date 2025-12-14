using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Models.Unifi;
public class UnifiLoginRequest
{
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}
