using System.Collections.Concurrent;
using Demo.App.Sessions;
using Demo.App.Sessions.Contracts;

namespace SIS.WebServer.Sessions
{
    public class HttpSessionStorage
    {
        public const string SessionCookieKey = "SIS_ID";

        private static readonly ConcurrentDictionary<string, IHttpSession> httpSessions =
            new ConcurrentDictionary<string, IHttpSession>();

        public static IHttpSession GetSession(string id)
        {
            return httpSessions.GetOrAdd(id, _ => new HttpSession(id));
        }
    }
}