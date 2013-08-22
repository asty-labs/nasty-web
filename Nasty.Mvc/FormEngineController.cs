using System.Web.Mvc;
using System;
using Nasty.Core;

namespace Nasty.Mvc
{
    /**
     * Servlet to handle all requests to FormEngine
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class FormEngineController : Controller {

        public void Index() {
            var formEngine = FormEngineFactory.Instance.GetFormEngine(System.Web.HttpContext.Current, ControllerContext);
            var expr = formEngine.DoProcess();

            var req = System.Web.HttpContext.Current.Request;
            var resp = System.Web.HttpContext.Current.Response;

            //if(resp.isCommitted()) return;

            resp.Charset = "UTF-8";
            resp.AddHeader("Cache-Control", "no-cache");
            resp.AddHeader("Cache-Control", "no-store");
            resp.AddHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
            resp.AddHeader("Pragma", "no-cache"); // HTTP 1.0
            resp.Expires = 0;
            resp.ExpiresAbsolute = DateTime.Now; // Proxies.

            var writer = resp.Output;
            var script = expr.Encode();

            if(req.ContentType.StartsWith("multipart/form-data")) {
                // to avoid displaying download dialog put script in html-tag.
                // see jquery.form.js docs

                var userAgent = req.Headers["user-agent"].ToLower();

                // STK: performance fix for firefox, when trying to render big javascript in textarea - use span containing comment instead
                if(userAgent.Contains("firefox"))
                {
                    writer.Write("<span><!--");
                    writer.Write(script);
                    writer.Write("--></span>");
                }
                else
                {
                    writer.Write("<textarea>");
                    writer.Write(script);
                    writer.Write("</textarea>");
                }
            }
            else {
                resp.ContentType = "text/javascript";
                writer.Write(script);
            }
        }

        public FileStreamResult Scripts(string id)
        {
            var resource = typeof(FormEngine).Assembly.GetManifestResourceStream("Nasty.Scripts." + id);
            return new FileStreamResult(resource, GetContentType(id));
        }

        private static string GetContentType(string fileName)
        {
            if (fileName.EndsWith(".js"))
            {
                return "text/javascript";
            }
            return fileName.EndsWith(".css") ? "text/stylesheet" : "text";
        }
    }

    public static class UrlHelperExtension
    {
        public static string FormEngineScript(this UrlHelper urlHelper, string scriptName)
        {
            return urlHelper.Action("Scripts/" + scriptName, "FormEngine");
        }

        public static string FormEngine(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Index", "FormEngine");
        }
    }
}