using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly Dictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }
        
        public void Add(HttpCookie cookie)
        {
            if (cookie == null)
            {
                throw new ArgumentNullException();
            }

            this.cookies[cookie.Key] = cookie; //should we override cookies
        }

        public bool ContainsCookie(string key) => this.cookies.ContainsKey(key);

        public HttpCookie GetCookie(string key)
        {
            if (!this.ContainsCookie(key))
            {
                return null;
            }
            return this.cookies[key];
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            foreach (var cookie in this.cookies)
            {
                yield return cookie.Value;
            }
        }

        public bool HasCookies() => this.cookies.Any();

        public override string ToString() => string.Join("; ", this.cookies.Values);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
