using System.Collections.Generic;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface IFirmaRepository
    {
        IEnumerable<Firma> FirmaListesi();
        Firma FirmaGetir(int firmaId);
        bool FirmaKaydetGuncelle(Firma firma);
    }
}
