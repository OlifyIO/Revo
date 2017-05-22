﻿using System.Linq;
using System.Web.OData.Query;

namespace GTRevo.Platform.Commands
{
    public class WrappedEntityQueryWithContext<TEntity, TWrapper> : QueryWithContext<TWrapper>,
        IWrappedEntityQuery<TEntity, TWrapper>
    {
        public ODataQueryOptions<TEntity> QueryOptions { get; set; }
    }
}