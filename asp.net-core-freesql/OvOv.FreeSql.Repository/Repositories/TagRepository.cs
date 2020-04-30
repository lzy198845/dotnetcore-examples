using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FreeSql;
using OvOv.Web.Core.Domain;

namespace OvOv.FreeSql.Repository.Repositories
{
    public class TagRepository : DefaultRepository<Tag,int>
    {
        public TagRepository(UnitOfWorkManager uowm) : base(uowm?.Orm, uowm) 
        {
        }
    }
}
