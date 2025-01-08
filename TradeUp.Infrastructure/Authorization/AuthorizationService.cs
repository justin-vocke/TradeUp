﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities.Users;

namespace TradeUp.Infrastructure.Authorization
{
    internal sealed class AuthorizationService
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorizationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
        {
            var roles = await _dbContext.Set<User>()
                .Where(x => x.IdentityId == identityId)
                .Select(user => new UserRolesResponse
                {
                    Id = user.Id,
                    Roles = user.Roles.ToList()
                })
                .FirstAsync();
            
            return roles;
        }

        internal async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
        {
            var permissions = await _dbContext.Set<User>()
                .Where(user => user.IdentityId == identityId)
                .SelectMany(user => user.Roles.Select(x => x.Permissions))
                .FirstAsync();

            var permissionSet = permissions.Select(x => x.Name).ToHashSet();

            return permissionSet;
        }
    }
}
