﻿using System;
using System.Collections.Generic;
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
                m.CreateMap<Firma, FirmaViewModel>()
                    .ForMember(fv => fv.GumrukKullaniciAdSoyad, opt => opt.MapFrom(f => f.GumrukKullanici.Adi + " " + f.GumrukKullanici.Soyadi))
                    .ForMember(fv => fv.Id, opt => opt.MapFrom(f => f.FirmaId));
                m.CreateMap<FirmaViewModel, Firma>()
                    .ForMember(f => f.GumrukKullaniciId, opt => opt.MapFrom(fv => fv.GumrukKullaniciId))
                    .ForMember(fv => fv.FirmaId, opt => opt.MapFrom(f => f.Id));
                m.CreateMap<GumrukKod, GumrukKodViewModel>();
                m.CreateMap<GumrukKodViewModel, GumrukKod>();
                m.CreateMap<TalepDetayFirma, TalepDetayFirmaViewModel>();
                m.CreateMap<CevapDetayGumruk, CevapViewModel>();
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
