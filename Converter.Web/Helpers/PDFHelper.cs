using ChromeHtmlToPdfLib;
using ChromeHtmlToPdfLib.Enums;
using ChromeHtmlToPdfLib.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Converter.Web.Helpers
{
    public class PDFHelper
    {
        public static byte[] GeneratePDFUsingChrome(string url)
        {
            var filePath = HttpContext.Current?.Server?.MapPath($"~/App_Data/${Guid.NewGuid()}.pdf");
            //var chromePath = HttpContext.Current?.Server?.MapPath("~/Chrome/App/Chrome-bin/chrome.exe");
            var chromePath = HttpContext.Current?.Server?.MapPath("~/Chrome/GoogleChromePortable.exe");

            using (var converter = new ChromeHtmlToPdfLib.Converter(chromePath))
            {
                var settings = new PageSettings(PaperFormat.A4)
                {
                    PrintBackground = true
                };
                using (var stream = new MemoryStream())
                {
                    converter.ConvertToPdf(new ConvertUri(url), stream, settings);
                    if (File.Exists(filePath))
                    {
                        var data = File.ReadAllBytes(filePath);
                        File.Delete(filePath);
                        return data;
                    }
                }
            }

            return null;
        }
    }
}