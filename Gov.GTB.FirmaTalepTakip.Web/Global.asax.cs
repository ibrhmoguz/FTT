using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;

namespace Gov.GTB.FirmaTalepTakip.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new FirmaDbInitializer());

            Mapper.Initialize(m =>
            {
                m.CreateMap<Firma, FirmaViewModel>();
                m.CreateMap<FirmaViewModel, Firma>();
            });
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception exc = Server.GetLastError();

            //// Exceptions logged using with Nlog. 
            //var logger = LogManager.GetCurrentClassLogger();
            //logger.Error(exc, JsonConvert.SerializeObject(exc));

            //Server.ClearError();

            //Response.Redirect("~/Error/Index");
        }
    }
}
