using System.Web.Mvc;

namespace MonopakApp.Areas.monopakadmin
{
    public class monopakadminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "monopakadmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "monopakadmin_default",
                "monopakadmin/{controller}/{action}/{id}",
                new { action = "Index",controller="Home", id = UrlParameter.Optional },
                new string[] { "MonopakApp.Areas.monopakadmin.Controllers" }
                );
        }
    }
}