using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FreeSql;
using OvOv.Web.Core.Domain;

namespace OvOv.FreeSql.Repository.Repositories
{
    public class BlogRepository : DefaultRepository<Blog,int>
    {
        public BlogRepository(UnitOfWorkManager uowm) : base(uowm?.Orm, uowm) 
        {
        }

        public List<Blog> GetBlogs()
        {
            return Select.Page(1, 10).ToList();
        }


    }
}
