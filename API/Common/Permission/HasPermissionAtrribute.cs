//using Microsoft.AspNetCore.Authorization;
//using Microsoft.Extensions.Options;
//using Moneyon.PowerBi.Domain.Model.Modeling;

//namespace Moneyon.PowerBi.API.Common.Permission
//{
//    public sealed class HasPermissionAttribute : AuthorizeAttribute
//    {
//        public HasPermissionAttribute(params PermissionEnum[] permissions)
//            : base(policy: string.Join(",", permissions.Select(p => ((int)p).ToString()).ToArray()))
//        {
//        }
//    }

//    public class PermissionRequirment : IAuthorizationRequirement
//    {
//        public string Permission { get; }
//        public PermissionRequirment(string permission)
//        {
//            Permission = permission;
//        }
//    }

//    public class PermissionAuthorizationHandler :
//        AuthorizationHandler<PermissionRequirment>
//    {
//        private readonly IServiceScopeFactory _scopeFactory;

//        public PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory)
//        {
//            _scopeFactory = scopeFactory;
//        }

//        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirment requirement)
//        {
//            string? personId = context.User.Identity!.Name;
//            if (personId == null)
//                return;

//            using IServiceScope scope = _scopeFactory.CreateScope();
//            PermissionService permisssionService = scope.ServiceProvider.GetRequiredService<PermissionService>();

//            var permissions = await permisssionService.GetPermissionIdsAsync(personId);
//            var inputPermissions = requirement.Permission.Split(',');
//            if (permissions!.Any(p => inputPermissions.Contains(p)))
//            {
//                context.Succeed(requirement);
//            }
//        }
//    }

//    public class PermissionAuthorizationPolicyProvider
//        : DefaultAuthorizationPolicyProvider
//    {
//        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
//        {
//        }

//        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
//        {
//            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

//            if (policy is not null)
//                return policy;

//            return new AuthorizationPolicyBuilder()
//                        .AddRequirements(new PermissionRequirment(policyName))
//                        .Build();
//        }
//    }
//}
