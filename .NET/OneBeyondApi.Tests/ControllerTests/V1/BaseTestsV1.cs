using OneBeyondApi.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneBeyondApi.Tests.ControllerTests.V1
{
    public abstract class BaseTestsV1 : BaseTest
    {
        protected override HttpClient CreateHttpClient()
        {
            var client = base.CreateHttpClient();
            client.AddApiVersionHeader(apiVersion: "1.0");
            return client;
        }
    }
}
