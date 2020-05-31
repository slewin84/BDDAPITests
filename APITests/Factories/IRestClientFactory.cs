using RestSharp;

namespace TestBdd.tests
{
    public interface IRestClientFactory
    {
        RestClient Create(string baseUrl);
    }
}
