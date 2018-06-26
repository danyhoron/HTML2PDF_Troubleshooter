using Converter.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Converter.Web.Controllers
{
    [RoutePrefix("converter")]
    public class ConverterController : ApiController
    {

        [HttpGet(), Route("pdf")]
        public HttpResponseMessage Convert(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var content = PDFHelper.GeneratePDFUsingChrome(url);
            if (content == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            var ms = new MemoryStream(content);
            HttpResponseMessage response = new HttpResponseMessage
            {
                Content = new StreamContent(ms),
                StatusCode = HttpStatusCode.OK
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentLength = content.Length;
            return response;
        }
    }
}
