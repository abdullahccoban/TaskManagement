using Microsoft.Extensions.Configuration;

namespace TaskManagement.Core.Helpers;

public static class AppSettingsHelper
{
    private static IConfiguration? _configuration;

    public static void Init(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string GetValue(string key)
    {
        if (_configuration == null) 
            throw new InvalidOperationException("AppSettingsHelper not initialized. Call Init() first.");
        
        string? value = _configuration[key];

        if (string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"Key '{key}' not found in configuration.");

        return value;        
    }
}
