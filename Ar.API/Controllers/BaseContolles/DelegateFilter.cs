using Ar.API.Filters;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Ar.API.Controllers.BaseContolles
{
    public class DelegateFilter : ActionFilterAttribute, IOrderFilter
    {
        public int Order { get { return 3; } }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string accountID = HttpContext.Current.Items["accountID"] as string;


        }
    }
}