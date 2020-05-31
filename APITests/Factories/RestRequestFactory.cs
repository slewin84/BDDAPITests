using RestSharp;

namespace TestBdd.tests
{
    public class RestRequestFactory : IRestRequestFactory
    {
        public RestRequest Create(string url, Method method)
        {
            var rRequest = new RestRequest(url, method);
            return rRequest;
        }
    }
}
