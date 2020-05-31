using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace TestBdd.tests
{
    public class RestClientFactory : IRestClientFactory
    {
        public RestClient Create(string baseUrl)
        {
            return new RestClient(baseUrl);
        }
    }
}
