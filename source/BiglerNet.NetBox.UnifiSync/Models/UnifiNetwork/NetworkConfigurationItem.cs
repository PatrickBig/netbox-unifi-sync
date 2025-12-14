using System.Text.Json.Serialization;

namespace BiglerNet.NetBox.UnifiSync.Models.UnifiNetwork;

public class NetworkConfigurationItem : ModelBase
{
    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("site_id")]
    public string SiteId { get; set; } = string.Empty;

    [JsonPropertyName("purpose")]
    public string Purpose { get; set; } = string.Empty;

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }



    [JsonPropertyName("is_nat")]
    public bool? IsNat { get; set; }

    [JsonPropertyName("vlan_enabled")]
    public bool? VlanEnabled { get; set; }


    [JsonPropertyName("domain_name")]
    public string? DomainName { get; set; }

    [JsonPropertyName("dhcpd_enabled")]
    public bool? DhcpdEnabled { get; set; }

    [JsonPropertyName("dhcpd_start")]
    public string? DhcpdStart { get; set; }

    [JsonPropertyName("dhcpd_stop")]
    public string? DhcpdStop { get; set; }

    [JsonPropertyName("dhcpd_leasetime")]
    public int? DhcpdLeasetime { get; set; }

    [JsonPropertyName("dhcpd_gateway_enabled")]
    public bool? DhcpdGatewayEnabled { get; set; }

    [JsonPropertyName("dhcpd_dns_enabled")]
    public bool? DhcpdDnsEnabled { get; set; }

    [JsonPropertyName("dhcpd_dns_1")]
    public string? DhcpdDns1 { get; set; }

    [JsonPropertyName("dhcpd_dns_2")]
    public string? DhcpdDns2 { get; set; }

    [JsonPropertyName("dhcpd_dns_3")]
    public string? DhcpdDns3 { get; set; }

    [JsonPropertyName("dhcp_relay_enabled")]
    public bool? DhcpRelayEnabled { get; set; }

    [JsonPropertyName("dhcpd_unifi_controller")]
    public string? DhcpdUnifiController { get; set; }

    [JsonPropertyName("dhcpd_conflict_checking")]
    public bool? DhcpdConflictChecking { get; set; }

    [JsonPropertyName("dhcpd_time_offset_enabled")]
    public bool? DhcpdTimeOffsetEnabled { get; set; }

    [JsonPropertyName("dhcpd_wpad_url")]
    public string? DhcpdWpadUrl { get; set; }

    [JsonPropertyName("dhcpd_boot_enabled")]
    public bool? DhcpdBootEnabled { get; set; }

    [JsonPropertyName("dhcpd_ntp_enabled")]
    public bool? DhcpdNtpEnabled { get; set; }

    [JsonPropertyName("dhcpd_wins_enabled")]
    public bool? DhcpdWinsEnabled { get; set; }

    [JsonPropertyName("dhcpd_tftp_server")]
    public string? DhcpdTftpServer { get; set; }

    [JsonPropertyName("dhcpdv6_start")]
    public string? Dhcpdv6Start { get; set; }

    [JsonPropertyName("dhcpdv6_stop")]
    public string? Dhcpdv6Stop { get; set; }

    [JsonPropertyName("dhcpdv6_leasetime")]
    public int? Dhcpdv6Leasetime { get; set; }

    [JsonPropertyName("dhcpdv6_allow_slaac")]
    public bool? Dhcpdv6AllowSlaac { get; set; }



    [JsonPropertyName("ip_subnet")]
    public string? IpSubnet { get; set; }

    [JsonPropertyName("setting_preference")]
    public string? SettingPreference { get; set; }

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("wan_type_v6")]
    public string? WanTypeV6 { get; set; }

    [JsonPropertyName("routing_table_id")]
    public int? RoutingTableId { get; set; }

    [JsonPropertyName("mac_override")]
    public string? MacOverride { get; set; }

    [JsonPropertyName("wan_dhcp_options")]
    public List<object> WanDhcpOptions { get; set; } = new();

    [JsonPropertyName("ipv6_wan_delegation_type")]
    public string? Ipv6WanDelegationType { get; set; }

    [JsonPropertyName("wan_dhcpv6_pd_size_auto")]
    public bool? WanDhcpv6PdSizeAuto { get; set; }

    [JsonPropertyName("firewall_zone_id")]
    public string? FirewallZoneId { get; set; }

    [JsonPropertyName("igmp_proxy_upstream")]
    public bool? IgmpProxyUpstream { get; set; }

    [JsonPropertyName("mac_override_enabled")]
    public bool? MacOverrideEnabled { get; set; }

    [JsonPropertyName("wan_load_balance_type")]
    public string? WanLoadBalanceType { get; set; }

    [JsonPropertyName("ipv6_setting_preference")]
    public string? Ipv6SettingPreference { get; set; }

    [JsonPropertyName("wan_failover_priority")]
    public int? WanFailoverPriority { get; set; }

    [JsonPropertyName("wan_ipv6_dns_preference")]
    public string? WanIpv6DnsPreference { get; set; }

    [JsonPropertyName("single_network_lan")]
    public string? SingleNetworkLan { get; set; }

    [JsonPropertyName("wan_ipv6_dns1")]
    public string? WanIpv6Dns1 { get; set; }

    [JsonPropertyName("wan_ipv6_dns2")]
    public string? WanIpv6Dns2 { get; set; }

    [JsonPropertyName("wan_networkgroup")]
    public string? WanNetworkgroup { get; set; }

    [JsonPropertyName("wan_provider_capabilities")]
    public WanProviderCapabilities WanProviderCapabilities { get; set; } = new();

    [JsonPropertyName("wan_ip_aliases")]
    public List<object> WanIpAliases { get; set; } = new();

    [JsonPropertyName("wan_smartq_enabled")]
    public bool? WanSmartqEnabled { get; set; }

    [JsonPropertyName("wan_dns_preference")]
    public string? WanDnsPreference { get; set; }

    [JsonPropertyName("wan_vlan_enabled")]
    public bool? WanVlanEnabled { get; set; }

    [JsonPropertyName("wan_load_balance_weight")]
    public int? WanLoadBalanceWeight { get; set; }

    [JsonPropertyName("vlan")]
    public int? Vlan { get; set; }


    [JsonPropertyName("report_wan_event")]
    public bool? ReportWanEvent { get; set; }

    [JsonPropertyName("ipv6_enabled")]
    public bool? Ipv6Enabled { get; set; }



    [JsonPropertyName("attr_no_delete")]
    public bool? AttrNoDelete { get; set; }

    [JsonPropertyName("wan_type")]
    public string? WanType { get; set; }

    [JsonPropertyName("attr_hidden_id")]
    public string? AttrHiddenId { get; set; }

    [JsonPropertyName("dhcpdv6_dns_auto")]
    public bool? Dhcpdv6DnsAuto { get; set; }

    [JsonPropertyName("ipv6_pd_stop")]
    public string? Ipv6PdStop { get; set; }



    [JsonPropertyName("ipv6_client_address_assignment")]
    public string? Ipv6ClientAddressAssignment { get; set; }



    [JsonPropertyName("ipv6_ra_enabled")]
    public bool? Ipv6RaEnabled { get; set; }








    [JsonPropertyName("ipv6_interface_type")]
    public string? Ipv6InterfaceType { get; set; }


    [JsonPropertyName("ipv6_ra_preferred_lifetime")]
    public int? Ipv6RaPreferredLifetime { get; set; }


    [JsonPropertyName("internet_access_enabled")]
    public bool? InternetAccessEnabled { get; set; }

    [JsonPropertyName("nat_outbound_ip_addresses")]
    public List<object> NatOutboundIpAddresses { get; set; } = new();


    [JsonPropertyName("ipv6_pd_auto_prefixid_enabled")]
    public bool? Ipv6PdAutoPrefixidEnabled { get; set; }


    [JsonPropertyName("lte_lan_enabled")]
    public bool? LteLanEnabled { get; set; }

    [JsonPropertyName("igmp_snooping")]
    public bool? IgmpSnooping { get; set; }

    [JsonPropertyName("dhcpguard_enabled")]
    public bool? DhcpguardEnabled { get; set; }






    [JsonPropertyName("networkgroup")]
    public string? Networkgroup { get; set; }




    [JsonPropertyName("gateway_type")]
    public string? GatewayType { get; set; }

    [JsonPropertyName("ipv6_ra_priority")]
    public string? Ipv6RaPriority { get; set; }



    [JsonPropertyName("ipv6_pd_start")]
    public string? Ipv6PdStart { get; set; }

    [JsonPropertyName("upnp_lan_enabled")]
    public bool? UpnpLanEnabled { get; set; }


    [JsonPropertyName("mdns_enabled")]
    public bool? MdnsEnabled { get; set; }



    [JsonPropertyName("auto_scale_enabled")]
    public bool? AutoScaleEnabled { get; set; }

    [JsonPropertyName("x_wireguard_private_key")]
    public string? XWireguardPrivateKey { get; set; }

    [JsonPropertyName("local_port")]
    public int? LocalPort { get; set; }

    [JsonPropertyName("wireguard_local_wan_ip")]
    public string? WireguardLocalWanIp { get; set; }

    [JsonPropertyName("vpn_type")]
    public string? VpnType { get; set; }

    [JsonPropertyName("wireguard_interface")]
    public string? WireguardInterface { get; set; }

    [JsonPropertyName("wireguard_public_key")]
    public string? WireguardPublicKey { get; set; }

    [JsonPropertyName("wireguard_id")]
    public int? WireguardId { get; set; }

    [JsonPropertyName("vpn_client_configuration_remote_ip_override")]
    public string? VpnClientConfigurationRemoteIpOverride { get; set; }
}

public class WanProviderCapabilities
{
    [JsonPropertyName("upload_kilobits_per_second")]
    public long UploadKilobitsPerSecond { get; set; }

    [JsonPropertyName("download_kilobits_per_second")]
    public long DownloadKilobitsPerSecond { get; set; }
}