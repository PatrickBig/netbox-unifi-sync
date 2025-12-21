using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.QueryFilters;

public sealed class IpamIpRangeFilter : NetBoxQueryFilter
{
}

public class IpamIpRangeFilterBuilder
    : NetBoxQueryFilterBuilder<IpamIpRangeFilterBuilder, IpamIpRangeFilter>
{
    public IpamIpRangeFilterBuilder()
    {
        Contact = Field<int[]>("contact", FilterOperator.N);
        ContactGroup = Field<string[]>("contact_group", FilterOperator.N);
        ContactRole = Field<int[]>("contact_role", FilterOperator.N);
        Contains = Field<string>("contains");
        Created = Field<DateTime>("created", FilterOperator.Empty, FilterOperator.Gt, FilterOperator.Gte, FilterOperator.Lt, FilterOperator.Lte, FilterOperator.N);
        CreatedByRequest = Field<Guid>("created_by_request");
        Description = Field<string[]>("description", FilterOperator.Empty, FilterOperator.Ic, FilterOperator.Ie, FilterOperator.Iew, FilterOperator.Iregex, FilterOperator.Isw, FilterOperator.N, FilterOperator.Nic, FilterOperator.Nie, FilterOperator.Niew, FilterOperator.Nisw, FilterOperator.Regex);
        EndAddress = Field<string[]>("end_address");
        Family = Field<float>("family");
        Id = Field<int[]>("id", FilterOperator.Empty, FilterOperator.Gt, FilterOperator.Gte, FilterOperator.Lt, FilterOperator.Lte, FilterOperator.N);
        LastUpdated = Field<DateTime[]>("last_updated", FilterOperator.Empty, FilterOperator.Gt, FilterOperator.Gte, FilterOperator.Lt, FilterOperator.Lte, FilterOperator.N);
        MarkPopulated = Field<bool>("mark_populated");
        MarkUtilized = Field<bool>("mark_utilized");
        ModifiedByRequest = Field<Guid>("modified_by_request");
        Ordering = Field<string>("ordering");
        Parent = Field<string[]>("parent");
        Query = Field<string>("q");
        RoleSlug = Field<string[]>("role", FilterOperator.N);
        RoleId = Field<int[]>("role_id", FilterOperator.N);
        Size = Field<int[]>("size", FilterOperator.Empty, FilterOperator.Gt, FilterOperator.Gte, FilterOperator.Lt, FilterOperator.Lte, FilterOperator.N);
        StartAddress = Field<string[]>("start_address");
        Status = Field<string[]>("status", FilterOperator.Empty, FilterOperator.Ic, FilterOperator.Ie, FilterOperator.Iew, FilterOperator.Iregex, FilterOperator.Isw, FilterOperator.N, FilterOperator.Nic, FilterOperator.Nie, FilterOperator.Niew, FilterOperator.Nisw, FilterOperator.Regex);
        Tag = Field<string[]>("tag", FilterOperator.N);
        TagId = Field<string[]>("tag_id", FilterOperator.N);
        TenantSlug = Field<string[]>("tenant", FilterOperator.N);
        TenantGroup = Field<string[]>("tenant_group", FilterOperator.N);
        TenantGroupId = Field<string[]>("tenant_group_id", FilterOperator.N);
        TenantId = Field<int[]>("tenant_id", FilterOperator.N);
        UpdatedByRequest = Field<Guid>("updated_by_request");
        Vrf = Field<string[]>("vrf", FilterOperator.N);
        VrfId = Field<int[]>("vrf_id", FilterOperator.N);
    }

    public FilterField<IpamIpRangeFilterBuilder, int[]> Contact { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> ContactGroup { get; }

    public FilterField<IpamIpRangeFilterBuilder, int[]> ContactRole { get; }

    public FilterField<IpamIpRangeFilterBuilder, string> Contains { get; }

    public FilterField<IpamIpRangeFilterBuilder, DateTime> Created { get; }

    public FilterField<IpamIpRangeFilterBuilder, Guid> CreatedByRequest { get; }
    
    public FilterField<IpamIpRangeFilterBuilder, string[]> Description { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> EndAddress { get; }

    public FilterField<IpamIpRangeFilterBuilder, float> Family { get; }

    public FilterField<IpamIpRangeFilterBuilder, int[]> Id { get; }

    public FilterField<IpamIpRangeFilterBuilder, DateTime[]> LastUpdated { get; }

    public FilterField<IpamIpRangeFilterBuilder, bool> MarkPopulated { get; }

    public FilterField<IpamIpRangeFilterBuilder, bool> MarkUtilized { get; }

    public FilterField<IpamIpRangeFilterBuilder, Guid> ModifiedByRequest { get; }

    public FilterField<IpamIpRangeFilterBuilder, string> Ordering { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> Parent { get; }

    public FilterField<IpamIpRangeFilterBuilder, string> Query { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> RoleSlug { get; }

    public FilterField<IpamIpRangeFilterBuilder, int[]> RoleId { get; }

    public FilterField<IpamIpRangeFilterBuilder, int[]> Size { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> StartAddress { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> Status { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> Tag { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> TagId { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> TenantSlug { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> TenantGroup { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> TenantGroupId { get; }

    public FilterField<IpamIpRangeFilterBuilder, int[]> TenantId { get; }

    public FilterField<IpamIpRangeFilterBuilder, Guid> UpdatedByRequest { get; }

    public FilterField<IpamIpRangeFilterBuilder, string[]> Vrf { get; }

    public FilterField<IpamIpRangeFilterBuilder, int[]> VrfId { get; }
}
