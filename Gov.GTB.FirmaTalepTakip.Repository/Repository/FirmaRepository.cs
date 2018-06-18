using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class FirmaRepository : IFirmaRepository
    {
        private readonly FirmaDbContext _dbContext;
        public FirmaRepository(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Firma> FirmaListesi()
        {
            return _dbContext.Firmalar.Include(firma => firma.BolgeKod).ToList();
        }

        public Firma FirmaGetir(int firmaId)
        {
            return _dbContext.Firmalar.Include(firma => firma.BolgeKod).FirstOrDefault(f => f.FirmaId == firmaId);
        }

        public bool FirmaKaydetGuncelle(Firma firma)
        {
            if (firma.FirmaId != 0)
            {
                var firmaFromDb = this.FirmaGetir(firma.FirmaId);
                firmaFromDb.Adi = firma.Adi;
                firmaFromDb.VergiNo = firma.VergiNo;
                firmaFromDb.TcNoIrtibatPersoneli = firma.TcNoIrtibatPersoneli;
                firmaFromDb.BolgeKodu = firma.BolgeKodu;
            }
            else
            {
                _dbContext.Firmalar.Add(firma);
            }

            _dbContext.SaveChanges();
            return true;
        }

        public bool FirmaSil(int firmaId)
        {
            if (firmaId <= 0) return false;

            var firmaFromDb = this.FirmaGetir(firmaId);
            _dbContext.Firmalar.Remove(firmaFromDb);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
