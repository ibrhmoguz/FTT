using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gov.GTB.FirmaTalepTakip.Model.Entities;

namespace Gov.GTB.FirmaTalepTakip.Model.ViewModel
{
    public class FirmaKullaniciViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Kimlik numarası giriniz!")]
        [DisplayName("TC Kimlik No")]
        public string TcNo { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı giriniz!")]
        [MaxLength(50)]
        public string Adi { get; set; }

        [Required(ErrorMessage = "Kullanıcı soyadı giriniz!")]
        [MaxLength(50)]
        public string Soyadi { get; set; }

        [Required(ErrorMessage = "E-mail giriniz!")]
        [MaxLength(50)]
        public string Email { get; set; }

        public bool Durum { get; set; }

        public int RolId { get; set; }
        public virtual Rol Rol { get; set; }

        [Required(ErrorMessage = "Şifre giriniz!")]
        [MaxLength(50)]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Vergi numarası zorunlu!")]
        public string VergiNo { get; set; }

        [Required(ErrorMessage = "Telefon giriniz!")]
        [MaxLength(50)]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Firma seçiniz!")]
        public long FirmaId { get; set; }

        public IEnumerable<Firma> Firmalar { get; set; }

        public FirmaKullaniciViewModel()
        {
            Firmalar = new List<Firma>();
        }
    }
}
