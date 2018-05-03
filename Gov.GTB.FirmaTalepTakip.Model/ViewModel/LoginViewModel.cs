using System.ComponentModel.DataAnnotations;

namespace Gov.GTB.FirmaTalepTakip.Model.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string TcNo { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
