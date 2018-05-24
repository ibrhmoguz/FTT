using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gov.GTB.FirmaTalepTakip.Web.Infrastructure.Concrete
{
    public static class FirmaHtmlHelper
    {
        public static IHtmlString ValidationClass(string field, ModelStateDictionary modelState)
        {
            var fieldIndex = modelState.Keys.ToList().IndexOf(field);
            if (fieldIndex > -1)
            {
                var fieldValue = modelState.Values.ToList()[fieldIndex];
                if (fieldValue != null)
                {
                    if (fieldValue.Errors.Any())
                    {
                        return new HtmlString("has-error");
                    }
                }
            }
            return new HtmlString("");
        }

        public static IHtmlString Disabled(int id)
        {
            if (id > 0)
            {
                return new HtmlString("true");
            }
            return new HtmlString("false");
        }
    }
}