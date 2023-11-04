namespace LTA.Domain.Common;

public class ApplicationSettings
{
    public string DatabaseAddress { get; set; }

    public string DatabaseName { get; set; }

    public string DatabaseUser { get; set; }

    public string DatabasePassword { get; set; }
    
    public string DatabasePort { get; set; }

    public string JsonPlaceholderApiUrl { get; set; }

    public int JsonPlaceholderApiDelayInSec { get; set; }

    public int JsonPlaceholderApiNumRetry { get; set; }

    public int DefaultCacheDuration { get; set; }
}