﻿using System.Collections.Concurrent;

namespace HTTP.Sessions
{
    public class HttpSessionStorage
    {
        public const string SessionCookieKey = "SIS_ID";

        private static readonly ConcurrentDictionary<string, IHttpSession> sessions = new ConcurrentDictionary<string, IHttpSession>();

        public static IHttpSession GetSession(string id) => sessions.GetOrAdd(id, _ => new HttpSession(id));
    }
}