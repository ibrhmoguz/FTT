using System.Collections.Generic;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class GumrukKodRepository : IGumrukKodRepository
    {
        private readonly FirmaDbContext _dbContext;
        public GumrukKodRepository(FirmaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<GumrukKod> BolgeKodListesi()
        {
            return _dbContext.GumrukKodlari.ToList();
        }
    }
}
