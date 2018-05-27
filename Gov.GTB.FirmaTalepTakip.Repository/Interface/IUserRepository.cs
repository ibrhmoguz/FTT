using System.Collections.Generic;
using System.Threading.Tasks;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Kullanici> KullanicilariGetir();
        IEnumerable<GumrukKullanici> GumrukKullanicilariGetir();
        IEnumerable<FirmaKullanici> FirmaKullanicilariGetir();
        Task<bool> FirmaKullaniciKaydetGuncelle(FirmaKullanici firmaKullanici);
    }
}
