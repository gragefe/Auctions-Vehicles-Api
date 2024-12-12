namespace Application.DTO;

using System.Collections.Generic;
using System.Linq;

public class Page<T>(int pageNumber = 1, int totalPages = 1, int totalItems = 0, IEnumerable<T> results = null)
    where T : class
{
    public int CurrentPage { get; set; } = pageNumber;

    public int TotalPages { get; set; } = totalPages;

    public int TotalItems { get; set; } = totalItems;

    public IEnumerable<T> Results { get; set; } = results ?? Enumerable.Empty<T>();
}