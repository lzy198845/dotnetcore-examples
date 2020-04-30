using FreeSql;
using OvOv.Web.Core.Web;

namespace OvOv.Web.Core.Extensions
{
    public static class CollectionsExtensions
    {
        public static ISelect<T> Page<T>(this ISelect<T> source, PageDto pageDto) where T : class, new()
        {
            return source.Page(pageDto.PageNumber, pageDto.PageNumber);
        }
    }
}
