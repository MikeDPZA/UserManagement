using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace UserManagement.Common.Dto;

public abstract class BaseFilterDto<T> where T: class
{
    [JsonPropertyName("searchText")] public string SearchText { get; set; } = "";

    [Range(1, int.MaxValue)]
    [JsonPropertyName("pageNum")]public int PageNum { get; set; } = 1;
    
    [Range(1, 100)]
    [JsonPropertyName("pageSize")]public int PageSize { get; set; } = 10;

    [JsonPropertyName("sortKey")]public bool SortAscending { get; set; }

    public abstract string SortKey { get; set; }
}