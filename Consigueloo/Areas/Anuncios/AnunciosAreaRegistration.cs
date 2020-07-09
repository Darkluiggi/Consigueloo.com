using System.Web.Mvc;

namespace Consigueloo.Areas.Anuncios
{
    public class AnunciosAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Anuncios";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Anuncios_default",
                "Anuncios/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}