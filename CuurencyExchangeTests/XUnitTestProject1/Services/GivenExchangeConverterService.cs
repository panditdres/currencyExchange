using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExchange.Interfaces;
using CurrencyExchange.Models;
using CurrencyExchange.Services;
using Moq;
using Xunit;

namespace XUnitTestProject1.Services
{
    public class GivenExchangeConverterService : TestBase<ExchangeConverterService>
    {
        [Fact]
        public void ExchangeCurrencyAsync_WhenRequestReceivedNull_ThenThrowException()
        {
            // Arrange, Act, Assert
            Assert.ThrowsAsync<HttpRequestException>(() => SystemUnderTest.GetConvertedCurrencyAsync(null));
        }

        [Fact]
        public async Task ExchangeCurrencyAsync_WhenRequestReceived_ThenReturnModel()
        {
            // Arrange
            var request = Some<CurrencyRequest>();

            GetMock<IRatesService>()
                .Setup(x => x.GetRatesAsync(request.SourceCurrency))
                .ReturnsAsync(Some<RatesResponse>());

            // Act
            var response = await SystemUnderTest.GetConvertedCurrencyAsync(request);

            // Assert
            Assert.IsType<ConvertedCurrencyResponse>(response);
            Assert.Equal(0, response.ConvertedAmount);
        }

        [Fact]
        public async Task ExchangeCurrencyAsync_WhenGBPBaseRequestReceived_AndTargetIsEuro_ThenReturnConvertedValue()
        {
            // Arrange
            var request = new CurrencyRequest()
            {
                TargetCurrency = "EUR",
                SourceCurrency = "GBP",
                Amount = 10,
            };

            var rateResponse = new RatesResponse()
            {
                Base = "GBP",
                Date = Some<string>(),
                Rates = new Dictionary<string, decimal>()
                {
                    { "EUR", 2 },
                }
            };

            GetMock<IRatesService>()
                .Setup(x => x.GetRatesAsync(request.SourceCurrency))
                .ReturnsAsync(rateResponse);

            // Act
            var response = await SystemUnderTest.GetConvertedCurrencyAsync(request);

            // Assert
            Assert.IsType<ConvertedCurrencyResponse>(response);
            Assert.Equal(20, response.ConvertedAmount);
        }
    }
}
