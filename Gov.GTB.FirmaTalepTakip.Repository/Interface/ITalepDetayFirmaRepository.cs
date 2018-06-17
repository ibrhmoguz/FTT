using System.Collections.Generic;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface ITalepDetayFirmaRepository
    {
        IEnumerable<TalepDetayFirma> TalepListesi(long firmaVergiNo);
        TalepDetayFirma TalepDetayGetir(string talepReferansNo);
        bool TalepKaydetGuncelle(TalepDetayFirma talep);
    }
}
