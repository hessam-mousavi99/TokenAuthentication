using AuthenticationApi.Data;
using AuthenticationApi.Entities;
using AuthenticationApi.Models.Enums;
using AuthenticationApi.Models.ViewModels;
using AuthenticationApi.Services.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthenticationApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;
        private readonly AuthenticationApiDbContext _context;

        public PermissionController(IPermissionService permissionService ,AuthenticationApiDbContext context)
        {
            _permissionService = permissionService;
            _context = context;
        }
        [HttpPost("Add-Permissions")]
        public async Task<IActionResult> Post()
        {
            var permissions = new List<Permission>();
            var assembly=Assembly.GetExecutingAssembly();
            var controllers = assembly.GetTypes().Where(q => q.BaseType == typeof(BaseController));
            foreach (var controller in controllers)
            {
                var actions = controller.GetMethods().Where(q => q.ReturnType == typeof(IActionResult) ||
                q.ReturnType == typeof(Task<IActionResult>));

                string areaname = string.Empty;
                if (controller.CustomAttributes.Any(q=>q.AttributeType==typeof(AreaAttribute)))
                {
                    string areaName = controller.CustomAttributes.
                        SingleOrDefault(q => q.AttributeType == typeof(AreaAttribute)).
                        ConstructorArguments[0].Value.ToString();
                }
                foreach (var action in actions)
                {
                    var permission = new Permission();
                    permission.Title =$"{controller.Name}-{action.Name}";
                    permission.ActionName = action.Name;
                    permission.ControllerName = controller.Name;
                    permission.AreaName = areaname;
                    permission.ActionType = GetActionType(action);
                    if (!_context.Permissions.Any(p => p.AreaName == areaname
                                      && p.ControllerName == controller.Name
                                      && p.ActionName == action.Name
                                      && p.ActionType == permission.ActionType))
                    {
                        permissions.Add(permission);
                    }
                }    
            }
            await _permissionService.InsertPermissionsAsync(permissions);
            return Ok();
        }
        [HttpPost("SaveRolePermissions")]
        public async Task<IActionResult> PostSaveRolePermissions(AddRolePermissionVM model)
        {
            var RPs = new List<RolePermission>();
            var rolePermissions = await _context.RolePermissions.Where(q => q.RoleId == model.RoleId).ToListAsync();

            foreach (var permissionId in model.SelectedPermissions)
            {
                if (!rolePermissions.Any(q => q.PermissionId == permissionId))
                {
                    RPs.Add(new RolePermission()
                    {
                        RoleId = model.RoleId,
                        PermissionId = permissionId
                    });
                }
            }
            await _permissionService.InsertRolePermissionAsync(RPs);
            return Ok();
        }
        [HttpGet("RolePermissions/{roleId}")]
        public async Task<IActionResult> GetPermissions(int roleId)
        {
            var selectedPermission = await _permissionService.GetPermissionsByRoleIdAsync(roleId);
            return Ok(selectedPermission);
        }
        private ActionType GetActionType(MethodInfo? action)
        {
            if (action.CustomAttributes.Any(q => q.AttributeType == typeof(HttpPostAttribute)))
            {
                return ActionType.HttpPost;
            }
            if (action.CustomAttributes.Any(q => q.AttributeType == typeof(HttpPutAttribute)))
            {
                return ActionType.HttpPut;
            }
            if (action.CustomAttributes.Any(q => q.AttributeType == typeof(HttpDeleteAttribute)))
            {
                return ActionType.HttpDelete;
            }

            return ActionType.HttpGet;
        }
    }
}
