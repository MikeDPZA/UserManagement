using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using UserManagement.Common.Models;

namespace UserManagement.Common.Dto.User;

[ExcludeFromCodeCoverage]
public class User
{
    public const string IdSortKey = nameof(Id);
    public const string FirstnameSortKey = nameof(Firstname);
    public const string LastnameSortKey = nameof(Lastname);
    public const string EmailSortKey = nameof(Email);
    public const string UserIdentifierSortKey = nameof(UserIdentifier);
    
    public static readonly Dictionary<string, Expression<Func<User, object>>> SortMap =
        new()
        {
            { IdSortKey, _ => _.Id },
            { FirstnameSortKey, _ => _.Id },
            { LastnameSortKey, _ => _.Id },
            { EmailSortKey, _ => _.Id },
            { UserIdentifierSortKey, _ => _.Id },
        };

    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string FullName => $"{Firstname} {Lastname}";
    public string Email { get; set; }
    public string UserIdentifier { get; set; }
    
}