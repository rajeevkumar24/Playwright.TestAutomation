using PlaywrightFramework.Driver;

namespace PlaywrightFramework.Config;

public class TestSettings
{
    public float? Timeout = PlaywrightDriverInitializer.DEFAULT_TIMEOUT;
    public DriverType DriverType { get; set; }
    public string ApplicationUrl { get; set; }
    public string[]? Args { get; set; }
    public bool? Headless { get; set; }
    public float? SlowMo { get; set; }
}

public enum DriverType
{
    Chrome,
    Firefox,
    Edge,
    Chromium,
    Opera
}