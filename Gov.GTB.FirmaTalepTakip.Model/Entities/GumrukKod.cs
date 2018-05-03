using System.ComponentModel.DataAnnotations;

namespace Gov.GTB.FirmaTalepTakip.Model.Entities
{
    public class GumrukKod
    {
        [Key]
        [MaxLength(50)]
        public string BolgeKodu { get; set; }

        [MaxLength(500)]
        [Required]
        public string Adi { get; set; }

    }
}
