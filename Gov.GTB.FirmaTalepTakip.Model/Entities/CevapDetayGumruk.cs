using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.GTB.FirmaTalepTakip.Model.Entities
{
    public class CevapDetayGumruk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TcNoIrtibatPersoneli { get; set; }

        public int RefTalepCevapId { get; set; }
        public virtual RefTalepCevap RefTalepCevap { get; set; }

        [MaxLength(1000)]
        public string CevapAciklama { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CevapTarih { get; set; }
    }
}
