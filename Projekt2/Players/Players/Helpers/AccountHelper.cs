using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Players.Helpers
{
    public static class AccountHelper
    {
        public static IHtmlString CustomHeader(this HtmlHelper helper, string content)
        {
            string LableStr = $"<h2 style=\"color:#DDF;font-size:39px;font-family:'Facebook Letter Faces' \">{content}</h2>";
            return new HtmlString(LableStr);
        }
    }
}