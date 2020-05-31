using System.Collections.Generic;
using System.Net;
using APITests.Models;
using FluentAssertions;
using RestSharp;
using Xunit;
using TestStack.BDDfy;
using Newtonsoft.Json;

namespace TestBdd.tests
{
    [Story(
        AsA = "API user",
        IWant = "to be able to send and receive posts to a blog",
        SoThat = "I can update the content via API"
    )]

    public class JsonEndpointTests
    {
        private IRestClientFactory _restClientFactory;
        private IRestRequestFactory _restRequestFactory;
        private static string _baseEndpoint;
        private static HttpStatusCode _responseCode;

        private static List<PostModel> _allPosts;
        private static int _id;

        public JsonEndpointTests()
        {
            this._restClientFactory = new RestClientFactory();
            this._restRequestFactory = new RestRequestFactory();

            _id = 1;

            _baseEndpoint = "https://jsonplaceholder.typicode.com/";
        }

        [Fact]
        public void PostANewPost()
        {
            this.Given(_ => SendNewPost(), "Given I send a new post as a POST request as a client")
                .Then(_ => CheckStatus(HttpStatusCode.Created), "Then the API server returns the correct status")
                .BDDfy("Client can send a new post to the API");
        }

        [Fact]
        public void GetAllPostsByUser()
        {
            this.Given(_ => SendGetPosts(),
                    "Given I send a GET request as a client to the server for posts by a particular user")
                .Then(_ => ReadContentOfPosts(), "Then I am able to read the content of each post")
                .BDDfy("Client is able to retrieve all posts by user");
        }

        [Fact]
        public void DeletePost()
        {
            this.Given(_ => SendDeletePostRequest(), "Given I have created a valid flyer")
                .Then(_ => CheckStatus(HttpStatusCode.OK), "Then the API returns the correct output in json format for this user")
                .BDDfy("Client does NOT receive a success response for an invalid request");
        }

        private void ReadContentOfPosts()
        {
            foreach (var post in _allPosts)
            {
                post.userId.Should().Equals(_id);
                post.body.Should().NotBeNullOrEmpty();
                post.title.Should().NotBeNullOrEmpty();
            }
        }

        private void SendGetPosts()
        {
            var restClient = _restClientFactory.Create(_baseEndpoint);
            var restRequest = _restRequestFactory.Create(string.Format("{0}posts?userId={1}", _baseEndpoint, _id), Method.GET);
            restRequest.AddHeader("Content-Type", "application/json; charset=utf-8");

            var response = restClient.Execute(restRequest);
            _responseCode = response.StatusCode;
            _allPosts = JsonConvert.DeserializeObject<List<PostModel>>(response.Content);
        }

        private void SendDeletePostRequest()
        {
            var restClient = _restClientFactory.Create(_baseEndpoint);
            var restRequest = _restRequestFactory.Create(string.Format("{0}posts/1", _baseEndpoint), Method.DELETE);
            restRequest.AddHeader("Content-Type", "application/json; charset=utf-8");

            var response = restClient.Execute(restRequest);

            _responseCode = response.StatusCode;
        }

        private void SendNewPost()
        {
            var newPost = new PostModel()
            {
                userId =  "1",  title = "TEST DATA TITLE", body = "TEST DATA BODY"
            };

            var restClient = _restClientFactory.Create(_baseEndpoint);
            var restRequest = _restRequestFactory.Create(string.Format("{0}posts",  _baseEndpoint), Method.POST);
            restRequest.AddHeader("Content-Type", "application/json; charset=utf-8");
            restRequest.AddJsonBody(newPost);

            var response = restClient.Execute(restRequest);

            _responseCode = response.StatusCode;
        }

        public void CheckStatus(HttpStatusCode ExpectedStatusCode)
        {
            _responseCode.Should().BeEquivalentTo(ExpectedStatusCode);
        }
    }
}
