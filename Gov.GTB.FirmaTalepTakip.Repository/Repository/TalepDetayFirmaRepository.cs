using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class TalepDetayFirmaRepository : ITalepDetayFirmaRepository
    {
        private readonly FirmaDbContext _dbContext;
        public TalepDetayFirmaRepository(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TalepDetayFirmaViewModel> TalepListesi(long kullaniciTcNo, string bolgeKodu)
        {
            List<TalepDetayFirmaViewModel> talepList;
            if (!string.IsNullOrEmpty(bolgeKodu))
            {
                talepList = (from td in _dbContext.TalepDetayi
                                                  .Include(t => t.RefTalepKonu)
                                                  .Include(t => t.CevapDetayGumruk)
                                                  .Include(t => t.FirmaKullanici)
                             join k in _dbContext.FirmaKullanicilar on td.FirmaKullaniciId equals k.Id
                             join f in _dbContext.Firmalar on k.VergiNo equals f.VergiNo
                             where td.BolgeKodu == bolgeKodu
                             select new TalepDetayFirmaViewModel
                             {
                                 TalepReferansNo = td.TalepReferansNo,
                                 CevapDetayGumrukId = td.CevapDetayGumrukId,
                                 VergiNo = td.VergiNo,
                                 Id = td.Id,
                                 CevapDurum = td.CevapDurum,
                                 RefTalepKonuId = td.RefTalepKonuId,
                                 BolgeKodu = td.BolgeKodu,
                                 FirmaKullanici = k,
                                 FirmaAdi = f.Adi,
                                 KonuTalepAciklama = td.KonuTalepAciklama,
                                 RefTalepKonu = td.RefTalepKonu,
                                 TalepTarih = td.TalepTarih,
                                 TcNoIrtibatPersoneli = k.Adi + " " + k.Soyadi,
                                 CevapDetayGumruk = td.CevapDetayGumruk
                             }).ToList();
            }
            else
            {
                talepList = (from td in _dbContext.TalepDetayi
                                                  .Include(t => t.RefTalepKonu)
                                                  .Include(t => t.CevapDetayGumruk)
                                                  .Include(t => t.FirmaKullanici)
                             where td.VergiNo == (_dbContext.FirmaKullanicilar
                                                             .FirstOrDefault(kullanici => kullanici.TcNo == kullaniciTcNo)
                                                  ).VergiNo
                             select new TalepDetayFirmaViewModel
                             {
                                 TalepReferansNo = td.TalepReferansNo,
                                 CevapDetayGumrukId = td.CevapDetayGumrukId,
                                 VergiNo = td.VergiNo,
                                 Id = td.Id,
                                 CevapDurum = td.CevapDurum,
                                 RefTalepKonuId = td.RefTalepKonuId,
                                 BolgeKodu = td.BolgeKodu,
                                 FirmaKullanici = td.FirmaKullanici,
                                 KonuTalepAciklama = td.KonuTalepAciklama,
                                 RefTalepKonu = td.RefTalepKonu,
                                 TalepTarih = td.TalepTarih,
                                 CevapDetayGumruk = td.CevapDetayGumruk
                             }).ToList();
            }

            return talepList;
        }

        public TalepDetayFirma TalepDetayGetir(long talepId)
        {
            return _dbContext.TalepDetayi
                .Include(t => t.RefTalepKonu)
                .Include(t => t.CevapDetayGumruk)
                .Include(t => t.FirmaKullanici)
                .FirstOrDefault(f => f.Id == talepId);
        }

        public TalepDetayFirma TalepDetayGetirReferansNoIle(long talepReferansNo)
        {
            return _dbContext.TalepDetayi
                .Include(t => t.RefTalepKonu)
                .Include(t => t.CevapDetayGumruk)
                .Include(t => t.FirmaKullanici)
                .FirstOrDefault(f => f.TalepReferansNo == talepReferansNo);
        }

        public bool TalepKaydetGuncelle(TalepDetayFirma talepDetay)
        {
            using (var dbTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var firma = _dbContext.Firmalar.FirstOrDefault(f => f.VergiNo == talepDetay.VergiNo);
                    talepDetay.BolgeKodu = firma?.BolgeKodu;

                    if (talepDetay.TalepReferansNo != 0)
                    {
                        var talepDetayFromDb = this.TalepDetayGetirReferansNoIle(talepDetay.TalepReferansNo);
                        talepDetayFromDb.FirmaKullaniciId = talepDetay.FirmaKullaniciId;
                        talepDetayFromDb.RefTalepKonuId = talepDetay.RefTalepKonuId;
                        talepDetayFromDb.KonuTalepAciklama = talepDetay.KonuTalepAciklama;
                        talepDetayFromDb.TalepTarih = talepDetay.TalepTarih;
                        talepDetayFromDb.CevapDetayGumrukId = talepDetay.CevapDetayGumrukId;
                        talepDetayFromDb.VergiNo = talepDetay.VergiNo;
                        talepDetayFromDb.BolgeKodu = talepDetay.BolgeKodu;
                        talepDetayFromDb.CevapDurum = talepDetay.CevapDurum;
                    }
                    else
                    {
                        var maxTalepDetay = _dbContext.TalepDetayi.OrderByDescending(td => td.Id).FirstOrDefault();
                        if (maxTalepDetay == null || maxTalepDetay.TalepReferansNo.ToString().Substring(0, 6) != DateTime.Now.ToString("yyyyMM"))
                        {
                            talepDetay.TalepReferansNo = Convert.ToInt64(DateTime.Now.ToString("yyyyMM000001"));
                        }
                        else
                        {
                            talepDetay.TalepReferansNo = maxTalepDetay.TalepReferansNo + 1;
                        }
                        _dbContext.TalepDetayi.Add(talepDetay);
                    }

                    var firmaKullanici = _dbContext.FirmaKullanicilar.FirstOrDefault(fk => fk.Id == talepDetay.FirmaKullaniciId);
                    // Talep Detay Log
                    var talepDetayLog = new TalepDetayFirmaLog
                    {
                        BolgeKodu = talepDetay.BolgeKodu,
                        CevapDurum = talepDetay.CevapDurum,
                        KonuTalepAciklama = talepDetay.KonuTalepAciklama,
                        KonuTalepBaslik = _dbContext.TalepKonulari.FirstOrDefault(konu => konu.Id == talepDetay.RefTalepKonuId)?.TKonu,
                        TalepReferansNo = talepDetay.TalepReferansNo,
                        TalepTarih = talepDetay.TalepTarih,
                        FirmaKullanici = (firmaKullanici != null) ? firmaKullanici.TcNo + "-" + firmaKullanici.Adi + " " + firmaKullanici.Soyadi : null,
                        VergiNo = talepDetay.VergiNo,
                        CevapDetayGumrukId = talepDetay.CevapDetayGumrukId,
                        IslemTarih = DateTime.Now
                    };
                    _dbContext.TalepDetayiLog.Add(talepDetayLog);

                    _dbContext.SaveChanges();
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool TalepCevapla(CevapViewModel talepCevap)
        {
            if (!talepCevap.CevapDetayGumrukId.HasValue)
            {
                using (var dbTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var cevap = new CevapDetayGumruk
                        {
                            CevapAciklama = talepCevap.CevapAciklama,
                            CevapTarih = DateTime.Now,
                            RefTalepCevapId = talepCevap.RefTalepCevapId,
                            TcNoIrtibatPersoneli = talepCevap.TcNoIrtibatPersoneli
                        };
                        _dbContext.CevapDetayi.Add(cevap);
                        _dbContext.SaveChanges();

                        var talep = _dbContext.TalepDetayi.FirstOrDefault(td => td.TalepReferansNo == talepCevap.TalepReferansNo);
                        if (talep != null)
                        {
                            talep.CevapDetayGumrukId = cevap.Id;
                            talep.CevapDurum = true;
                        }

                        _dbContext.SaveChanges();
                        dbTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        return false;
                    }
                }
            }
            else
            {
                var cevap = _dbContext.CevapDetayi.FirstOrDefault(cd => cd.Id == talepCevap.CevapDetayGumrukId.Value);
                if (cevap != null)
                {
                    cevap.CevapAciklama = talepCevap.CevapAciklama;
                    cevap.CevapTarih = DateTime.Now;
                    cevap.RefTalepCevapId = talepCevap.RefTalepCevapId;
                    cevap.TcNoIrtibatPersoneli = talepCevap.TcNoIrtibatPersoneli;

                    _dbContext.SaveChanges();
                }
            }

            return true;
        }
    }
}
