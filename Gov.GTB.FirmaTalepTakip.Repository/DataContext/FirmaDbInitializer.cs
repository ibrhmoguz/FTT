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
                new GumrukKullanici{TcNo=11111111112,Adi="GGM", Soyadi="GGM", BolgeKodu="040001", Durum=true,Email="GGM@gumruk.gov.tr", Rol = roles[1]},
                new GumrukKullanici{TcNo=11111111113,Adi="BM", Soyadi="BM", BolgeKodu="040001", Durum=true,Email="BM@gumruk.gov.tr",  Rol = roles[2]},
                new GumrukKullanici{TcNo=11111111114,Adi="BIP", Soyadi="BIP", BolgeKodu="040001", Durum=true,Email="BIP@gumruk.gov.tr",  Rol = roles[3]}
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
                new Firma { VergiNo=124123, Adi="Firma1", BolgeKodu = "040001", TcNoIrtibatPersoneli= "11111111114"},
            };
            firmalar.ForEach(f => context.Firmalar.Add(f));

            var gumrukKodlari = new List<GumrukKod>
            {
                new GumrukKod {Adi = "GÜMRÜKLER GENEL MÜDÜRLÜĞÜ MÜDÜRLÜĞÜ", BolgeKodu = "010000"},
                new GumrukKod {Adi = "GÜRBULAK GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "040001"},
                new GumrukKod {Adi = "ORTA ANADOLU GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "060000"},
                new GumrukKod {Adi = "BATI AKDENİZ GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "070000"},
                new GumrukKod {Adi = "KAÇKAR GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "080000"},
                new GumrukKod {Adi = "ULUDAĞ GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "160000"},
                new GumrukKod {Adi = "İPEKYOLU GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "210000"},
                new GumrukKod {Adi = "TRAKYA  GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "220000"},
                new GumrukKod {Adi = "GAP GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "270000"},
                new GumrukKod {Adi = "DOĞU AKDENİZ GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "310000"},
                new GumrukKod {Adi = "ORTA AKDENİZ GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "330000"},
                new GumrukKod {Adi = "İSTANBUL GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "340000"},
                new GumrukKod {Adi = "EGE GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "350000"},
                new GumrukKod {Adi = "DOĞU MARMARA GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "410000"},
                new GumrukKod {Adi = "FIRAT GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "440000"},
                new GumrukKod {Adi = "ORTA KARADENİZ GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "550000"},
                new GumrukKod {Adi = "BATI MARMARA GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "590000"},
                new GumrukKod {Adi = "DOĞU KARADENİZ GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "610000"},
                new GumrukKod {Adi = "DOĞU KARADENİZ GÜMRÜK VE TİCARET BÖLGE MÜDÜRLÜĞÜ", BolgeKodu = "650000"}
            };
            gumrukKodlari.ForEach(g => context.GumrukKodlari.Add(g));


            var cevapKonulari = new List<RefTalepCevap>
            {
                new RefTalepCevap {CKonu = "Talep, Bakanlık Merkez teşkilatına iletildi."},
                new RefTalepCevap {CKonu = "Talep, ilgili gümrük müdürlükleri nezdinde incelenmektedir."},
                new RefTalepCevap {CKonu = "Talep hakkında ilgili gümrük idarelerine gerekli talimat verildi."}
            };
            cevapKonulari.ForEach(c => context.CevapKonulari.Add(c));


            var talepKonulari = new List<RefTalepKonu>
            {
                new RefTalepKonu {TKonu = "Tarife,Menşe,Kıymet"},
                new RefTalepKonu {TKonu = "İthalat"},
                new RefTalepKonu {TKonu = "İhracat"},
                new RefTalepKonu {TKonu = "Antrepo"},
                new RefTalepKonu {TKonu = "Dahilde İşleme"},
                new RefTalepKonu {TKonu = "Geçici İthalat"},
                new RefTalepKonu {TKonu = "TIR Sistemi"},
                new RefTalepKonu {TKonu = "Transit İşlemleri"},
                new RefTalepKonu {TKonu = "Muafiyetler"},
                new RefTalepKonu {TKonu = "Gümrüklerde Uygulanan Cezalar"},
                new RefTalepKonu {TKonu = "Gümrük Personeli"},
                new RefTalepKonu {TKonu = "Sınır Kapıları"},
                new RefTalepKonu {TKonu = "Yetkilendirilmiş Yükümlü Mevzuatı ve Uygulamaları"},
                new RefTalepKonu {TKonu = "Elektronik Gümrük İşlemleri ve TPS"},
                new RefTalepKonu {TKonu = "Risk Analizleri"},
                new RefTalepKonu {TKonu = "Gümrük Müşavirleri"},
                new RefTalepKonu {TKonu = "Sonradan Kontrol İşlemleri "},
                new RefTalepKonu {TKonu = "Diğer "}
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
                new TalepDetayFirma {TalepReferansNo=201806000001, VergiNo=firmalar[0].VergiNo,BolgeKodu="040001",FirmaKullanici=firmaKullanicilar[0],RefTalepKonuId=1,TalepTarih=DateTime.Now,CevapDurum = false,CevapDetayGumruk = null},
                new TalepDetayFirma {TalepReferansNo=201806000002, VergiNo=firmalar[0].VergiNo,BolgeKodu="040001",FirmaKullanici=firmaKullanicilar[1],RefTalepKonuId=2,TalepTarih=DateTime.Now,CevapDurum = true, CevapDetayGumruk = cevapDetayi[0]},
                new TalepDetayFirma {TalepReferansNo=201806000003, VergiNo=firmalar[0].VergiNo,BolgeKodu="040001",FirmaKullanici=firmaKullanicilar[2],RefTalepKonuId=3,TalepTarih=DateTime.Now,CevapDurum = false, CevapDetayGumruk = null},

            };
            talepDetayi.ForEach(z => context.TalepDetayi.Add(z));

            context.SaveChanges();
        }
    }
}
