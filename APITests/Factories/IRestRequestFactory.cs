using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace TestBdd.tests
{
    public interface IRestRequestFactory
    {
        public RestRequest Create(string url, Method method);
    }
}
