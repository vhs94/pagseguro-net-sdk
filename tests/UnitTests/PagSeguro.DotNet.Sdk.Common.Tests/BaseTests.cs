using AutoFixture;
using Flurl.Http.Configuration;
using Flurl.Http.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PagSeguro.DotNet.Sdk.Common.Tests
{
    public abstract class BaseTests : IDisposable
    {
        public Fixture Fixture { get; }
        public HttpTest HttpTestMock { get; }

        protected BaseTests()
        {
            Fixture = new Fixture();
            HttpTestMock = CreateHttpTestMock();
            InitializeMocks();
        }

        private static HttpTest CreateHttpTestMock()
        {
            var httpTestMock = new HttpTest();
            httpTestMock.Configure(settings =>
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                settings.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
            });
            return httpTestMock;
        }

        protected virtual void InitializeMocks()
        {
            CreateMocks();
            SetupMocks();
        }

        protected virtual void CreateMocks() { }

        protected virtual void SetupMocks() { }

        public void Dispose()
        {
            HttpTestMock.Dispose();
        }
    }
}
