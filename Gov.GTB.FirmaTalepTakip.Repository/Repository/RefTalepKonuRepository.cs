using System.Collections.Generic;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class RefTalepKonuRepository : IRefTalepKonuRepository
    {
        private readonly FirmaDbContext _dbContext;
        public RefTalepKonuRepository(FirmaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<RefTalepKonu> TalepKonuListesi()
        {
            return _dbContext.TalepKonulari.ToList();
        }
    }
}
