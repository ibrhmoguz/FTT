using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gov.GTB.FirmaTalepTakip.Model.ViewModel
{
    public class FirmaViewModel
    {
        public int FirmaId { get; set; }

        [Required(ErrorMessage = "Vergi numarası giriniz!")]
        [DisplayName("Vergi No")]
        public string VergiNo { get; set; }

        [MaxLength(11)]
        [DisplayName("İrtibat Personeli Kimlik No")]
        public string TcNoIrtibatPersoneli { get; set; }

        [MaxLength(500)]
        [Required(ErrorMessage = "Firma adı giriniz!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Bölge adı seçiniz!")]
        [DisplayName("Bölge Adı")]
        public string BolgeKodu { get; set; }

        public IEnumerable<GumrukKodViewModel> GumrukKodlari { get; set; }

        public bool IsDisabled { get; set; }

        public int SiraNo { get; set; }

        public long? GumrukKullaniciId { get; set; }

        public string GumrukKullaniciAdSoyad { get; set; }
    }
}
