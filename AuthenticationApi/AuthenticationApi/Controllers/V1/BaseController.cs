using AuthenticationApi.Data;
using AuthenticationApi.Models.Enums;
using AuthenticationApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Controllers.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[EnableCors(CorsPolicyKeys.Policy_A)]
[Authorize]
public class BaseController : Controller
{
    //public override void OnActionExecuting(ActionExecutingContext context)
    //{
    //    AuthenticationApiDbContext dbcontext = (AuthenticationApiDbContext)context.HttpContext.RequestServices
    //       .GetService(typeof(AuthenticationApiDbContext));

    //    Guid userId = Guid.Parse(User.Claims.SingleOrDefault(q => q.Type == "UserId").Value);

    //    var roleIds = dbcontext.Roles.Where(q => q.Users.Any(u => u.Id == userId)).Select(q => q.RoleId).ToList();

    //    var userPermissions = dbcontext.Permissions.Where(q => q.RolePermissions.Any(r => roleIds.Contains(r.RoleId))).ToList();

    //    string areaName = null;
    //    var dataTokens = context.RouteData.DataTokens;
    //    if (dataTokens.ContainsKey("area")) { areaName = (string)dataTokens["area"]; }

    //    string controllerName = context.RouteData.Values["Controller"].ToString();
    //    string actionName = context.RouteData.Values["Controller"].ToString();

    //    ActionType actionType = ActionType.HttpGet;
    //    var attribute = context.ActionDescriptor.EndpointMetadata.OfType<HttpMethodAttribute>().FirstOrDefault();
    //    if (attribute != null && attribute.GetType() == typeof(HttpGetAttribute)) { actionType = ActionType.HttpGet; }
    //    if (attribute != null && attribute.GetType() == typeof(HttpPostAttribute)) { actionType = ActionType.HttpPost; }
    //    if (attribute != null && attribute.GetType() == typeof(HttpDeleteAttribute)) { actionType = ActionType.HttpDelete; }
    //    if (attribute != null && attribute.GetType() == typeof(HttpPutAttribute)) { actionType = ActionType.HttpPut; }

    //    if (userPermissions.Any(x => (string.IsNullOrEmpty(areaName) ? true : x.AreaName == areaName) &&
    //    x.ControllerName == controllerName + "Controller" && x.ActionName == actionName && x.ActionType == actionType))
    //    {
    //        base.OnActionExecuting(context);
    //    }
    //    else
    //    {
    //        context.Result = new RedirectToRouteResult(
    //            new RouteValueDictionary {

    //                 {"controller","Authenticate" },
    //                 {"action","Login"}
    //            }
    //       );
    //    }


    //}
   
}


