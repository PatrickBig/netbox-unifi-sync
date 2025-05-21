using BiglerNet.NetBox.UnifiSync.Models.Unifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiglerNet.NetBox.UnifiSync.Services;
public interface IUnifiClient
{
    public Task<PagedResponse<SiteListItem>> GetSitesAsync();

    public Task<PagedResponse<DeviceListItem>> GetDevicesAsync(Guid siteId, int offset = 0, int limit = 25);

    public Task<DeviceDetails> GetDeviceAsync(Guid siteId, Guid deviceId);

    public Task<PagedResponse<ClientListItem>> GetClientsAsync(Guid siteId, int offset = 0, int limit = 25);
}
