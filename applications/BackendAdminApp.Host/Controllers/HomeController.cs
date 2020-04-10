using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BackendAdminApp.Host.Controllers
{
    public class HomeController : AbpController
    {
        readonly IStringLocalizer<BackendAdminAppResource> _L;
        public HomeController(IStringLocalizer<BackendAdminAppResource> L)
        {
            _L = L;
        }

        public ActionResult Index()
        {
            return Redirect("/Identity/Users");
        }

        [AllowAnonymous]
        public string Test()
        {
            return L["LongWelcomeMessage"];
        }
    }
}
