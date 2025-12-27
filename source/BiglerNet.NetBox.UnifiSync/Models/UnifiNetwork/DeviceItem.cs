using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;

public class DeviceItem
{
    [JsonPropertyName("required_version")]
    public string? RequiredVersion { get; set; }

    [JsonPropertyName("port_table")]
    public IEnumerable<DevicePort> Ports { get; set; } = Enumerable.Empty<DevicePort>();

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("config_network")]
    public DeviceNetworkConfig? NetworkConfig { get; set; }

    [JsonPropertyName("ip")]
    public string Ip { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }
}

public class DeviceNetworkConfig
{
    [JsonPropertyName("ip")]
    public string IpAddress { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class DevicePort
{
    [JsonPropertyName("port_idx")]
    public int PortIdx { get; set; }

    [JsonPropertyName("op_mode")]
    public string OperationMode { get; set; }

    [JsonPropertyName("forward")]
    public string? Forward { get; set; }

    [JsonPropertyName("attr_no_edit")]
    public bool AttrNoEdit { get; set; }

    [JsonPropertyName("autoneg")]
    public bool AutoNegotiate { get; set; }

    [JsonPropertyName("enable")]
    public bool Enabled { get; set; }

    [JsonPropertyName("flowctrl_rx")]
    public bool FlowControlReceive { get; set; }

    [JsonPropertyName("flowctrl_tx")]
    public bool FlowControlTransmit { get; set; }

    [JsonPropertyName("is_uplink")]
    public bool IsUplink { get; set; }

    [JsonPropertyName("jumbo")]
    public bool Jumbo { get; set; }

    [JsonPropertyName("mac_table_count")]
    public int MacTableCount { get; set; }

    [JsonPropertyName("media")]
    public string Media { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("poe_caps")]
    public int PoeCaps { get; set; }

    [JsonPropertyName("port_poe")]
    public bool PortPoe { get; set; }

    [JsonPropertyName("rx_broadcast")]
    public long RxBroadcast { get; set; }

    [JsonPropertyName("rx_bytes")]
    public long RxBytes { get; set; }

    [JsonPropertyName("rx_dropped")]
    public long RxDropped { get; set; }

    [JsonPropertyName("rx_errors")]
    public long RxErrors { get; set; }

    [JsonPropertyName("rx_multicast")]
    public long RxMulticast { get; set; }

    [JsonPropertyName("rx_packets")]
    public long RxPackets { get; set; }

    [JsonPropertyName("speed")]
    public int Speed { get; set; }

    [JsonPropertyName("stp_pathcost")]
    public int StpPathCost { get; set; }

    [JsonPropertyName("stp_state")]
    public string StpState { get; set; }

    [JsonPropertyName("tx_broadcast")]
    public long TxBroadcast { get; set; }

    [JsonPropertyName("tx_bytes")]
    public long TxBytes { get; set; }

    [JsonPropertyName("tx_dropped")]
    public long TxDropped { get; set; }

    [JsonPropertyName("tx_errors")]
    public long TxErrors { get; set; }

    [JsonPropertyName("tx_multicast")]
    public long TxMulticast { get; set; }

    [JsonPropertyName("tx_packets")]
    public long TxPackets { get; set; }

    [JsonPropertyName("masked")]
    public bool Masked { get; set; }

    [JsonPropertyName("aggregated_by")]
    public bool AggregatedBy { get; set; }

    [JsonPropertyName("up")]
    public bool Up { get; set; }
}
