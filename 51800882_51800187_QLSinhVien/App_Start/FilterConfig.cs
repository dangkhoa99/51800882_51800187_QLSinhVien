using System.Web;
using System.Web.Mvc;

namespace _51800882_51800187_QLSinhVien
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
