using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gov.GTB.FirmaTalepTakip.Model.Entities
{
    public abstract class Kullanici
    {
        [Key]
        public long Id { get; set; }

        [DisplayName("TC Kimlik No")]
        public long TcNo { get; set; }

        [MaxLength(50)]
        [Required]
        public string Adi { get; set; }

        [MaxLength(50)]
        [Required]
        public string Soyadi { get; set; }

        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [Required]
        public bool Durum { get; set; }

        public int RolId { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
