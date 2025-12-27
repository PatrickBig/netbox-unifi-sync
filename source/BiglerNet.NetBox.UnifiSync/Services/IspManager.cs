using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.UnifiSync.Services;

internal class IspManager : UnifiSiteResourceManagerBase
{
    public override Task<int> SyncronizeUnifiResourcesAsync(string unifiSiteId, Guid unifiExternalId, string unifiSiteName, int netBoxSiteId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
