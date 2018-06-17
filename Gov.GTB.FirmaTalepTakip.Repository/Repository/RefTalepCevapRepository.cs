using System.Collections.Generic;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class RefTalepCevapRepository : IRefTalepCevapRepository
    {
        private readonly FirmaDbContext _dbContext;
        public RefTalepCevapRepository(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<RefTalepCevap> TalepCevapListesi()
        {
            return _dbContext.CevapKonulari.ToList();
        }
    }
}
