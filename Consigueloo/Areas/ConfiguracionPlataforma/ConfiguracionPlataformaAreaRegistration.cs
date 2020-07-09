using System.Web.Mvc;

namespace Consigueloo.Areas.ConfiguracionPlataforma
{
    public class ConfiguracionPlataformaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ConfiguracionPlataforma";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ConfiguracionPlataforma_default",
                "ConfiguracionPlataforma/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}