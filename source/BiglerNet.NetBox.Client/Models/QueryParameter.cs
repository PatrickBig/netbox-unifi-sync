using System;
using System.Collections.Generic;
using System.Text;

namespace BiglerNet.NetBox.Client.Models;

public sealed record QueryParameter(string Name, IReadOnlyList<string> Values);
