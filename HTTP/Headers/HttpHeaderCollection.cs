﻿using System;
using System.Collections.Generic;

namespace HTTP.Headers
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        private readonly Dictionary<string, HttpHeader> headers;
        
        public void Add(HttpHeader header)
        {
            if (header == null ||
                string.IsNullOrEmpty(header.Key) ||
                string.IsNullOrEmpty(header.Value) ||
                this.ContainsHeader(header.Key))
            {
                throw new Exception();
            }

            this.headers.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} cannot be null");
            }

            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} cannot be null");
            }

            if (ContainsHeader(key))
            {
                return this.headers[key];
            }

            return null;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, this.headers.Values);
        }
    }
}