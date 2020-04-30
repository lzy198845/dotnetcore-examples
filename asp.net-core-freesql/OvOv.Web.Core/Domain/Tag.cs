using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvOv.Web.Core.Domain
{
    public class Tag
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public string TagName { get; set; }
    }
}
