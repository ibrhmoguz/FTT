using System;
using System.ComponentModel.DataAnnotations;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Model.ViewModel
{
    public class CevapViewModel
    {
        public long TalepReferansNo { get; set; }

        public int RefTalepCevapId { get; set; }

        public RefTalepCevap RefTalepCevap { get; set; }

        [MaxLength(1000)]
        public string CevapAciklama { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CevapTarih { get; set; }
    }
}
