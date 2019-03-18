//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authorization.Infrastructure;
//using StudyHub.Models;
//using StudyHub.Repositories.Interfaces;

//namespace StudyHub.Authorizations
//{
//    public class UserIsOwnerAuthorizationHandler
//    : AuthorizationHandler<OperationAuthorizationRequirement, User>
//    {
//        private readonly IUserRepository userRepository;

//        public UserIsOwnerAuthorizationHandler(IUserRepository userRepository)
//        {
//            this.userRepository = userRepository;
//        }

//        protected override Task HandleRequirementAsync(
//            AuthorizationHandlerContext context,
//            OperationAuthorizationRequirement requirement,
//            User resource)
//        {
//            if (context.User == null || resource == null)
//            {
//                // Return Task.FromResult(0) if targeting a version of
//                // .NET Framework older than 4.6:
//                return Task.CompletedTask;
//            }

//            if (requirement.Name != Constants.CreateOperationName &&
//                requirement.Name != Constants.ReadOperationName &&
//                requirement.Name != Constants.UpdateOperationName &&
//                requirement.Name != Constants.DeleteOperationName)
//            {
//                return Task.CompletedTask;
//            }

//            if (resource.Id == userRepository.GetUserId(context.User))
//            {
//                context.Succeed(requirement);
//            }

//            return Task.CompletedTask;
//        }
//    }
//}
