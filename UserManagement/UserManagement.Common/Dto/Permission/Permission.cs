using System.Linq.Expressions;

namespace UserManagement.Common.Dto.Permission;

public class Permission
{
    public const string IdSortKey = nameof(Id);
    public const string NameSortKey = nameof(Name);
    public const string KeySortKey = nameof(Key);
    public const string DescriptionSortKey = nameof(Description);
    
    public static readonly Dictionary<string, Expression<Func<Permission, object>>> SortMap =
        new()
        {
            { IdSortKey, _ => _.Id },
            { NameSortKey, _ => _.Name },
            { KeySortKey, _ => _.Key },
            { DescriptionSortKey, _ => _.Description },
        };

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }
}