using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.UnifiSync.Services;

public abstract class UnifiSiteResourceManagerBase
{
    public abstract Task<int> SyncronizeUnifiResourcesAsync(string unifiSiteId, string unifiSiteName, int netBoxSiteId, CancellationToken cancellationToken = default);
}
