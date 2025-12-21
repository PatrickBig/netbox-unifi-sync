using BiglerNet.NetBox.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client;

internal interface IDcimClient
{
    public Task<PaginatedCableTerminationList> GetCableTerminationListAsync(CancellationToken cancellationToken = default);
}
