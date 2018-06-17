using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.GTB.FirmaTalepTakip.Model.Entities
{
    public class TalepDetayFirma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long TalepReferansNo { get; set; }

        public long VergiNo { get; set; }

        public long FirmaKullaniciId { get; set; }
        public virtual FirmaKullanici FirmaKullanici { get; set; }

        [MaxLength(1000)]
        public string KonuTalepAciklama { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? TalepTarih { get; set; }

        [MaxLength(500)]
        public string BolgeKodu { get; set; }

        public bool CevapDurum { get; set; }

        public int RefTalepKonuId { get; set; }
        public virtual RefTalepKonu RefTalepKonu { get; set; }

        public int? CevapDetayGumrukId { get; set; }
        public virtual CevapDetayGumruk CevapDetayGumruk { get; set; }
    }
}
