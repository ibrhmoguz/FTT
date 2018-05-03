using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gov.GTB.FirmaTalepTakip.Model.ViewModel
{
    public class FirmaViewModel
    {
        public int FirmaId { get; set; }
        [DisplayName("Vergi No")]
        public string VergiNo { get; set; }

        [Required]
        public long TcNoIrtibatPersoneli { get; set; }

        [MaxLength(500)]
        [Required]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [MaxLength(50)]
        [Required]
        [DisplayName("Bölge No")]
        public string BolgeKodu { get; set; }
    }
}
