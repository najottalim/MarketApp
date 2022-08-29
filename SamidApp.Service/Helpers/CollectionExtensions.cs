using SamidApp.Domain.Configurations;

namespace SamidApp.Service.Helpers;

public static class CollectionExtensions
{
    public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params)
    {
        return @params.PageIndex > 0 && @params.PageSize >= 0
            ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : source;
    }
}