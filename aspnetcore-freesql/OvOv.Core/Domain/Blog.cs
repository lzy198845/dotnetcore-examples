using System;
using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace OvOv.Core.Domain
{
    public class Blog : ISoftDelete
    {
        /// <summary>
        /// �������ڷ���ר��Id
        /// </summary>
        public int? ClassifyId { get; set; }
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public virtual Classify Classify { get; set; }
        [Column(DbType = "varchar(50)")]
        public string Title { get; set; }
        [Column(DbType = "varchar(500)")]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual List<Post> Posts { get; set; }
        public bool IsDeleted { get; set; }

        [Navigate("ArticleId")]
        public virtual ICollection<UserLike> UserLikes { get; set; }
    }
    public class Classify
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public int SortCode { get; set; }
        /// <summary>
        /// ����ר������
        /// </summary>
        [Column(DbType = "varchar(50)")]
        public string ClassifyName { get; set; }

        public virtual List<Blog> Articles { get; set; }

    }
        [Table(Name = "blog_user_like")]
    public class UserLike 
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public bool Status { get; set; }
        public virtual Blog Blog { get; set; }
    }
}