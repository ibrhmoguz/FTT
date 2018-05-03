using System.ComponentModel.DataAnnotations;

namespace Gov.GTB.FirmaTalepTakip.Model.Entities
{
    public class FirmaKullanici : Kullanici
    {
        [MaxLength(50)]
        public string Sifre { get; set; }

        [Required]
        public long VergiNo { get; set; }

        [MaxLength(50)]
        public string Telefon { get; set; }
    }
}
