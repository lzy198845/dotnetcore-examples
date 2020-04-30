namespace OvOv.Web.Core.Domain
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
