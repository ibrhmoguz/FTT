using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.GTB.FirmaTalepTakip.Model.Entities
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolId { get; set; }

        [MaxLength(5)]
        [Required]
        public string Code { get; set; }

        [MaxLength(100)]
        [Required]
        public string Adi { get; set; }
    }
}
