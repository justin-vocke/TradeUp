using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions.Caching;
using TradeUp.Domain.Core.Entities.Users;

namespace TradeUp.Infrastructure.Authorization
{
    internal sealed class AuthorizationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICacheService _cacheService;

        public AuthorizationService(ApplicationDbContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
        {
            var cacheKey = $"auth:roles-{identityId}";

            var cachedRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

            if(cachedRoles is not null)
            {
                return cachedRoles;
            }

            var roles = await _dbContext.Set<User>()
                .Where(x => x.IdentityId == identityId)
                .Select(user => new UserRolesResponse
                {
                    Id = user.Id,
                    Roles = user.Roles.ToList()
                })
                .FirstAsync();
            
            await _cacheService.SetAsync(cacheKey, roles);  

            return roles;
        }

        internal async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
        {
            var cacheKey = $"auth:permissions-{identityId}";

            var cachedPermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey);

            if(cachedPermissions is not null)
            {
                return cachedPermissions;
            }
            var permissions = await _dbContext.Set<User>()
                .Where(user => user.IdentityId == identityId)
                .SelectMany(user => user.Roles.Select(x => x.Permissions))
                .FirstAsync();

            var permissionSet = permissions.Select(x => x.Name).ToHashSet();

            await _cacheService.SetAsync(cacheKey, permissionSet);

            return permissionSet;
        }
    }
}
