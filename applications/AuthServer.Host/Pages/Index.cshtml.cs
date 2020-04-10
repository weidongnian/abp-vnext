using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.Localization;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace AuthServer.Host.Pages
{
    public class IndexModel : AbpPageModel
    {
        readonly IStringLocalizer<SolutionResource> _L;
        public IndexModel(IStringLocalizer<SolutionResource> L)
        {
            _L = L;
        }

        public void OnGet()
        {
            Console.WriteLine("\nOnGet==>“≥√Ê≥ı ºªØ HelloWorld:" + _L["HelloWorld"]+ "\n");
        }
    }
}