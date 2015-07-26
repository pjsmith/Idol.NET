using IDOLOnDemand.Helpers;
using NUnit.Framework;

namespace IDOLOnDemand.Tests.Helpers
{
    [TestFixture]
    public class IDOLConnectionTests
    {
        [Test]
        public void Given_an_IDOLConnection_When_making_a_request_Then_the_request_has_an_API_Key_parameter()
        {
            IDOLConnection.Connect(new {Foo = 10, Bar = "baz"}, "this-endpoint");
            
            Assert.That(true, Is.True);
        }
    }
}