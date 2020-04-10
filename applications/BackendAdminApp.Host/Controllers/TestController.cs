using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Json;

namespace BackendAdminApp.Host.Controllers
{
    public class TestController : AbpController
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IPermissionChecker _permissionChecker;

        ILogger<TestController> _ILogger;

        public TestController(IJsonSerializer jsonSerializer, IPermissionChecker permissionChecker,
            ILogger<TestController> ILogger)
        {
            _jsonSerializer = jsonSerializer;
            _permissionChecker = permissionChecker;
            _ILogger = ILogger;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            _ILogger.LogInformation("\nEnter Index page=>\n");

            var newLine = Environment.NewLine + Environment.NewLine;

            return Content(
                "Claims: " + User.Claims.Select(c => $"{c.Type} = {c.Value}").JoinAsString(" | ") + newLine +
                "CurrentUser: " + _jsonSerializer.Serialize(CurrentUser) + newLine +
                "access_token: " + await HttpContext.GetTokenAsync("access_token") + newLine +
                "isGranted: AbpIdentity.Users: " + await _permissionChecker.IsGrantedAsync("AbpIdentity.Users")
            );
        }
    }
}
