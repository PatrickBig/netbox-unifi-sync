using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client;

public interface ITenancyClient
{
    public Task<Tenant> CreateTenantAsync(TenantRequest request, CancellationToken cancellationToken = default);
}
