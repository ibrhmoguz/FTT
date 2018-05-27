using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Gov.GTB.FirmaTalepTakip.Web.Helpers
{
    public class MailHelper
    {
        private readonly SmtpClient _mailClient;

        public MailHelper()
        {
            this._mailClient = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["SmtpHost"],
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUsername"],
                    ConfigurationManager.AppSettings["SmtpPass"]),
                EnableSsl = true
            };
        }

        public async Task SendMail(string toMailAddress, string body)
        {
            const string subject = "Firma Kullanıcı Talep Onayı";
            await _mailClient.SendMailAsync(ConfigurationManager.AppSettings["SmtpUsername"], toMailAddress, subject, body);
        }
    }
}