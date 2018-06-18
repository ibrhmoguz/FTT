using System.Collections.Generic;
using Gov.GTB.FirmaTalepTakip.Model.Entities;
using Gov.GTB.FirmaTalepTakip.Model.ViewModel;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface ITalepDetayFirmaRepository
    {
        IEnumerable<TalepDetayFirma> TalepListesi(long kullaniciTcNo, string bolgeKodu);
        TalepDetayFirma TalepDetayGetir(long talepId);
        TalepDetayFirma TalepDetayGetirReferansNoIle(long talepReferansNo);
        bool TalepKaydetGuncelle(TalepDetayFirma talep);
        bool TalepCevapla(CevapViewModel talepCevap);
    }
}
