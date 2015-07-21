using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDOLOnDemand.Authentication;
using IDOLOnDemand.Model;
using NUnit.Framework;

namespace IDOLOnDemand.Tests
{
    public class BarcodeRecognitionTests
    {
        [Test]
        public void BarcodeRecogTest()
        {
            const string apiKey = "foo";

            var auth = new ApiKeyAuthenticator(apiKey);

            var bc = new BarcodeRecognition(new Uri("http://idolondemand.com"), auth);

            bc.Execute(BarcodeRecognition.BarcodeType.All, BarcodeRecognition.Orientation.Upright, new Uri("http://example.com"));
        }
    }

    public class IdolGateway
    {
        public IdolGateway()
        {
            
        }
    }
}
