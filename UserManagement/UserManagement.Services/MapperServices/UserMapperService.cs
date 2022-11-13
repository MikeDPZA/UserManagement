using System.Linq.Expressions;
using LinqKit;
using UserManagement.Common.Dto.User;
using UserManagement.Common.Models;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.MapperServices;

public class UserMapperService : IUserMapperService
{
    /// <inheritdoc />
    public Expression<Func<UserModel, bool>> MapToFilterExpression(UserFilterDto filter)
    {
        var builder = PredicateBuilder.New<UserModel>(true);

        if (filter == null)
            return builder;
        
        if (!string.IsNullOrEmpty(filter.Email))
        {
            filter.Email = filter.Email.ToLower();
            builder.And(_ => _.Email.ToLower().Contains(filter.Email));
        }

        if (!string.IsNullOrEmpty(filter.Firstname))
        {
            filter.Firstname = filter.Firstname.ToLower();
            builder.And(_ => _.Name.ToLower().Contains(filter.Firstname));
        }

        if (!string.IsNullOrEmpty(filter.Lastname))
        {
            filter.Lastname = filter.Lastname.ToLower();
            builder.And(_ => _.Lastname.ToLower().Contains(filter.Lastname));
        }

        if (!string.IsNullOrEmpty(filter.UserIdentifier))
        {
            filter.UserIdentifier = filter.UserIdentifier.ToLower();
            builder.And(_ => _.UserIdentifier.ToLower().Contains(filter.UserIdentifier));
        }

        if (!string.IsNullOrEmpty(filter.SearchText))
        {
            filter.SearchText = filter.SearchText.ToLower();
            builder.And(_ =>
                _.UserIdentifier.ToLower().Contains(filter.SearchText) ||
                _.Name.ToLower().Contains(filter.SearchText) ||
                _.Lastname.ToLower().Contains(filter.SearchText) ||
                _.Email.ToLower().Contains(filter.SearchText));
        }

        return builder;
    }

    /// <inheritdoc />
    public Expression<Func<UserModel, User>> MapToUserDto()
        => _ => new User()
                {
                    Email = _.Email,
                    Firstname = _.Name,
                    Lastname = _.Lastname,
                    Id = _.Id,
                    UserIdentifier = _.UserIdentifier
                };

    /// <inheritdoc />
    public User MapToUserDto(UserModel user)
    {
        return new User()
        {
            Id = user.Id,
            Email = user.Email,
            Firstname = user.Name,
            Lastname = user.Lastname,
            UserIdentifier = user.UserIdentifier
        };
    }
}