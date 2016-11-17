using MeetUp.ApiTokenDemo.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Routing;

namespace MeetUp.ApiTokenDemo.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            TokenHelper.AddKeyFile("SESSION", HostingEnvironment.MapPath(@"~/App_Data/SESSION.xml"));
        }
    }
}
