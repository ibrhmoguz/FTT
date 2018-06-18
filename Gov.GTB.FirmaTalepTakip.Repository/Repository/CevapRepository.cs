using System.Data.Entity;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class CevapRepository : ICevapRepository
    {
        private readonly FirmaDbContext _dbContext;
        public CevapRepository(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CevapDetayGumruk TalepCevabiGetir(long talepId)
        {
            var cevap = new CevapDetayGumruk();
            var talep = _dbContext.TalepDetayi.FirstOrDefault(t => t.Id == talepId);
            if (talep != null && talep.CevapDetayGumrukId.HasValue)
            {
                cevap = _dbContext.CevapDetayi
                                  .Include(cd => cd.RefTalepCevap)
                                  .FirstOrDefault(c => c.Id == talep.CevapDetayGumrukId.Value);
            }

            return cevap;
        }
    }
}
