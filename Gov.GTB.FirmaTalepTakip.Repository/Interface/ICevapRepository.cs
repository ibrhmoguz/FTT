using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Repository.Interface
{
    public interface ICevapRepository
    {
        CevapDetayGumruk TalepCevabiGetir(long talepId);
    }
}
