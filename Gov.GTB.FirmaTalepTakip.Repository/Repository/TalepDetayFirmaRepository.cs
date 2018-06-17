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

        public TalepDetayFirma TalepDetayGetir(string talepReferansNo)
        {
            return _dbContext.TalepDetayi.FirstOrDefault(f => f.TalepReferansNo == talepReferansNo);
        }

        public bool TalepKaydetGuncelle(TalepDetayFirma talepDetay)
        {
            if (talepDetay.TalepReferansNo != null)
            {
                var talepDetayFromDb = this.TalepDetayGetir(talepDetay.TalepReferansNo);
                talepDetayFromDb.TcNoFirmaKullanici = talepDetay.TcNoFirmaKullanici;
                talepDetayFromDb.KonuTalepBaslik = talepDetay.KonuTalepBaslik;
                talepDetayFromDb.KonuTalepAciklama = talepDetay.KonuTalepAciklama;
                talepDetayFromDb.TalepTarih = talepDetay.TalepTarih;
            }
            else
            {
                _dbContext.TalepDetayi.Add(talepDetay);
            }

            _dbContext.SaveChanges();
            return true;
        }
    }
}
