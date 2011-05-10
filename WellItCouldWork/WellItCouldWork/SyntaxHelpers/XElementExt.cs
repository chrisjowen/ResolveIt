using System.Xml.Linq;

namespace WellItCouldWork.SyntaxHelpers
{
    public static class XElementExt
    {
        public static bool HasAttribute(this XElement el, string attributeName)
        {
            return el != null && el.Attribute(attributeName) != null;
        }
    }
}