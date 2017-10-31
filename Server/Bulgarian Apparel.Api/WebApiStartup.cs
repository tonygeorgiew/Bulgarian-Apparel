using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Bulgarian_Apparel.Api.Startup))]

namespace Bulgarian_Apparel.Api
{
    public class WebApiStartup
    {
        public static void StartWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
