using System.Collections.Generic;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface ITalepDetayFirmaRepository
    {
        IEnumerable<TalepDetayFirma> TalepListesi(long kullaniciTcNo, string bolgeKodu);
        TalepDetayFirma TalepDetayGetir(long talepId);
        TalepDetayFirma TalepDetayGetirReferansNoIle(long talepReferansNo);
        bool TalepKaydetGuncelle(TalepDetayFirma talep);
    }
}
