namespace BiglerNet.NetBox.UnifiSync.Models.Configuration;

public class UnifiOptions
{
    public string BaseUrl { get; set; } = string.Empty;

    public UnifiAuthenticationOptions Authentication { get; set; } = new();
}
