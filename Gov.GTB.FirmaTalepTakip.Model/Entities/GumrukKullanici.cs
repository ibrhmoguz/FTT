using System.ComponentModel.DataAnnotations;

namespace Gov.GTB.FirmaTalepTakip.Model.Entities
{
    public class GumrukKullanici : Kullanici
    {
        [MaxLength(50)]
        [Required]
        public string BolgeKodu { get; set; }
    }
}
