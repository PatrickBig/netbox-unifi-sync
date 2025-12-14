namespace BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;

public class AggregatedDashboardResponse
{
    public Dashboard_Meta? dashboard_meta { get; set; }
    public Wifi_Channel_Preset_Configuration? wifi_channel_preset_configuration { get; set; }
    public Wan? wan { get; set; }
    public Wan_Activity? wan_activity { get; set; }
    public Wifi_Tx_Retries? wifi_tx_retries { get; set; }
    public Wifi_Connectivity? wifi_connectivity { get; set; }
    public Traffic_Flow_Stats? traffic_flow_stats { get; set; }
    public Wifi_Activity? wifi_activity { get; set; }
    public Most_Active_Apps_Aps_Clients? most_active_apps_aps_clients { get; set; }
    public Ap_Radio_Density? ap_radio_density { get; set; }
    public Critical_Traffic_Prioritization? critical_traffic_prioritization { get; set; }
    public Next_Ai? next_ai { get; set; }
    public Gateway? gateway { get; set; }
    public Cybersecure? cybersecure { get; set; }
    public Connectivity_Status? connectivity_status { get; set; }
    public Wan_History? wan_history { get; set; }
    public Speed_Test? speed_test { get; set; }
    public System_Status? system_status { get; set; }
    public Most_Common_Client_Fingerprints? most_common_client_fingerprints { get; set; }
}

public class Dashboard_Meta
{
    public long end_timestamp { get; set; }
    public string? layout { get; set; }
    public long start_timestamp { get; set; }
    public string[]? widgets { get; set; }
}

public class Wifi_Channel_Preset_Configuration
{
    public string? channel_preset_type { get; set; }
    public Radios_Configuration[]? radios_configuration { get; set; }
}

public class Radios_Configuration
{
    public string? channel_width { get; set; }
    public bool dfs_enabled { get; set; }
    public string? radio { get; set; }
}

public class Wan
{
    public Wan_Details[]? wan_details { get; set; }
}

public class Wan_Details
{
    public Isp? isp { get; set; }
    public string? network_group { get; set; }
    public string? network_name { get; set; }
    public Stats? stats { get; set; }
    public Status? status { get; set; }
}

public class Isp
{
    public int asn { get; set; }
    public Capabilities? capabilities { get; set; }
    public string? name { get; set; }
}

public class Capabilities
{
    public int download_kilobits_per_second { get; set; }
    public int upload_kilobits_per_second { get; set; }
}

public class Stats
{
    public Activity? activity { get; set; }
    public long monthly_bytes { get; set; }
    public Service_Latencies[]? service_latencies { get; set; }
}

public class Activity
{
    public int max_rx_bytesr { get; set; }
    public int max_tx_bytesr { get; set; }
    public int rx_bytesr { get; set; }
    public int tx_bytesr { get; set; }
}

public class Service_Latencies
{
    public int latency { get; set; }
    public string? service_name { get; set; }
}

public class Status
{
    public string? interface_name { get; set; }
    public bool is_active { get; set; }
    public string? state { get; set; }
    public bool up { get; set; }
    public string? uplink_type { get; set; }
    public int downtime { get; set; }
}

public class Wan_Activity
{
    public Activity_By_Network_Group? activity_by_network_group { get; set; }
    public Network_Groups[]? network_groups { get; set; }
    public Total_Activity? total_activity { get; set; }
}

public class Activity_By_Network_Group
{
    public WAN? WAN { get; set; }
    public WAN? WAN2 { get; set; }
}

public class WAN
{
    public History[]? history { get; set; }
    public Summary? summary { get; set; }
}

public class Summary
{
    public long rx_bytes { get; set; }
    public long tx_bytes { get; set; }
}

public class History
{
    public float? avg_latency_ms { get; set; }
    public float? avg_packet_loss_pct { get; set; }
    public float? avg_rx_rate_bps { get; set; }
    public float? avg_tx_rate_bps { get; set; }
    public int? client_count { get; set; }
    public float? max_latency_ms { get; set; }
    public float? max_rx_rate_bps { get; set; }
    public float? max_tx_rate_bps { get; set; }
    public float? min_latency_ms { get; set; }
    public long? timestamp { get; set; }
}


