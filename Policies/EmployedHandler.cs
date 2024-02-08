using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PuntoVenta.Policies
{
    public class EmployedHandler : AuthorizationHandler<EmployedRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            EmployedRequirement requirement
         )
        {

            if (context.User.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == "Admin"))
            {
                return Task.FromResult(true);
            }

            if (context.User.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == "Empleado"))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult( 0 );
        }
    }
}
