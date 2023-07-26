using AutoFixture;
using Flurl.Http.Testing;

namespace PagSeguro.DotNet.Sdk.Common.Tests
{
    public abstract class BaseTests : IDisposable
    {
        public Fixture Fixture { get; }
        public HttpTest HttpTestMock { get; }

        protected BaseTests()
        {
            Fixture = new Fixture();
            HttpTestMock = new HttpTest();
            InitializeMocks();
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
