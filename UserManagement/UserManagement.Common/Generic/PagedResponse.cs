using System.ComponentModel.DataAnnotations;

namespace UserManagement.Common.Generic;

public class PagedResponse<T>
{
    public PagedResponse()
    {
        
    }
    
    /// <summary>
    /// This is a constructor that takes in a list of data, a page number, a page size, and a total. It then sets the total
    /// to the total if it is not 0, otherwise it sets it to the count of the data. It then sets the page number, page size,
    /// and data to the values passed in.
    /// </summary>
    /// <param name="dataToPage">Set of data to apply paging to</param>
    /// <param name="pageNum">Current page number</param>
    /// <param name="pageSize">Amount of records to return</param>
    /// <param name="total">Amount of records in the set</param>
    public PagedResponse(IEnumerable<T> dataToPage, int pageNum = 1, int pageSize = 10, int total = 0)
    {
        Total = total == 0 ? dataToPage.Count() : total;
        PageNum = pageNum;
        PageSize = pageSize;
        Data = dataToPage
            .Skip((pageNum - 1) * pageSize)
            .Take(PageSize)
            .ToList();
    }
    
    /// <summary>
    /// This is a constructor that takes in a list of data, a page number, a page size, and a total. It then sets the total
    /// to the total if it is not 0, otherwise it sets it to the count of the data. It then sets the page number, page size,
    /// and data to the values passed in.
    /// </summary>
    /// <param name="dataToPage">Set of data to apply paging to</param>
    /// <param name="pageNum">Current page number</param>
    /// <param name="pageSize">Amount of records to return</param>
    /// <param name="total">Amount of records in the set</param>
    public PagedResponse(IQueryable<T> dataToPage, int pageNum = 1, int pageSize = 10, int total = 0)
    {
        Total = total == 0 ? dataToPage.Count() : total;
        PageNum = pageNum;
        PageSize = pageSize;
        Data = dataToPage
            .Skip((pageNum - 1) * pageSize)
            .Take(PageSize)
            .ToList();
    }

    /// <summary>
    /// Total amount of records that are available in set
    /// </summary>
    public int Total { get; set; } = 0;
    
    /// <summary>
    /// The current page number
    /// </summary>
    /// <example>1</example>
    [Range(1, int.MaxValue)]public int PageNum { get; set; } = 1;
   
    /// <summary>
    /// The amount of records to return
    /// </summary>
    /// <example>10</example>
    [Range(1, 50)]public int PageSize { get; set; } = 10;
    
    /// <summary>
    /// Resulting paged data set
    /// </summary>
    public List<T> Data { get; set; }
}