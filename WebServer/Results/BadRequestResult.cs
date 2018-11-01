﻿using HTTP.Enums;
using HTTP.Headers;
using HTTP.Responses;
using System;
using System.Text;

namespace WebServer.Results
{
    public class BadRequestResult : HttpResponse
    {
        private const string DefaultErrorHeading = "<h1>Error of type occured, see details</h1>";

        public BadRequestResult(string content, HttpResponseStatusCode responseStatusCode)
           : base(responseStatusCode)
        {
            content = DefaultErrorHeading + Environment.NewLine + content;
            this.Headers.Add(new HttpHeader("Content-type", "text/html"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
