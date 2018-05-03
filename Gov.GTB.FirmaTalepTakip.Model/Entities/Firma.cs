using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.GTB.FirmaTalepTakip.Model.Entities
{
    public class Firma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FirmaId { get; set; }

        public long VergiNo { get; set; }

        public string TcNoIrtibatPersoneli { get; set; }

        [MaxLength(500)]
        [Required]
        public string Adi { get; set; }

        [MaxLength(50)]
        [Required]
        public string BolgeKodu { get; set; }

        public virtual GumrukKod BolgeKod { get; set; }
    }
}
