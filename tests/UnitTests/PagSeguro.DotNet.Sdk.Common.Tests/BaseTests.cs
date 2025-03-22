using AutoFixture;
using Flurl.Http.Testing;
using PagSeguro.DotNet.Sdk.Common.Serialization;

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
            httpTestMock.Settings.JsonSerializer = DefaultSerializer.Build();
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
            GC.SuppressFinalize(this);
        }
    }
}