public class Total_Activity
{
    public History[]? history { get; set; }
    public Summary? summary { get; set; }
}

public class Network_Groups
{
    public int[]? isp_asn { get; set; }
    public string[]? isp_name { get; set; }
    public string? name { get; set; }
}

public class Wifi_Tx_Retries
{
    public Category[]? categories { get; set; }
    public int total_ap_count { get; set; }
}

public class Category
{
    public int? band_count { get; set; }
    public int? category_id { get; set; }
    public Wifi_Tx_Retry_Stats[]? wifi_tx_retry_stats { get; set; }
}

public class Wifi_Tx_Retry_Stats
{
    public int? ap_count { get; set; }
    public float? avg_tx_retry_percent { get; set; }
    public string? radio { get; set; }
}

public class Wifi_Connectivity
{
    public required Radio_Connectivity[] radio_connectivity { get; set; }
}

public class Radio_Connectivity
{
    public Attempts? attempts { get; set; }
    public string? radio_filter { get; set; }
}

public class Attempts
{
    public float? association_ratio { get; set; }
    public float? authentication_ratio { get; set; }
    public float? dhcp_ratio { get; set; }
    public float? dns_ratio { get; set; }
    public int? failed_client_connections { get; set; }
    public float? success_ratio { get; set; }
    public int? total_attempts { get; set; }
}

public class Traffic_Flow_Stats
{
    public Metadata? metadata { get; set; }
    public Summary? summary { get; set; }
    public History3[]? history { get; set; }
}

public class Metadata
{
    public string[]? counters { get; set; }
}

public class History3
{
    public long? timestamp { get; set; }
    public required int[] allowed { get; set; }
    public required int[] blocked { get; set; }
}

public class Wifi_Activity
{
    public By_Radio_Band? by_radio_band { get; set; }
    public History6[]? history { get; set; }
    public long? rx_bytes { get; set; }
    public int? tx_bytes { get; set; }
}

public class By_Radio_Band
{
    public Na? na { get; set; }
    public Ng? ng { get; set; }
}

public class Na
{
    public History4[]? history { get; set; }
    public long? rx_bytes { get; set; }
    public int? tx_bytes { get; set; }
}

public class History4
{
    public float max_tx_retry_percentage { get; set; }
    public int num_sta { get; set; }
    public int rx_byter { get; set; }
    public long timestamp { get; set; }
    public int tx_byter { get; set; }
    public float tx_retry_percentage { get; set; }
}

public class Ng
{
    public History5[]? history { get; set; }
    public int rx_bytes { get; set; }
    public int tx_bytes { get; set; }
}

public class History5
{
    public float max_tx_retry_percentage { get; set; }
    public int num_sta { get; set; }
    public int rx_byter { get; set; }
    public long timestamp { get; set; }
    public int tx_byter { get; set; }
    public float tx_retry_percentage { get; set; }
}

public class History6
{
    public float max_tx_retry_percentage { get; set; }
    public int num_sta { get; set; }
    public int rx_byter { get; set; }
    public long timestamp { get; set; }
    public int tx_byter { get; set; }
    public float tx_retry_percentage { get; set; }
}

public class Most_Active_Apps_Aps_Clients
{
    public Usage_By[]? usage_by { get; set; }
}

public class Usage_By
{
    public int application { get; set; }
    public int category { get; set; }
    public long received_bytes { get; set; }
    public long total_bytes { get; set; }
    public long transmitted_bytes { get; set; }
    public string? type { get; set; }
    public string? display_name { get; set; }
    public Fingerprint? fingerprint { get; set; }
    public string? hostname { get; set; }
    public string? mac { get; set; }
    public string? oui { get; set; }
    public int satisfaction { get; set; }
    public Ap_Details? ap_details { get; set; }
}

public class Fingerprint
{
    public int computed_dev_id { get; set; }
    public int computed_engine { get; set; }
    public int confidence { get; set; }
    public int dev_cat { get; set; }
    public int dev_family { get; set; }
    public int dev_id { get; set; }
    public int dev_vendor { get; set; }
    public bool has_override { get; set; }
    public int os_name { get; set; }
    public int dev_id_override { get; set; }
    public int os_class { get; set; }
}

