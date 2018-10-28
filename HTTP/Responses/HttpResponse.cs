using System;
using System.Text;
using HTTP.Common;
using HTTP.Enums;
using HTTP.Headers;
using HTTP.Extensions;
using HTTP.Cookies;

namespace HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse() { }

        public HttpResponse(HttpResponseStatusCode stausCode)
        {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
            this.StatusCode = stausCode;
            this.Cookies = new HttpCookieCollection();
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; private set; }

        public byte[] Content { get; set; }

        public IHttpCookieCollection Cookies { get; private set; }


        public void AddHeader(HttpHeader header) => this.Headers.Add(header);

        public byte[] GetBytes()
        {
            //return Encoding.UTF8.GetBytes(thsi.ToString()).Concat(this.Content).ToArray();

            byte[] response = Encoding.ASCII.GetBytes(this.ToString());

            byte[] responseContent = new byte[response.Length + this.Content.Length];
            Buffer.BlockCopy(response, 0, responseContent, 0, response.Length);
            Buffer.BlockCopy(this.Content, 0, responseContent, response.Length, this.Content.Length);

            return responseContent;
        }

        public void AddCookie(HttpCookie cookie) => this.Cookies.Add(cookie);

        public override string ToString()
        {
            var result = new StringBuilder();

            result
                .AppendLine($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetResponseLine()}")
                .AppendLine(this.Headers.ToString());

            if (this.Cookies.HasCookies())
            {
                foreach (var httpCookie in this.Cookies)
                {
                    result.AppendLine($"{GlobalConstants.CookieResponsetHeaderName}: {httpCookie}");
                }
            }

            result.AppendLine();
            return result.ToString();
        }
    }
}