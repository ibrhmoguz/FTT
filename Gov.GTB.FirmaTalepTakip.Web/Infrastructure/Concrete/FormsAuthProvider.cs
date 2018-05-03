using System.Web.Security;
using Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Abstract;

namespace Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public virtual bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            FormsAuthentication.SetAuthCookie(username, false);
            return true;
        }

        public virtual void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}