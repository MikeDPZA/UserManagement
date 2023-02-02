using System.Linq.Expressions;
using LinqKit;
using UserManagement.Common.Dto.Permission;
using UserManagement.Common.Models;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.MapperServices;

public class PermissionMapperService: IPermissionMapperService
{
    public Expression<Func<PermissionModel, bool>> MapToFilterExpression(PermissionFilterDto? filter)
    {
        var builder = PredicateBuilder.New<PermissionModel>(true);
        if (filter == null) return builder;

        if (!string.IsNullOrEmpty(filter.Name))
        {
            filter.Name = filter.Name.ToLower();
            builder.And(_ => _.Name.Contains(filter.Name));
        }
        
        if (!string.IsNullOrEmpty(filter.Key))
        {
            filter.Key = filter.Key.ToLower();
            builder.And(_ => _.Key.Contains(filter.Key));
        }
        
        if (!string.IsNullOrEmpty(filter.Description))
        {
            filter.Description = filter.Description.ToLower();
            builder.And(_ => _.Description.Contains(filter.Description));
        }
        
        if (!string.IsNullOrEmpty(filter.SearchText))
        {
            filter.SearchText = filter.SearchText.ToLower();
            builder.And(_ =>
                _.Key.ToLower().Contains(filter.SearchText) ||
                _.Name.ToLower().Contains(filter.SearchText) ||
                _.Description.ToLower().Contains(filter.SearchText));
        }

        return builder;
    }

    public Expression<Func<PermissionModel, Permission>> MapToPermissionDto()
        => _ => new Permission()
        {
            Id = _.Id,
            Key = _.Key,
            Name = _.Name,
            Description = _.Description
        };
}