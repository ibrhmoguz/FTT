using System.Collections.Generic;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class FirmaRepository : IFirmaRepository
    {
        FirmaDbContext dbContext;
        public FirmaRepository(FirmaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Firma> FirmaListesi()
        {
            return dbContext.Firmalar.ToList();
        }

        public Firma FirmaGetir(int firmaId)
        {
            return dbContext.Firmalar.FirstOrDefault(f => f.FirmaId == firmaId);
        }

        public bool FirmaKaydetGuncelle(Firma firma)
        {
            if (firma.FirmaId != 0)
            {
                var firmaFromDb = this.FirmaGetir(firma.FirmaId);
                firmaFromDb.BolgeKodu = firma.BolgeKodu;
            }
            else
            {
                dbContext.Firmalar.Add(firma);
            }

            dbContext.SaveChanges();
            return true;
        }
    }
}
