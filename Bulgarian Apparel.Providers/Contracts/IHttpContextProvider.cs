using Microsoft.Owin;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;

namespace Bulgarian_Apparel.Providers.Contracts
{
    public interface IHttpContextProvider
    {
        HttpContext CurrentHttpContext { get; }

        IOwinContext CurrentOwinContext { get; }

        IIdentity CurrentIdentity { get; }

        Cache ContextCache { get; }

        TManager GetUserManager<TManager>();
    }
}
