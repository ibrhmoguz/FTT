using System.Collections.Generic;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface IRefTalepCevapRepository
    {
        IEnumerable<RefTalepCevap> TalepCevapListesi();
    }
}
