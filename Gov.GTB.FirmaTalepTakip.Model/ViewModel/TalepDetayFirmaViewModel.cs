using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Model.ViewModel
{
    public class TalepDetayFirmaViewModel
    {
        public long Id { get; set; }

        public long SiraNo { get; set; }

        public long TalepReferansNo { get; set; }

        public long VergiNo { get; set; }

        public int FirmaKullaniciId { get; set; }
        public FirmaKullanici FirmaKullanici { get; set; }

        [Required(ErrorMessage = "Talep açıklaması giriniz!")]
        public string KonuTalepAciklama { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? TalepTarih { get; set; }

        [MaxLength(500)]
        public string BolgeKodu { get; set; }

        public bool CevapDurum { get; set; }

        [Required(ErrorMessage = "Talep konusu seçiniz!")]
        public int RefTalepKonuId { get; set; }
        public virtual RefTalepKonu RefTalepKonu { get; set; }

        public int? CevapDetayGumrukId { get; set; }
        public CevapDetayGumruk CevapDetayGumruk { get; set; }

        public IEnumerable<RefTalepKonu> Konular { get; set; }

        [MaxLength(11)]
        public string TcNoIrtibatPersoneli { get; set; }

        [MaxLength(500)]
        public string FirmaAdi { get; set; }

        public string IrtibatPersoneli { get; set; }

        public string CevaplayanPersonel { get; set; }

        public TalepDetayFirmaViewModel()
        {
            Konular = new List<RefTalepKonu>();
        }
    }
}
