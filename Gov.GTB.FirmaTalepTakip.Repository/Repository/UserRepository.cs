using System.Collections.Generic;
using System.Linq;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Repository.DataContext;
using Gov.GTB.FirmaTalepTakip.Repository.Interface;

namespace Gov.GTB.FirmaTalepTakip.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        FirmaDbContext dbContext;
        public UserRepository(FirmaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Kullanici> KullanicilariGetir()
        {
            return dbContext.Kullanici.ToList();
        }

        public IEnumerable<GumrukKullanici> GumrukKullanicilariGetir()
        {
            return dbContext.GumrukKullanicilar.ToList();
        }

        public IEnumerable<FirmaKullanici> FirmaKullanicilariGetir()
        {
            return dbContext.FirmaKullanicilar.ToList();
        }
    }
}
