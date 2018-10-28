using System;
using System.Collections.Generic;
using System.Linq;
using HTTP.Common;
using HTTP.Cookies;
using HTTP.Enums;
using HTTP.Exceptions;
using HTTP.Headers;
using HTTP.Sessions;

namespace HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public IHttpCookieCollection Cookies { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public IHttpSession Session { get; set; }

        private bool IsValidRequestLine(string[] requestLine)
        {
            if (!requestLine.Any())
            {
                throw new BadRequestException();
            }

            int elementsCount = requestLine.Length;
            string thirdElementContent = requestLine[2];

            if (elementsCount == 3 && thirdElementContent == GlobalConstants.HttpOneProtocolFragment)
            {
                return true;
            }
            return false;
        }

        //private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        //{
        //    if (!string.IsNullOrEmpty(queryString) || queryParameters.Length > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private void ParseRequestMethod(string[] requestLine)
        {
            string requestMethodUpperCase = requestLine[0];
            string requestMethodTitleCase = requestMethodUpperCase[0] + requestMethodUpperCase.Substring(1).ToLower();
            
            bool isParsed = Enum.TryParse<HttpRequestMethod>(requestMethodTitleCase, out var requestMethod);

            if (!isParsed)
            {
                throw new BadRequestException();
            }

            this.RequestMethod = requestMethod;
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            if (string.IsNullOrEmpty(requestLine[1]))
            {
                throw new BadRequestException();
            }

            this.Url = requestLine[1];
        }

        private void ParseRequestPath()
        {
            //Uri uri = new Uri(this.Url);
            //this.Path = uri.AbsolutePath;

            string path = this.Url.Split('?').FirstOrDefault(); // Във видеоте е this.Url?.Split...
            if (string.IsNullOrEmpty(path))
            {
                throw new BadImageFormatException();
            }

            this.Path = path;
        }

        private void ParseHeaders(string[] requestHeaders)
        {
            if (!requestHeaders.Any())
            {
                throw new BadRequestException();
            }

            var splitHeaderCollection = new Dictionary<string, string>();

            foreach (var header in requestHeaders)
            {
                if (string.IsNullOrEmpty(header))
                {
                    return;
                }

                string[] splitHeader = header.Split(": ", StringSplitOptions.RemoveEmptyEntries);
                string key = splitHeader[0];
                string value = splitHeader[1];

                splitHeaderCollection.Add(key, value);
            }

            if (!splitHeaderCollection.ContainsKey(GlobalConstants.HostHeaderKey))
            {
                throw new BadRequestException();
            }

            foreach (var header in splitHeaderCollection)
            {
                HttpHeader httpHeader = new HttpHeader(header.Key, header.Value);
                this.Headers.Add(httpHeader);
            }
        }

        private void ParseQueryParameters()
        {
            if (!this.Url.Contains('?'))
            {
                return;
            }

            string queryString = string.Empty;
            string[] splitUrl = this.Url.Split(new[] { '?', '#' });
            if (splitUrl.Length > 1)
            {
                queryString = splitUrl
                    .Skip(1)
                    .ToArray()[0]
                    .ToString();
            }

            if (string.IsNullOrWhiteSpace(queryString))
            {
                return;
            }

            string[] queryParameters = queryString.Split('&', StringSplitOptions.RemoveEmptyEntries);

            //if (!IsValidRequestQueryString(queryString, queryParameters))
            //{
            //    throw new BadRequestException();
            //}

            foreach (var queryParameter in queryParameters)
            {
                string[] kvp = queryParameter.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (kvp.Length != 2)
                {
                    throw new BadRequestException();
                }

                string key = kvp[0];
                string value = kvp[1];

                this.QueryData[key] = value; //should we override the value
            }
        }

        private void ParseFormDataParameters(string formData)
        {
            string[] formDataParams = formData.Split('&', StringSplitOptions.RemoveEmptyEntries);

            foreach (var param in formDataParams)
            {
                string[] kvp = param.Split('=');

                if (kvp.Length != 2)
                {
                    throw new BadRequestException();
                }

                string key = kvp[0];
                string value = kvp[1];

                this.FormData[key] = value; //should we override the value
            }
        }

        private void ParseRequestParameters(string formData)
        {
            ParseQueryParameters();
            ParseFormDataParameters(formData);
        }

        private void ParseCookies()
        {
            if (!this.Headers.ContainsHeader(GlobalConstants.CookieRequestHeaderName))
            {
                return;
            }

            string cookieHeader = this.Headers.GetHeader(GlobalConstants.CookieRequestHeaderName).Value;
            string[] cookies = cookieHeader.Split("; ", StringSplitOptions.RemoveEmptyEntries);

            foreach (var cookie in cookies)
            {
                string[] cookieKeyValuePair = cookie.Split('=', 2);

                if (cookieKeyValuePair.Length != 2)
                {
                    throw new BadRequestException();
                }

                string cookieName = cookieKeyValuePair[0];
                string cookieValue = cookieKeyValuePair[1];
                this.Cookies.Add(new HttpCookie(cookieName, cookieValue));
            }
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString.Split(Environment.NewLine + Environment.NewLine);
            string[] splitRequestFirstPart = splitRequestContent[0].Split(Environment.NewLine);

            string[] requestLine = splitRequestFirstPart[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);
            this.ParseRequestPath();

            string[] requestHeaders = splitRequestFirstPart.Skip(1).ToArray();
            this.ParseHeaders(requestHeaders);
            this.ParseCookies();

            string requestBody = splitRequestContent.Skip(1).LastOrDefault();
            this.ParseRequestParameters(requestBody);
        }
    }
}
