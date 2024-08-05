namespace Core.DTOs.Paging
{
    public record BasePaging(
        int Page,
        int PageSize,
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder);
}
