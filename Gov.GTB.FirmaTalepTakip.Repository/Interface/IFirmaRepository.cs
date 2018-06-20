using System.Collections.Generic;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface IFirmaRepository
    {
        IEnumerable<Firma> FirmaListesi();
        Firma FirmaGetir(int firmaId);
        bool FirmaKaydetGuncelle(Firma firma);
        bool FirmaSil(int firmaId);
        IEnumerable<GorevlendirmeFirmaViewModel> GorevlendirilecekFirmalariGetir(string bolgeKodu);
        bool FirmaPersonelGorevlendir(int firmaId, long kullaniciId);
    }
}
