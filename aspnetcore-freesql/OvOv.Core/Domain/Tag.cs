using FreeSql.DataAnnotations;

namespace OvOv.Core.Domain
{
    public class Tag
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public string TagName { get; set; }
    }
}
