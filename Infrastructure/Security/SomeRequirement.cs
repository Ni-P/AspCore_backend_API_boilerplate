using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure.Security
{
    public class SomeRequirement : IAuthorizationRequirement
    {
    }

    public class SomeRequirementHandler : AuthorizationHandler<SomeRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public SomeRequirementHandler(IHttpContextAccessor httpContextAccessor, DataContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            SomeRequirement requirement)
        {
            var currentUserName = _httpContextAccessor.HttpContext.User?.Claims
                ?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            // Insert logic for requirement checks here
            
            
            // if success
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}