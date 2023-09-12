using System.Linq;

// ReSharper disable once CheckNamespace
namespace System.ComponentModel.DataAnnotations;
public static class ObjectValidator {
    public static IEnumerable<ValidationResult> Validate(object origin) {
        var validationResult = new List<ValidationResult>();

        _ = Validator.TryValidateObject(origin, new ValidationContext(origin), validationResult, true);

        return validationResult;
    }

    public static void ThrowIfInvalidate(object origin) {
        var validationResult = new List<ValidationResult>();

        if (!Validator.TryValidateObject(origin, new ValidationContext(origin), validationResult, true)) {
            throw new ValidationException(string.Join(Environment.NewLine, validationResult.Select(r => r.ErrorMessage)));
        }
    }
}
