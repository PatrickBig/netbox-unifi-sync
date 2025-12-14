using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Models.Unifi;
public class DeviceDetails
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("model")]
    public required string Model { get; set; }

    [JsonPropertyName("supported")]
    public bool Supported { get; set; }

    [JsonPropertyName("macAddress")]
    public required string MacAddress { get; set; }

    [JsonPropertyName("ipAddress")]
    public required string IpAddress { get; set; }

    [JsonPropertyName("state")]
    public required string State { get; set; }

    [JsonPropertyName("firmwareVersion")]
    public required string FirmwareVersion { get; set; }

    [JsonPropertyName("firmwareUpdatable")]
    public bool FirmwareUpdatable { get; set; }

    [JsonPropertyName("adoptedAt")]
    public DateTime AdoptedAt { get; set; }

    [JsonPropertyName("provisionedAt")]
    public DateTime ProvisionedAt { get; set; }

    [JsonPropertyName("configurationId")]
    public required string ConfigurationId { get; set; }

    [JsonPropertyName("uplink")]
    public UplinkDetails? Uplink { get; set; }

    [JsonPropertyName("features")]
    public required FeatureDetails Features { get; set; }

    [JsonPropertyName("interfaces")]
    public required InterfaceDetails Interfaces { get; set; }
}

public class UplinkDetails
{
    [JsonPropertyName("deviceId")]
    public Guid DeviceId { get; set; }
}


public class FeatureDetails
{
    [JsonPropertyName("switching")]
    public required Switching Switching { get; set; }

    [JsonPropertyName("accessPoint")]
    public required Accesspoint AccessPoint { get; set; }
}


public class Switching
{
    [JsonExtensionData]
    public IDictionary<string, object?> Data { get; set; } = new Dictionary<string, object?>();
}

public class Accesspoint
{
    [JsonExtensionData]
    public IDictionary<string, object?> Data { get; set; } = new Dictionary<string, object?>();
}

public class InterfaceDetails
{
    [JsonPropertyName("ports")]
    public Port[]? Ports { get; set; }

    [JsonPropertyName("radios")]
    public Radio[]? Radios { get; set; }
}


public class Port
{
    [JsonPropertyName("idx")]
    public int Idx { get; set; }

    [JsonPropertyName("state")]
    public required string State { get; set; }

    [JsonPropertyName("connector")]
    public required string Connector { get; set; }

    [JsonPropertyName("maxSpeedMbps")]
    public int MaxSpeedMbps { get; set; }

    [JsonPropertyName("speedMbps")]
    public int SpeedMbps { get; set; }

    [JsonPropertyName("poe")]
    public Poe? Poe { get; set; }
}


public class Poe
{
    [JsonPropertyName("standard")]
    public required string Standard { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("state")]
    public required string State { get; set; }
}


public class Radio
{
    [JsonPropertyName("wlanStandard")]
    public required string WlanStandard { get; set; }

    [JsonPropertyName("frequencyGHz")]
    public float FrequencyGHz { get; set; }

    [JsonPropertyName("channelWidthMHz")]
    public int ChannelWidthMHz { get; set; }

    [JsonPropertyName("channel")]
    public int Channel { get; set; }
}