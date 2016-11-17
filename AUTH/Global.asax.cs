using MeetUp.ApiTokenDemo.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Routing;

namespace MeetUp.ApiTokenDemo.AUTH
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            TokenHelper.AddKeyFile("APIKEY", HostingEnvironment.MapPath(@"~/App_Data/APIKEY.xml"));
            TokenHelper.AddKeyFile("SESSION", HostingEnvironment.MapPath(@"~/App_Data/SESSION-PRIVATE.xml"));
        }
    }
}
