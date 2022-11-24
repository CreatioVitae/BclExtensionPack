using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigurationExtensions {
    public static T GetAvailable<T>(this IConfigurationSection configuration) {
        var obj = configuration.Get<T>();

        ArgumentNullException.ThrowIfNull(obj);

        Validator.ValidateObject(obj, new ValidationContext(obj), true);
        return obj;
    }

    public static string GetAvailableValueByKey(this IConfiguration configuration, string key) {
        var valueOrDefault = configuration[key];

        ArgumentNullException.ThrowIfNull(valueOrDefault);

        return valueOrDefault;
    }
}