public class Ap_Details
{
    public string? model { get; set; }
    public string? name { get; set; }
    public string? type { get; set; }
}

public class Ap_Radio_Density
{
    public Density_Details[]? density_details { get; set; }
}

public class Density_Details
{
    public Device_Meta? device_meta { get; set; }
    public Radio_Groups[]? radio_groups { get; set; }
}

public class Device_Meta
{
    public string? mac { get; set; }
    public string? model { get; set; }
    public string? name { get; set; }
    public string? type { get; set; }
}

public class Radio_Groups
{
    public int clients_count { get; set; }
    public int phy_rate_max { get; set; }
    public int signal_avg { get; set; }
    public float total_traffic_percentage { get; set; }
    public float tx_retries_percentage { get; set; }
    public int weakest_clients_signal_avg { get; set; }
    public string? radio_filter { get; set; }
}

public class Critical_Traffic_Prioritization
{
    public object[]? enabled_critical_applications { get; set; }
    public int[]? predefined_critical_applications { get; set; }
}

public class Next_Ai
{
}

public class Gateway
{
    public string? hardware_uuid { get; set; }
    public string? mac { get; set; }
    public string? model { get; set; }
    public string? name { get; set; }
    public string? type { get; set; }
}

public class Cybersecure
{
    public bool enterprise { get; set; }
    public bool has_subscription { get; set; }
    public bool ips_enabled { get; set; }
    public bool is_activating { get; set; }
    public long scanned_bytes { get; set; }
    public int signature_capacity { get; set; }
    public int signatures { get; set; }
    public int threats { get; set; }
    public long updated_timestamp { get; set; }
}

public class Connectivity_Status
{
    public Connection_Types[]? connection_types { get; set; }
}

public class Connection_Types
{
    public string? search_tag { get; set; }
    public int total_count { get; set; }
    public string? type { get; set; }
}

public class Wan_History
{
    public Wan_History_Details[]? wan_history_details { get; set; }
}

public class Wan_History_Details
{
    public object[]? downtime_history { get; set; }
    public Health_History[]? health_history { get; set; }
    public string? network_group { get; set; }
    public float uptime { get; set; }
}

public class Health_History
{
    public bool failover_wan_active { get; set; }
    public bool high_latency { get; set; }
    public bool packet_loss { get; set; }
    public long timestamp { get; set; }
    public bool wan2_failover_active { get; set; }
    public bool wan_downtime { get; set; }
}

public class Speed_Test
{
    public Datum[]? data { get; set; }
    public bool is_supported { get; set; }
}

public class Datum
{
    public int download_mbps { get; set; }
    public string? id { get; set; }
    public string? interface_name { get; set; }
    public int latency_ms { get; set; }
    public string? network_conf_id { get; set; }
    public long time { get; set; }
    public int upload_mbps { get; set; }
    public string? wan_networkgroup { get; set; }
    public Wan_Provider_Capabilities? wan_provider_capabilities { get; set; }
}

public class Wan_Provider_Capabilities
{
    public int download_kilobits_per_second { get; set; }
    public int upload_kilobits_per_second { get; set; }
}

public class System_Status
{
    public string? gateway_ip { get; set; }
    public int system_uptime { get; set; }
    public Wan_Groups[]? wan_groups { get; set; }
}

public class Wan_Groups
{
    public string? ip { get; set; }
    public string? wan_group { get; set; }
}

public class Most_Common_Client_Fingerprints
{
    public Fingerprint_Counters[]? fingerprint_counters { get; set; }
}

public class Fingerprint_Counters
{
    public int count { get; set; }
    public Fingerprint1? fingerprint { get; set; }
}

public class Fingerprint1
{
    public int computed_dev_id { get; set; }
    public int computed_engine { get; set; }
    public int dev_cat { get; set; }
    public int dev_family { get; set; }
    public int dev_vendor { get; set; }
    public int os_name { get; set; }
    public int os_class { get; set; }
}
