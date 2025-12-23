using BiglerNet.NetBox.UnifiSync.Models.Unifi;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.UnifiSync.Services;

public interface IUnifiSiteSynchronizer
{
    public Task<int> SynchronizeSiteAsync(SiteListItem site, CancellationToken cancellationToken = default);
}
