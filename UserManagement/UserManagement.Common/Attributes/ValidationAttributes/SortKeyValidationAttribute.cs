using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace UserManagement.Common.Attributes.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class SortKeyValidationAttribute: ValidationAttribute
{
    private readonly string[] _validKeys;

    public SortKeyValidationAttribute(params string[] validKeys): base(() => $"Sort Key must be one of the following {string.Join(", ", validKeys)}")
    {
        _validKeys = validKeys;
    }
    
    public override bool IsValid(object? value)
    {
        return value != default && _validKeys.Contains(value.ToString());
    }
}