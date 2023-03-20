using System.Web;
using System.Web.Mvc;

namespace ArturCzarnecki_9807
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
