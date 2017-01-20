using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Visug2CommitBOTApp.Model;
using Visug2CommitBOTApp.Persistence;

namespace Visug2CommitBOTApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //VisugRepo<Registrant>.Initialize(ConfigurationManager.AppSettings["collection-registrant-data"]);
            //VisugRepo<RegistrantBotData>.Initialize(ConfigurationManager.AppSettings["collection-registrant-bot-data"]);

            VisugRepoTableStorage<Registrant>.Initialize(ConfigurationManager.AppSettings["registrant-table"]);
        }
    }
}
