using BiglerNet.NetBox.Client.Models;
using BiglerNet.NetBox.Client.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client;

public interface IExtrasClient
{
    public Task<CustomField> CreateCustomFieldAsync(WritableCustomFieldRequest request, CancellationToken cancellationToken = default);

    public Task<PaginatedCustomFieldList> ListCustomFieldsAsync(ExtrasCustomFieldFilter filter, CancellationToken cancellationToken = default);
}
