using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Moq.AutoMock;
using Moq.Protected;

namespace XUnitTestProject1
{
    public abstract class TestBase<TSut>
        where TSut : class
    {
        private readonly AutoMocker _mocker = new AutoMocker();

        protected TestBase()
        {
            RegisterMocksBase();
            SystemUnderTest = _mocker.CreateInstance<TSut>();
        }

        protected TSut SystemUnderTest { get; private set; }

        public Mock<HttpMessageHandler> SetUpHttpMessageHandleMock(HttpResponseMessage response)
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            if (response != null)
            {
                httpMessageHandlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = response.StatusCode,
                        Content = response.Content,
                    })
                    .Verifiable();
            }
            return httpMessageHandlerMock;
        }

        protected void ResetSut(Action<AutoMocker> setupMocker)
        {
            setupMocker?.Invoke(_mocker);

            SystemUnderTest = _mocker.CreateInstance<TSut>();
        }

        protected virtual void RegisterMocks(AutoMocker mocker)
        {
        }

        protected Mock<TService> GetMock<TService>()
            where TService : class
        {
            return _mocker.GetMock<TService>();
        }

        protected T Any<T>() => It.IsAny<T>();

        protected T Some<T>() => new Fixture().Create<T>();

        protected string SomeUrl() => "https://url";

        private void RegisterMocksBase()
        {
            RegisterMocks(_mocker);
        }
    }
}
