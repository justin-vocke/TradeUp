﻿using Microsoft.AspNetCore.Authorization;

namespace TradeUp.Infrastructure.Authorization
{
    internal sealed class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }  
    }
}
