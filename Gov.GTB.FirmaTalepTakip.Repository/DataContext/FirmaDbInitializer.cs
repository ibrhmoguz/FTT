using Gov.GTB.FirmaTalepTakip.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Gov.GTB.FirmaTalepTakip.Repository.DataContext
{
    public class FirmaDbInitializer : CreateDatabaseIfNotExists<FirmaDbContext>
    {
        protected override void Seed(FirmaDbContext context)
        {
            var roles = new List<Rol>
            {
                new Rol(){Code="FIP", Adi="Firma İrtibat Personeli"},
                new Rol(){Code="GGM", Adi="Gümrükler Genel Müdürlüğü Personeli"},
                new Rol(){Code="BM", Adi="Bölge Müdürü"},
                new Rol(){Code="BIP", Adi="Bölge İrtibat Personeli"}
            };
            roles.ForEach(r => context.Roller.Add(r));

            var gumrukKullanicilar = new List<GumrukKullanici>
            {
                new GumrukKullanici{TcNo=11111111112,Adi="GGM", Soyadi="GGM", BolgeKodu="123", Durum=true,Email="GGM@gumruk.gov.tr", Rol = roles[1]},
                new GumrukKullanici{TcNo=11111111113,Adi="BM", Soyadi="BM", BolgeKodu="123", Durum=true,Email="BM@gumruk.gov.tr",  Rol = roles[2]},
                new GumrukKullanici{TcNo=11111111114,Adi="BIP", Soyadi="BIP", BolgeKodu="123", Durum=true,Email="BIP@gumruk.gov.tr",  Rol = roles[3]}
            };
            gumrukKullanicilar.ForEach(u => context.GumrukKullanicilar.Add(u));

            var firmaKullanicilar = new List<FirmaKullanici>
            {
                new FirmaKullanici{TcNo=11111111111, VergiNo=124123, Telefon="12341234", Adi="FIP",Soyadi="FIP", Durum=true,Email="FIP@gumruk.gov.tr", Sifre="123", Rol = roles[0]},
                new FirmaKullanici{TcNo=11111111115, VergiNo=124123, Telefon="12341234", Adi="FIP1",Soyadi="FIP", Durum=true,Email="FIP1@gumruk.gov.tr", Sifre="123", Rol = roles[0]},
                new FirmaKullanici{TcNo=11111111116, VergiNo=124123, Telefon="12341234", Adi="FIP2",Soyadi="FIP", Durum=true,Email="FIP2@gumruk.gov.tr", Sifre="123", Rol = roles[0]},
                new FirmaKullanici{TcNo=11111111117, VergiNo=124123, Telefon="12341234", Adi="FIP3",Soyadi="FIP", Durum=true,Email="FIP3@gumruk.gov.tr", Sifre="123", Rol = roles[0]},
                new FirmaKullanici{TcNo=11111111118, VergiNo=124123, Telefon="12341234", Adi="FIP4",Soyadi="FIP", Durum=true,Email="FIP4@gumruk.gov.tr", Sifre="123", Rol = roles[0]}
            };
            firmaKullanicilar.ForEach(u => context.FirmaKullanicilar.Add(u));

            var firmalar = new List<Firma>
            {
                new Firma { VergiNo=124123, Adi="Firma1", BolgeKodu = "123", TcNoIrtibatPersoneli= "11111111114"},
            };
            firmalar.ForEach(f => context.Firmalar.Add(f));

            var gumrukKodlari = new List<GumrukKod>
            {
                new GumrukKod {Adi = "Akdeniz Bölge", BolgeKodu = "123"},
                new GumrukKod {Adi = "Karadeniz Bölge", BolgeKodu = "234"},
                new GumrukKod {Adi = "Marmara Bölge", BolgeKodu = "567"},
                new GumrukKod {Adi = "Guney Doğu Bölge", BolgeKodu = "789"}
            };
            gumrukKodlari.ForEach(g => context.GumrukKodlari.Add(g));


            var cevapKonulari = new List<RefTalepCevap>
            {
                new RefTalepCevap {CKonu = "Talep, Bakanlık Merkez teşkilatına iletildi."},
                new RefTalepCevap {CKonu = "Talep, ilgili gümrük müdürlükleri nezdinde incelenmektedir."},
                new RefTalepCevap {CKonu = "Talep hakkında ilgili gümrük idarelerine gerekli talimat verildi"}

            };
            cevapKonulari.ForEach(c => context.CevapKonulari.Add(c));


            var talepKonulari = new List<RefTalepKonu>
            {
                new RefTalepKonu {TKonu = "Tarife,Menşe,Kıymet"},
                new RefTalepKonu {TKonu = "İthalat"},
                new RefTalepKonu {TKonu = "İhracat"}

            };
            talepKonulari.ForEach(t => context.TalepKonulari.Add(t));


            var cevapDetayi = new List<CevapDetayGumruk>
            {
                new CevapDetayGumruk {TcNoIrtibatPersoneli = "36725314789", RefTalepCevap = cevapKonulari[0],CevapAciklama="asd"},
                new CevapDetayGumruk {TcNoIrtibatPersoneli = "36725314784", RefTalepCevap = cevapKonulari[1],CevapAciklama="asd"},
                new CevapDetayGumruk {TcNoIrtibatPersoneli = "36725314782", RefTalepCevap = cevapKonulari[2],CevapAciklama="asd"}

            };
            cevapDetayi.ForEach(d => context.CevapDetayi.Add(d));


            var talepDetayi = new List<TalepDetayFirma>
            {
                new TalepDetayFirma {TalepReferansNo=201806000001, VergiNo=1111111111,BolgeKodu="010005",FirmaKullanici=firmaKullanicilar[0],RefTalepKonuId=1,TalepTarih=DateTime.Now,CevapDurum = false,CevapDetayGumruk = null},
                new TalepDetayFirma {TalepReferansNo=201806000002, VergiNo=1111111111,BolgeKodu="010005",FirmaKullanici=firmaKullanicilar[1],RefTalepKonuId=2,TalepTarih=DateTime.Now,CevapDurum = true, CevapDetayGumruk = cevapDetayi[0]},
                new TalepDetayFirma {TalepReferansNo=201806000003, VergiNo=1111111111,BolgeKodu="010005",FirmaKullanici=firmaKullanicilar[2],RefTalepKonuId=3,TalepTarih=DateTime.Now,CevapDurum = false, CevapDetayGumruk = null},

            };
            talepDetayi.ForEach(z => context.TalepDetayi.Add(z));

            context.SaveChanges();
        }
    }
}
