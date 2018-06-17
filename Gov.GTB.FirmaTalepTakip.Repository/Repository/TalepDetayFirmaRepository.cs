using System;
using System.Collections.Generic;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
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

        public IEnumerable<TalepDetayFirma> TalepListesi(long firmaVergiNo)
        {
            var talepList = _dbContext.TalepDetayi.Where(firma => firma.VergiNo == firmaVergiNo);
            return talepList.Any() ? talepList.ToList() : null;
        }

        public TalepDetayFirma TalepDetayGetir(long talepReferansNo)
        {
            return _dbContext.TalepDetayi.FirstOrDefault(f => f.TalepReferansNo == talepReferansNo);
        }

        public bool TalepKaydetGuncelle(TalepDetayFirma talepDetay)
        {
            using (var dbTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (talepDetay.TalepReferansNo != 0)
                    {
                        var talepDetayFromDb = this.TalepDetayGetir(talepDetay.TalepReferansNo);
                        talepDetayFromDb.TcNoFirmaKullanici = talepDetay.TcNoFirmaKullanici;
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
                        var maxTalepDetay = _dbContext.TalepDetayi.OrderByDescending(firma => firma.Id).FirstOrDefault();
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

                    // Talep Detay Log
                    var talepDetayLog = new TalepDetayFirmaLog
                    {
                        BolgeKodu = talepDetay.BolgeKodu,
                        CevapDurum = talepDetay.CevapDurum,
                        KonuTalepAciklama = talepDetay.KonuTalepAciklama,
                        KonuTalepBaslik = _dbContext.TalepKonulari.FirstOrDefault(konu => konu.Id == talepDetay.RefTalepKonuId)?.TKonu,
                        TalepReferansNo = talepDetay.TalepReferansNo,
                        TalepTarih = talepDetay.TalepTarih,
                        TcNoFirmaKullanici = talepDetay.TcNoFirmaKullanici,
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
    }
}
