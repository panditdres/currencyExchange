using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExchange.Interfaces;
using CurrencyExchange.Models;

namespace CurrencyExchange.Services
{
    public class ExchangeConverterService : IExchangeConverterService
    {
        private readonly IRatesService _ratesService;
        public ExchangeConverterService(IRatesService ratesService)
        {
            _ratesService = ratesService;
        }

        public async Task<ConvertedCurrencyResponse> GetConvertedCurrencyAsync(CurrencyRequest request)
        {
            if (request == null)
            {
                throw new HttpRequestException();
            }

            var rates = await _ratesService.GetRatesAsync(request.SourceCurrency);
            rates.Rates.TryGetValue(request.TargetCurrency, out var value);

            var convertedValue = ConvertAmount(value, request.Amount);

             return new ConvertedCurrencyResponse()
             {
                 TargetCurrency = request.TargetCurrency,
                 ConvertedAmount = convertedValue,
             };
        }

        private static decimal ConvertAmount(decimal rate, decimal amount)
        {
            return rate * amount;
        }
    }
}
