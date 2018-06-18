using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Model.ViewModel
{
    public class CevapViewModel
    {
        public long TalepReferansNo { get; set; }

        [Required(ErrorMessage = "Cevap konusu seçiniz!")]
        public int RefTalepCevapId { get; set; }

        public RefTalepCevap RefTalepCevap { get; set; }

        [MaxLength(1000)]
        [Required(ErrorMessage = "Cevap açıklaması giriniz!")]
        public string CevapAciklama { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CevapTarih { get; set; }

        public string TalepKonu { get; set; }

        public string TalepAciklama { get; set; }

        public IEnumerable<RefTalepCevap> CevapBasliklar { get; set; }

        public CevapViewModel()
        {
            CevapBasliklar = new List<RefTalepCevap>();
        }
    }
}
