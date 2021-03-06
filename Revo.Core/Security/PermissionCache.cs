﻿using System;
using System.Collections.Generic;

namespace Revo.Core.Security
{
    public class PermissionCache : IPermissionCache
    {
        public PermissionCache()
        {
        }

        public IEnumerable<Permission> GetRolePermissions(Guid roleId, IRolePermissionResolver rolePermissionResolver)
        {
            //TODO: cache
            return rolePermissionResolver.GetRolePermissions(roleId);
        }

        public void Invalidate()
        {
            throw new NotImplementedException();
        }
    }
}
