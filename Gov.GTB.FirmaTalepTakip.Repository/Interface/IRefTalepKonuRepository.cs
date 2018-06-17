using System.Collections.Generic;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface IRefTalepKonuRepository
    {
        IEnumerable<RefTalepKonu> TalepKonuListesi();
    }
}
