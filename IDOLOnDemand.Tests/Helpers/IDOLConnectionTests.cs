using System;
using System.Linq;
using FluentAssertions;
using IDOLOnDemand.Helpers;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace IDOLOnDemand.Tests.Helpers
{
    [TestFixture]
    public class IDOLConnectionTests
    {
        [Test]
        public void Given_an_IDOLConnection_When_making_a_request_Then_the_request_has_an_API_Key_parameter()
        {
            string testApiKey = Guid.NewGuid().ToString("D");
            const string baseUri = "http://example.com";

            // Arrange
            IRestRequest actualRequest = null;
            var mockRestClient = new Moq.Mock<IRestClient>(MockBehavior.Strict);
            mockRestClient
                .Setup(c => c.Execute(It.IsAny<IRestRequest>()))
                .Callback<IRestRequest>(r => actualRequest = r)
                .Returns(new RestResponse());

            // Act
            var connection = new IDOLConnection(_ => mockRestClient.Object, baseUri, testApiKey);
            connection.SendRequest(new {Foo = 10, Bar = "baz"}, "this-endpoint");
            
            // Assert
            actualRequest.Parameters.Should().Contain(p => p.Name == "apikey" && p.Value.Equals(testApiKey));
        }
    }
}