namespace Api.Extensions;

public class ConfigurationBuilder
{
    public ConfigurationBuilder()
    {
        UseRateLimit = false;
        UseHealthChecks = false;
        UseResponseCompression = false;
        UseApiVersioning = true;
        UseCors = true;
    }

    public bool UseApiVersioning { get; internal set; }
    public bool UseHealthChecks { get; internal set; }
    public bool UseResponseCompression { get; internal set; }
    public bool UseRateLimit { get; internal set; }
    public bool UseCors { get; internal set; }

    public ConfigurationBuilder SetApiVersioning()
    {
        UseApiVersioning = true;
        return this;
    }

    public ConfigurationBuilder SetHealthChecks()
    {
        UseHealthChecks = true;
        return this;
    }

    public ConfigurationBuilder SetResponseCompressiony()
    {
        UseResponseCompression = true;
        return this;
    }

    public ConfigurationBuilder SetRateLimit()
    {
        UseRateLimit = true;
        return this;
    }

    public ConfigurationBuilder SetCors() {
        UseCors = true;
        return this;
    }

}
