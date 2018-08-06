using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using CryptoBot.Models;
using CryptoBot.Models.Parse;

namespace CryptoBot
{
    public partial class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Bot.Get().GetAwaiter().GetResult();
            string[] coinsNames = {
                    "bitcoin",
                    "ethereum",
                    "ripple",
                    "bitcoin-cash",
                    "cardano",
                    "litecoin",
                    "stellar",
                    "neo",
                    "dash",
                    "ethereum-classic",
                };
            CoinsInfo.Instance.RefreshRateAsync(coinsNames);
        }
    }
}
