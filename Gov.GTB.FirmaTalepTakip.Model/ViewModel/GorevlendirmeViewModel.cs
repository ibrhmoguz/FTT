using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gov.GTB.FirmaTalepTakip.Model.ViewModel
{
    public class GorevlendirmeViewModel
    {
        [Required(ErrorMessage = "Firma Seçiniz!")]
        public int FirmaId { get; set; }

        [Required(ErrorMessage = "Vergi numarası giriniz!")]
        [DisplayName("Vergi No")]
        public string VergiNo { get; set; }

        [Required(ErrorMessage = "Personel Seçiniz!")]
        public long GumrukKullaniciId { get; set; }

        public IEnumerable<GorevlendirmeFirmaViewModel> GorevlendirmeFirmaListesi { get; set; }

        public IEnumerable<GorevlendirmeKullaniciViewModel> GorevlendirmKullaniciListesi { get; set; }
    }
}
