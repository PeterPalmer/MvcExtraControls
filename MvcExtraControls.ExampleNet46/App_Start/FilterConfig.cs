using System.Web;
using System.Web.Mvc;

namespace MvcExtraControls.ExampleNet46
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
