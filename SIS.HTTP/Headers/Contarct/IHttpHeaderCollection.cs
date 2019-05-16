using System.Net.Http.Headers;

namespace SIS.HTTP.Headers.Contarct
{
    public interface IHttpHeaderCollection
    {

        void AddHeader(HttpHeader header);
        bool ContainsHeader(string key);
        HttpHeader GetHeader(string key);
    }
}